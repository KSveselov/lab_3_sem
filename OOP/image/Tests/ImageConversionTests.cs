using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Skia;
using NUnit.Framework;
using Recognizer.UI;

namespace Recognizer.Tests;

[TestFixture]
public class ImageConversionTests
{
    [OneTimeSetUp]
    public void InitializeRenderer()
    {
        SkiaPlatform.Initialize();
    }

    [Test]
    public void LoadPixels_PreservesColorsAndCoordinates()
    {
        using var bitmap = new WriteableBitmap(new PixelSize(3, 2), new Vector(96, 96),
            PixelFormat.Bgra8888, AlphaFormat.Opaque);
        var colors = new[]
        {
            unchecked((int)0xFFFF0000), unchecked((int)0xFF00FF00), unchecked((int)0xFF0000FF),
            unchecked((int)0xFF123456), unchecked((int)0xFF000000), unchecked((int)0xFFFFFFFF)
        };
        using (var buffer = bitmap.Lock())
        {
            for (var y = 0; y < 2; y++)
                Marshal.Copy(colors, y * 3, IntPtr.Add(buffer.Address, y * buffer.RowBytes), 3);
        }

        var pixels = MainWindow.LoadPixels(bitmap);

        Assert.That(pixels.GetLength(0), Is.EqualTo(3));
        Assert.That(pixels.GetLength(1), Is.EqualTo(2));
        for (var y = 0; y < 2; y++)
        for (var x = 0; x < 3; x++)
        {
            var color = colors[y * 3 + x];
            Assert.That(pixels[x, y].R, Is.EqualTo((color >> 16) & 255));
            Assert.That(pixels[x, y].G, Is.EqualTo((color >> 8) & 255));
            Assert.That(pixels[x, y].B, Is.EqualTo(color & 255));
        }
    }
}
