using System;
using NUnit.Framework;

namespace Recognizer.Tests;

[TestFixture]
public class SobelFilterTests
{
    [Test]
    public void SobelFilter_StandardKernel_CalculatesGradientAndLeavesBorderBlack()
    {
        var image = new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        var kernel = new double[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
        var expected = new double[3, 3];
        expected[1, 1] = Math.Sqrt(640);

        Assert.That(SobelFilterTask.SobelFilter(image, kernel), Is.EqualTo(expected));
    }

    [Test]
    public void SobelFilter_AsymmetricKernel_UsesSuppliedKernelAndItsTranspose()
    {
        var image = new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        var kernel = new double[,] { { 0, 1, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        var result = SobelFilterTask.SobelFilter(image, kernel);

        Assert.That(result[1, 1], Is.EqualTo(Math.Sqrt(20)).Within(1e-12));
    }
    [TestCase(1)]
    [TestCase(3)]
    [TestCase(5)]
    public void SobelFilter_UsesKernelSizeForWindowAndBorder(int kernelSize)
    {
        var image = new double[kernelSize, kernelSize];
        var kernel = new double[kernelSize, kernelSize];
        int center = kernelSize / 2;
        image[center, center] = 1;
        kernel[center, center] = 2;
        var expected = new double[kernelSize, kernelSize];
        expected[center, center] = Math.Sqrt(8);

        Assert.That(SobelFilterTask.SobelFilter(image, kernel), Is.EqualTo(expected));
    }
}
