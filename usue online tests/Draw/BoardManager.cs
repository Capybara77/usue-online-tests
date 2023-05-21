using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;


public class BoardManager
{
    public string PathToSaves { get; } = "saves";
    public Dictionary<int, List<byte[]>> Boards { get; }

    public BoardManager(Dictionary<int, List<byte[]>> boards)
    {
        Boards = boards;

        if (!Directory.Exists(PathToSaves))
        {
            Directory.CreateDirectory(PathToSaves);
        }
    }


    public void SaveBoard(int id, string fileName)
    {
        if (!Boards.ContainsKey(id))
        {
            throw new ArgumentException(nameof(id));
        }

        string pathToFile = $"{PathToSaves}/{fileName}";

        if (File.Exists(pathToFile))
            File.Delete(pathToFile);

        File.WriteAllText(pathToFile, JsonConvert.SerializeObject(Boards[id]));
    }

    public void LoadBoard(int id, string fileName)
    {
        string pathToFile = $"{PathToSaves}/{fileName}";

        if (!Boards.ContainsKey(id))
            Boards.Add(id, new());

        if (!File.Exists(pathToFile))
            return;

        Boards[id] = JsonConvert.DeserializeObject<List<byte[]>>(File.ReadAllText(pathToFile)) ?? new();
    }
}

