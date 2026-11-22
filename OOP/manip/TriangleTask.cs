using System;
using NUnit.Framework;

namespace Manipulation;

public class TriangleTask
{
    /// <summary>
    /// Возвращает угол (в радианах) между сторонами a и b в треугольнике со сторонами a, b, c 
    /// </summary>
    public static double GetABAngle(double a, double b, double c)
    {
        if (!double.IsFinite(a) || !double.IsFinite(b) || !double.IsFinite(c))
        {
            return double.NaN;
        }

        if (a <= 0 || b <= 0 || c < 0)
        {
            return double.NaN;
        }

        if (a < b)
        {
            var originalA = a;
            a = b;
            b = originalA;
        }

        var aMinusB = a - b;
        var cMinusA = c - a;
        
        double gapA;
        if (c < b)
        {
            gapA = (c - aMinusB) / b;
        }
        else
        {
            gapA = (b + cMinusA) / b;
        }

        double gapC;
        if (cMinusA > 0)
        {
            gapC = (b - cMinusA) / b;
        }
        else
        {
            gapC = 1 - cMinusA / b;
        }

        if (gapA < 0 || gapC < 0)
        {
            return double.NaN;
        }

        var gapB = aMinusB / a + c / a;
        var scaledPerimeter = 1 + b / a + c / a;
        var numerator = Math.Sqrt(gapA) * Math.Sqrt(gapB);
        var denominator = Math.Sqrt(gapC) * Math.Sqrt(scaledPerimeter);

        var halfAngle = Math.Atan2(numerator, denominator);
        return 2 * halfAngle;
    }
}

[TestFixture]
public class TriangleTask_Tests
{
    [TestCase(3, 4, 5, Math.PI / 2)]
    [TestCase(1, 1, 1, Math.PI / 3)]
    [TestCase(3, 4, 7, Math.PI)]
    [TestCase(4, 3, 1, 0)]
    [TestCase(3, 4, 1, 0)]
    [TestCase(2, 2, 0, 0)]
    [TestCase(1, 1, 1e-12, 1e-12)]
    [TestCase(double.MaxValue, double.MaxValue, double.MaxValue, Math.PI / 3)]
    [TestCase(double.Epsilon, double.Epsilon, double.Epsilon, Math.PI / 3)]
    [TestCase(0, 2, 2, double.NaN)]
    [TestCase(2, 0, 2, double.NaN)]
    [TestCase(0, 0, 0, double.NaN)]
    [TestCase(2, 3, 0, double.NaN)]
    [TestCase(-1, 2, 2, double.NaN)]
    [TestCase(2, -1, 2, double.NaN)]
    [TestCase(2, 2, -1, double.NaN)]
    [TestCase(5, 2, 2, double.NaN)]
    [TestCase(2, 5, 2, double.NaN)]
    [TestCase(2, 2, 5, double.NaN)]
    [TestCase(double.NaN, 2, 2, double.NaN)]
    [TestCase(2, double.PositiveInfinity, 2, double.NaN)]
    [TestCase(2, 2, double.NegativeInfinity, double.NaN)]
    public void TestGetABAngle(double a, double b, double c, double expectedAngle)
    {
        var actualAngle = TriangleTask.GetABAngle(a, b, c);
        if (double.IsNaN(expectedAngle))
        {
            Assert.That(actualAngle, Is.NaN);
            return;
        }

        var tolerance = 1e-12;
        if (expectedAngle != 0)
        {
            tolerance *= expectedAngle;
        }

        Assert.That(actualAngle, Is.EqualTo(expectedAngle).Within(tolerance));
    }
}
