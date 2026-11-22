using System;
using Avalonia;
using NUnit.Framework;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class AnglesToCoordinatesTask
{
	/// <summary>
	/// По значению углов суставов возвращает массив координат суставов
	/// в порядке new []{elbow, wrist, palmEnd}
	/// </summary>
	public static Point[] GetJointPositions(double shoulder, double elbow, double wrist)
	{
		var forearmAngle = shoulder + elbow - Math.PI;
		var palmAngle = forearmAngle + wrist - Math.PI;

		var elbowPos = new Point(UpperArm * Math.Cos(shoulder), UpperArm * Math.Sin(shoulder));
		var wristPos = new Point(
			elbowPos.X + Forearm * Math.Cos(forearmAngle),
			elbowPos.Y + Forearm * Math.Sin(forearmAngle));
		var palmEndPos = new Point(
			wristPos.X + Palm * Math.Cos(palmAngle),
			wristPos.Y + Palm * Math.Sin(palmAngle));

		return new[]
		{
			elbowPos,
			wristPos,
			palmEndPos
		};
	}
}

[TestFixture]
public class AnglesToCoordinatesTask_Tests
{
	[TestCase(0, Math.PI, Math.PI, UpperArm, 0, UpperArm + Forearm, 0, UpperArm + Forearm + Palm, 0)]
	[TestCase(Math.PI / 2, Math.PI / 2, Math.PI, 0, UpperArm, Forearm, UpperArm, Forearm + Palm, UpperArm)]
	[TestCase(0, Math.PI / 2, Math.PI / 2, UpperArm, 0, UpperArm, -Forearm, UpperArm - Palm, -Forearm)]
	[TestCase(Math.PI, 0, 0, -UpperArm, 0, Forearm - UpperArm, 0, Forearm - UpperArm - Palm, 0)]
	[TestCase(0, 3 * Math.PI / 2, 3 * Math.PI / 2, UpperArm, 0, UpperArm, Forearm, UpperArm - Palm, Forearm)]
	public void TestGetJointPositions(
		double shoulder, double elbow, double wrist,
		double elbowX, double elbowY, double wristX, double wristY, double palmEndX, double palmEndY)
	{
		var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
		Assert.That(joints, Has.Length.EqualTo(3));
		Assert.That(joints[0].X, Is.EqualTo(elbowX).Within(1e-5), "elbow X");
		Assert.That(joints[0].Y, Is.EqualTo(elbowY).Within(1e-5), "elbow Y");
		Assert.That(joints[1].X, Is.EqualTo(wristX).Within(1e-5), "wrist X");
		Assert.That(joints[1].Y, Is.EqualTo(wristY).Within(1e-5), "wrist Y");
		Assert.That(joints[2].X, Is.EqualTo(palmEndX).Within(1e-5), "palm endX");
		Assert.That(joints[2].Y, Is.EqualTo(palmEndY).Within(1e-5), "palm endY");
	}
}
