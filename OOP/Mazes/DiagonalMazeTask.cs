namespace Mazes;

public static class DiagonalMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        if (width > height) 
            MoveRightFirst(robot, width, height);
        else 
            MoveDownFirst(robot, width, height);
    }

    private static void MoveRightFirst(Robot robot, int width, int height)
    {
        for (var j = 0; j < height - 2; j++)
        {
            Move(robot, Direction.Right, (width - 3) / (height - 2));
            if (j < height - 3) 
                robot.MoveTo(Direction.Down);
        }
    }

    private static void MoveDownFirst(Robot robot, int width, int height)
    {
        for (var i = 0; i < width - 2; i++)
        {
            Move(robot, Direction.Down, (height - 3) / (width - 2));
            if (i < width - 3) 
                robot.MoveTo(Direction.Right);
        }
    }

    private static void Move(Robot robot, Direction direction, int step)
    {
        for (var i = 0; i < step; i++)
            robot.MoveTo(direction);
    }
}