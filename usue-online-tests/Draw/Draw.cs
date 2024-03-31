using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


public class Draw : Controller
{
    public static int[] PrivateBoards { get; set; } = { 7, 9 };
    public static Dictionary<int, List<WebSocket>> Sockets { get; set; } = new();
    public static Dictionary<int, List<byte[]>> History { get; set; } = new();
    private const int MaxArrayLength = 10000 * 50;
    public static BoardManager BoardManager { get; } = new(History);
    public event Action<WebSocket, int> ClientDisconnect;
    public event Action<WebSocket, int> ClientConnected;

    public Draw()
    {
        ClientDisconnect += SaveBoard;
        ClientDisconnect += EventForClient;
        ClientConnected += OnClientConnected;
    }

    private async void OnClientConnected(WebSocket socket, int id)
    {
        await SendData(socket, Encoding.UTF8.GetBytes("message:::Пользователь подключился"), id);
    }

    private async void EventForClient(WebSocket socket, int id)
    {
        await SendData(socket, Encoding.UTF8.GetBytes("disconnect:::"), id);
    }

    private void SaveBoard(WebSocket client, int boardId)
    {
        if (Sockets[boardId].Count == 0)
        {
            BoardManager.SaveBoard(boardId, $"{boardId}.board");
            History[boardId] = new();
        }
    }

    public IActionResult Index()
    {
        List<string> colorList = new List<string>
            {
                "rgb(0, 0, 0)", "rgb(52, 58, 64)", "rgb(73, 80, 87)", "rgb(201, 42, 42)", "rgb(166, 30, 77)",
                "rgb(134, 46, 156)", "rgb(95, 61, 196)", "rgb(54, 79, 199)", "rgb(24, 100, 171)", "rgb(11, 114, 133)",
                "rgb(8, 127, 91)", "rgb(43, 138, 62)", "rgb(92, 148, 13)", "rgb(230, 119, 0)", "rgb(217, 72, 15)"
            };

        return View("Index", colorList);
    }

    [Route("/draw/ws")]
    public async Task StartSocketDraw(int id)
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            return;
        }

        var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();

        if (!Sockets.ContainsKey(id))
        {
            Sockets.Add(id, new());
        }

        if (Sockets[id].Count == 0)
        {
            BoardManager.LoadBoard(id, $"{id}.board");
        }

        Sockets[id].Add(socket);

        ClientConnected(socket, id);
        await socket.SendAsync(Encoding.UTF8.GetBytes($"message:::Элементов на доске: {History[id].Count}"), WebSocketMessageType.Text, true,
            CancellationToken.None);

        if (!History.ContainsKey(id))
            History.Add(id, new());

        try
        {
            foreach (var t in History[id])
            {
                await socket.SendAsync(t, WebSocketMessageType.Text, true, CancellationToken.None);
            }

        }
        catch
        {
            // ignored
        }

        if (PrivateBoards.Contains(id))
        {
            await socket.SendAsync(Encoding.UTF8.GetBytes("message:::Это закрытая доска. Изменения не сохраняются"),
                WebSocketMessageType.Text, true, CancellationToken.None);
        }

        while (socket.State == WebSocketState.Open)
        {
            try
            {
                byte[] bufferSize = new byte[32];
                await socket.ReceiveAsync(bufferSize, CancellationToken.None);

                bool successParse = int.TryParse(Encoding.UTF8.GetString(bufferSize), out int size);

                if (!successParse || PrivateBoards.Contains(id))
                    continue;

                byte[] buffer = new byte[size];

                await socket.ReceiveAsync(buffer, CancellationToken.None);
                string message = Encoding.UTF8.GetString(buffer);

                AddToHistory(buffer, id);
                await DeleteHistory(buffer, id, socket);

                if (message == "save")
                {
                    BoardManager.SaveBoard(id, $"{id}.board");
                }

                await SendData(socket, buffer, id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                break;
            }
        }

        Sockets[id].Remove(socket);
        ClientDisconnect(socket, id);
    }

    private async Task DeleteHistory(byte[] buffer, int id, WebSocket socket)
    {
        string message = Encoding.UTF8.GetString(buffer);
        string[] parts = message.Split(":::");

        if (parts.Length < 2)
            return;

        if (parts[0] == "delete")
        {
            string delId = (JsonConvert.DeserializeObject(parts[1]) as dynamic)?.objId.ToString();

            var historyToDelete = History[id].Where(bytes => delId != null && Encoding.UTF8.GetString(bytes).Contains(delId)).ToArray();

            foreach (byte[] bytes in historyToDelete)
            {
                History[id].Remove(bytes);
            }
        }
    }

    private static void AddToHistory(byte[] buffer, int id)
    {
        if (History[id].Count > MaxArrayLength)
        {
            History[id].Remove(History[id][0]);
        }

        var message = Encoding.UTF8.GetString(buffer);

        if (message.StartsWith("clear"))
        {
            History[id].Clear();
        }

        if (message.StartsWith("cur"))
            return;

        //if (message.StartsWith("line"))
        History[id].Add(buffer);
    }

    private async Task SendData(WebSocket socketSender, byte[] buffer, int id)
    {
        for (var i = 0; i < Sockets[id].Count; i++)
        {
            var client = Sockets[id][i];
            if (client.State != WebSocketState.Open)
            {
                continue;
            }

            if (socketSender == client)
                continue;

            await client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

    ~Draw()
    {
        ClientDisconnect -= SaveBoard;
        ClientDisconnect -= EventForClient;
        ClientConnected -= OnClientConnected;
    }
}

