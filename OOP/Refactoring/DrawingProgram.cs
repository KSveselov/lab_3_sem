using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    public class Drawer
    {
        static float x, y;
        static IGraphics graphics;

        public static void Initialization ( IGraphics newGraphics )
        {
            graphics = newGraphics;
            graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0; 
            y = y0;
        }

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
            
            DrawSide(0, sz);
            DrawSide(-Math.PI / 2, sz);
            DrawSide(Math.PI, sz);
            DrawSide(Math.PI / 2, sz);
        }

        private static void DrawSide(double baseAngle, float sz)
        {
            float mainLength = sz * 0.375f;          // Длина основной части
            float cathetOffset = sz * 0.04f;         // Длина катета при скосе
            float diagonalOffset = cathetOffset * (float)Math.Sqrt(2); // Длина диагонального отрезка (скоса)

            // Рисуем первый отрезок (основной)
            Drawer.MakeIt(new Pen(Brushes.Yellow), mainLength, baseAngle);

            // Второй отрезок (скос под 45°)
            Drawer.MakeIt(new Pen(Brushes.Yellow), diagonalOffset, baseAngle + Math.PI / 4);

            // Третий отрезок (основной, разворот на 180°)
            Drawer.MakeIt(new Pen(Brushes.Yellow), mainLength, baseAngle + Math.PI);

            // Четвёртый отрезок (укороченный, повёрнут на 90°)
            Drawer.MakeIt(new Pen(Brushes.Yellow), mainLength - cathetOffset, baseAngle + Math.PI / 2);

            // Перемещение (завершение скоса)
            Drawer.Change(cathetOffset, baseAngle - Math.PI);
            Drawer.Change(diagonalOffset, baseAngle + 3 * Math.PI / 4);
        }
    }
}