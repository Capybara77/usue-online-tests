﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using usue_online_tests.Data;
using usue_online_tests.Helpers;
using usue_online_tests.Models;

namespace usue_online_tests.Controllers;

public class CameraController : Controller
{
    private readonly IHostEnvironment _environment;
    private readonly DataContext _context;

    public CameraController(IHostEnvironment environment, DataContext context)
    {
        _environment = environment;
        _context = context;
    }

    public IActionResult TestCamera()
    {
        return View();
    }

    [HttpPost]
    public async Task SendImage(IFormFile image)
    {
        var path = _environment.ContentRootPath;
        await using var stream = image.OpenReadStream();
        Image img = Image.FromStream(stream);
        img.Save(Path.Combine(path, "camera.jpg"), ImageFormat.Png);
    }

    [HttpPost]
    public bool SendData([FromBody] PredictionResult predictionResult)
    {
        var eyesOpen = PredictionHelper.IsEyesOpen(predictionResult);

        return eyesOpen;
    }
}