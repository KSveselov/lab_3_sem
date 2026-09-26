namespace Mazes;

public static class SnakeMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        
        for( int row = 0; row <= (height - 3) / 2; row++)
        {
            if(robot.X == 1)
                MoveRight(robot, width);
            else
                MoveLeft(robot, width);
            
            if(row < (height - 3) / 2)
                MoveDown(robot, 5);
        }
    }

    public static void MoveDown(Robot robot, int height)
	{
		for(int i = 0; i < height-3; i++)
		{
			robot.MoveTo(Direction.Down);
		}
	}

	public static void MoveRight(Robot robot, int width)
	{
		for(int i = 0; i < width-3; i++)
		{
			robot.MoveTo(Direction.Right);
		}
	}

    public static void MoveLeft(Robot robot, int width)
	{
		for(int i = 0; i < width-3; i++)
		{
			robot.MoveTo(Direction.Left);
		}
	}

}