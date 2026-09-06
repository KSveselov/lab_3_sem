using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    class Drawer
    {
        static float x, y;
        static IGraphics graphics;

        public static void Initialization ( IGraphics newGraphics )
        {
            graphics = newGraphics;
            graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        {x = x0; y = y0;}

        public static void MakeIt(Pen pen, double list, double angle)
        {
        //Делает шаг длиной list в направлении angle и рисует пройденную траекторию
        var x1 = (float)(x + list * Math.Cos(angle));
        var y1 = (float)(y + list * Math.Sin(angle));
        graphics.DrawLine(pen, x, y, x1, y1);
        x = x1;
        y = y1;
        }

        public static void Change(double list, double angle)
        {
            x = (float)(x + list * Math.Cos(angle)); 
           y = (float)(y + list * Math.Sin(angle));
           }
    }
    
    public class ImpossibleSquare
{
    public static void Draw(int width, int hight, double turnAngle, IGraphics graphics)
    {
        Drawer.Initialization(graphics);

        var sz = Math.Min(width, hight);

        var diagonalLength = Math.Sqrt(2) * (sz * 0.375f + sz * 0.04f) / 2;
        var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
        var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + hight / 2f;

        Drawer.SetPosition(x0, y0);
        //Рисуем 1-ую сторону
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f, 0);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.04f * Math.Sqrt(2), Math.PI / 4);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f, Math.PI);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f - sz * 0.04f, Math.PI / 2);

        Drawer.Change(sz * 0.04f, -Math.PI);
        Drawer.Change(sz * 0.04f * Math.Sqrt(2), 3 * Math.PI / 4);

        //Рисуем 2-ую сторону
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f, -Math.PI / 2);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.04f * Math.Sqrt(2), -Math.PI / 2 + Math.PI / 4);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f, -Math.PI / 2 + Math.PI);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f - sz * 0.04f, -Math.PI / 2 + Math.PI / 2);

        Drawer.Change(sz * 0.04f, -Math.PI / 2 - Math.PI);
        Drawer.Change(sz * 0.04f * Math.Sqrt(2), -Math.PI / 2 + 3 * Math.PI / 4);

        //Рисуем 3-ю сторону
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f, Math.PI);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.04f * Math.Sqrt(2), Math.PI + Math.PI / 4);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f, Math.PI + Math.PI);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f - sz * 0.04f, Math.PI + Math.PI / 2);

        Drawer.Change(sz * 0.04f, Math.PI - Math.PI);
        Drawer.Change(sz * 0.04f * Math.Sqrt(2), Math.PI + 3 * Math.PI / 4);

        //Рисуем 4-ую сторону
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f, Math.PI / 2);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.04f * Math.Sqrt(2), Math.PI / 2 + Math.PI / 4);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f, Math.PI / 2 + Math.PI);
        Drawer.MakeIt(new Pen(Brushes.Yellow), sz * 0.375f - sz * 0.04f, Math.PI / 2 + Math.PI / 2);

        Drawer.Change(sz * 0.04f, Math.PI / 2 - Math.PI);
        Drawer.Change(sz * 0.04f * Math.Sqrt(2), Math.PI / 2 + 3 * Math.PI / 4);
    }
}
}