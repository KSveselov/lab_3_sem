using System.Collections.Generic;
using NUnit.Framework;

namespace Recognizer;

internal static class MedianFilterTask
{
	/* 
	 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
	 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
	 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
	 * https://en.wikipedia.org/wiki/Median_filter
	 * 
	 * Используйте окно размером 3х3 для не граничных пикселей,
	 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
	 */
	public static double[,] MedianFilter(double[,] original)
	{
		int width = original.GetLength(0);
		int height = original.GetLength(1);
		var result = new double[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{

				List<double> values = GetNeighbor(x, y, original);
				values.Sort();
				int middle = values.Count / 2;
				if (values.Count % 2 == 0)
				{
					result[x, y] = (values[middle - 1] + values[middle]) / 2.0;
				}
				else
				{
					result[x, y] = values[middle];
				}
			}
		}

		return result;
	}

	public static List<double> GetNeighbor(int x, int y, double[,] original)
	{	
		int width = original.GetLength(0);
		int height = original.GetLength(1);
		var values = new List<double>();
		for (int dx = -1; dx <= 1; dx++)
		{
			for (int dy = -1; dy <= 1; dy++)
			{
				int neighborX = x + dx;
				int neighborY = y + dy;
				if (neighborX >= 0 && neighborX < width
					&& neighborY >= 0 && neighborY < height)
				{
					values.Add(original[neighborX, neighborY]);
				}
			}
		}
		return values;
	}
}
