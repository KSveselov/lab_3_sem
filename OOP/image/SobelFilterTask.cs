using System;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var result = new double[width, height];
        int kernelSize = sx.GetLength(0);
        int radius = kernelSize / 2;
        for (int x = radius; x < width - radius; x++)
            for (int y = radius; y < height - radius; y++)
            {
                double gx = 0;
                double gy = 0;
                for (int i = 0; i < kernelSize; i++)
                {
                    for (int j = 0; j < kernelSize; j++)
                    {
                        double pixel = g[x + i - radius, y + j - radius];
                        gx += sx[i, j] * pixel;
                        gy += sx[j, i] * pixel;
                    }
                }
                result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            }
        return result;
    }
}