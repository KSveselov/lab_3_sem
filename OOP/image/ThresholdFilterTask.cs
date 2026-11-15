using System;

namespace Recognizer;

public static class ThresholdFilterTask
{
	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
	{

		int width = original.GetLength(0);
		int height = original.GetLength(1);
		int whiteCount = (int)(whitePixelsFraction * original.Length);
		var result = new double[width, height];
		if (whiteCount == 0)
		{
			return result;
		}

		var tmp = new double[original.Length];
		int index = 0;
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				tmp[index] = original[x, y];
				index++;
			}
		}

		Array.Sort(tmp);
		double t = tmp[tmp.Length - whiteCount];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if(original[x, y] >= t)
				{
					result[x, y] = 1.0;
				} else
				{
					result[x, y] =  0.0;
				}
			}
		}
		return result;
	}
}
