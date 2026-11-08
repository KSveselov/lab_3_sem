using System;
using System.Drawing;

namespace RoutePlanning;

public static class PathFinderTask
{
	public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
	{
		int[] path = new int[checkpoints.Length];
		for (int i = 0; i < path.Length; i++)
			path[i] = i;

		if (path.Length < 2)
			return path;

		int[] bestPath = new int[path.Length];
		Search(checkpoints, path, bestPath, 1, 0, double.PositiveInfinity);
		return bestPath;
	}

	private static double Search(
		Point[] points,
		int[] path,
		int[] bestPath,
		int step,
		double length,
		double bestLength)
	{
		if (length >= bestLength)
			return bestLength;

		if (step == path.Length)
		{
			Array.Copy(path, bestPath, path.Length);
			return length;
		}

		Point from = points[path[step - 1]];
		for (int i = step; i < path.Length; i++)
		{
			Swap(path, step, i);
			double nextLength = length + from.DistanceTo(points[path[step]]);
			bestLength = Search(points, path, bestPath, step + 1, nextLength, bestLength);
			Swap(path, step, i);
		}

		return bestLength;
	}

	private static void Swap(int[] path, int first, int second)
	{
		int temp = path[first];
		path[first] = path[second];
		path[second] = temp;
	}
}
