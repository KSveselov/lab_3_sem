using System;

namespace DistanceTask;

public static class DistanceTask
{
    // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
public static double GetDistanceToSegment(
      double ax, double ay, double bx, double by, double x, double y)
  {
      var dx = bx - ax;
      var dy = by - ay;
      var length = dx * dx + dy * dy;

      if (length == 0)
          return Math.Sqrt((x - ax) * (x - ax) + (y - ay) * (y - ay));

      var t = ((x - ax) * dx + (y - ay) * dy) / length;
      t = Math.Max(0, Math.Min(1, t));

      var nearestX = ax + t * dx;
      var nearestY = ay + t * dy;

      return Math.Sqrt(
          (x - nearestX) * (x - nearestX) +
          (y - nearestY) * (y - nearestY));
  }

}