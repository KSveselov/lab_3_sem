using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Controls;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace Recognizer.UI;

public partial class MainWindow : Window
{
	private const int ResizeRate = 2;

	public MainWindow()
	{
		InitializeComponent();
		var image = this.Find<Avalonia.Controls.Image>("Default");
		var pixelsImage = this.Find<Avalonia.Controls.Image>("Pixels");
		var grayscaleImage = this.Find<Avalonia.Controls.Image>("Grayscale");
		var clearImage = this.Find<Avalonia.Controls.Image>("Clear");
		var sobellImage = this.Find<Avalonia.Controls.Image>("Sobell");
		var trashholdImage = this.Find<Avalonia.Controls.Image>("Trashhold");

		var bmp = new Bitmap(Path.Combine(AppContext.BaseDirectory, "eurobot.bmp"));
		image.Source = bmp;

		var pixels = LoadPixels(bmp);
		pixelsImage.Source = ConvertToBitmap(pixels);

		var grayscale = GrayscaleTask.ToGrayscale(pixels);
		grayscaleImage.Source = ConvertToBitmap(grayscale);
		
		var clear = MedianFilterTask.MedianFilter(grayscale);
		clearImage.Source = ConvertToBitmap(clear);
		
		var sobell = SobelFilterTask.SobelFilter(clear, new double[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } });
		sobellImage.Source = ConvertToBitmap(sobell);
		
		var trashhold = ThresholdFilterTask.ThresholdFilter(sobell, 0.1);
		trashholdImage.Source = ConvertToBitmap(trashhold);
	}

	public static Pixel[,] LoadPixels(Bitmap bmp)
	{
		var size = bmp.PixelSize;
		using var normalized = new WriteableBitmap(size, bmp.Dpi, PixelFormat.Bgra8888, AlphaFormat.Opaque);
		using var buffer = normalized.Lock();
		bmp.CopyPixels(buffer, AlphaFormat.Opaque);

		var pixels = new Pixel[size.Width, size.Height];
		for (var x = 0; x < size.Width; x++)
		for (var y = 0; y < size.Height; y++)
		{
			var offset = y * buffer.RowBytes + x * 4;
			pixels[x, y] = new Pixel(
				Marshal.ReadByte(buffer.Address, offset + 2),
				Marshal.ReadByte(buffer.Address, offset + 1),
				Marshal.ReadByte(buffer.Address, offset));
		}
		return pixels;
	}

	private static Bitmap ConvertToBitmap(int width, int height, Func<int, int, Color> getPixelColor)
	{
		var bmp = new WriteableBitmap(new PixelSize(ResizeRate * width, ResizeRate * height),
			new Vector(96, 96), PixelFormat.Bgra8888, AlphaFormat.Opaque);
		using var buffer = bmp.Lock();
		var row = new int[bmp.PixelSize.Width];
		for (var y = 0; y < height; y++)
		{
			for (var x = 0; x < width; x++)
				Array.Fill(row, getPixelColor(x, y).ToArgb(), ResizeRate * x, ResizeRate);

			for (var repeat = 0; repeat < ResizeRate; repeat++)
				Marshal.Copy(row, 0, IntPtr.Add(buffer.Address,
					(ResizeRate * y + repeat) * buffer.RowBytes), row.Length);
		}
		return bmp;
	}

	private static Bitmap ConvertToBitmap(Pixel[,] array)
	{
		return ConvertToBitmap(array.GetLength(0), array.GetLength(1),
			(x, y) => Color.FromArgb(array[x, y].R, array[x, y].G, array[x, y].B));
	}

	private static Bitmap ConvertToBitmap(double[,] array)
	{
		return ConvertToBitmap(array.GetLength(0), array.GetLength(1), (x, y) =>
		{
			var gray = (int)(255 * array[x, y]);
			gray = Math.Min(gray, 255);
			gray = Math.Max(gray, 0);
			return Color.FromArgb(gray, gray, gray);
		});
	}
}