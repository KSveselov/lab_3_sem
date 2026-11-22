using System;
using System.Globalization;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media;

namespace Manipulation;

public static class VisualizerTask
{
	public static double X = 220;
	public static double Y = -100;
	public static double Alpha = 0.05;
	public static double Wrist = 2 * Math.PI / 3;
	public static double Elbow = 3 * Math.PI / 4;
	public static double Shoulder = Math.PI / 2;
	public static double moveSpeed = 0.1;

	public static Brush UnreachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 255, 230, 230));
	public static Brush ReachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 230, 255, 230));
	public static Pen ManipulatorPen = new Pen(Brushes.Black, 3);
	public static Brush JointBrush = new SolidColorBrush(Colors.Gray);

	public static void KeyDown(Visual visual, KeyEventArgs key)
	{
		// TODO: Добавьте реакцию на QAWS и пересчитывать Wrist
		switch (key.Key)
		{
			case Key.Q:
				Shoulder += moveSpeed;
				break;
			case Key.A:
				Shoulder -= moveSpeed;
				break;
			case Key.W:
				Elbow += moveSpeed;
				break;
			case Key.S:
				Elbow -= moveSpeed;
				break;
			
		}
		Wrist = -Alpha - Shoulder - Elbow;
		visual.InvalidateVisual(); // вызывает перерисовку канваса
	}

	public static void MouseMove(Visual visual, PointerEventArgs e)
	{
		var mousePosition = e.GetPosition(visual);
		var shoulderPos = GetShoulderPos(visual);
		var logicalPosition = ConvertWindowToMath(mousePosition, shoulderPos);
		X = logicalPosition.X;
		Y = logicalPosition.Y;

		UpdateManipulator();
		visual.InvalidateVisual();
	}

	public static void MouseWheel(Visual visual, PointerWheelEventArgs e)
	{
		Alpha += e.Delta.Y * moveSpeed;

		UpdateManipulator();
		visual.InvalidateVisual();
	}

	public static void UpdateManipulator()
	{
		var angles = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
		if (double.IsNaN(angles[0]) || double.IsNaN(angles[1]) || double.IsNaN(angles[2]))
			return;

		(Shoulder, Elbow, Wrist) = (angles[0], angles[1], angles[2]);
	}

	public static void DrawManipulator(DrawingContext context, Point shoulderPos)
	{
		var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

		DrawReachableZone(context, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);

		var formattedText = new FormattedText(
			$"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}",
			CultureInfo.InvariantCulture,
			FlowDirection.LeftToRight,
			Typeface.Default,
			18,
			Brushes.DarkRed
		)
		{
			TextAlignment = TextAlignment.Center
		};
		context.DrawText(formattedText, new Point(10, 10));

		var elbowPos = ConvertMathToWindow(joints[0], shoulderPos);
		var wristPos = ConvertMathToWindow(joints[1], shoulderPos);
		var palmEndPos = ConvertMathToWindow(joints[2], shoulderPos);

		context.DrawLine(ManipulatorPen, shoulderPos, elbowPos);
		context.DrawLine(ManipulatorPen, elbowPos, wristPos);
		context.DrawLine(ManipulatorPen, wristPos, palmEndPos);

		const double jointRadius = 5;
		context.DrawEllipse(JointBrush, null, shoulderPos, jointRadius, jointRadius);
		context.DrawEllipse(JointBrush, null, elbowPos, jointRadius, jointRadius);
		context.DrawEllipse(JointBrush, null, wristPos, jointRadius, jointRadius);
	}

	private static void DrawReachableZone(
		DrawingContext context,
		Brush reachableBrush,
		Brush unreachableBrush,
		Point shoulderPos,
		Point[] joints)
	{
		var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
		var rmax = Manipulator.UpperArm + Manipulator.Forearm;
		var mathCenter = new Point(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
		var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
		context.DrawEllipse(reachableBrush,
			null,
			new Point(windowCenter.X, windowCenter.Y),
			rmax, rmax);
		context.DrawEllipse(unreachableBrush,
			null,
			new Point(windowCenter.X, windowCenter.Y),
			rmin, rmin);
	}

	public static Point GetShoulderPos(Visual visual)
	{
		return new Point(visual.Bounds.Width / 2, visual.Bounds.Height / 2);
	}

	public static Point ConvertMathToWindow(Point mathPoint, Point shoulderPos)
	{
		return new Point(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
	}

	public static Point ConvertWindowToMath(Point windowPoint, Point shoulderPos)
	{
		return new Point(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
	}
}