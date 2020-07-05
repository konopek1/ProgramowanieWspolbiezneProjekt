using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace WindowsFormsApp1.com
{
    public class Animation
    {
        public Size target;
        public Point pos { get; set; }
        public Point endPos { get; set; }
        private int diffX;
        private int diffY;
        public Brush brush { get; }
        private Random r;

        public Animation(Point pos, Point endPos, Size target)
        {
            this.pos = pos;
            this.endPos = endPos;
            this.target = target;
            this.diffX = endPos.X < pos.X ? -2 : 2;
            this.diffY = endPos.Y < pos.Y ? -2 : 2;
            r = new Random(endPos.Y + pos.Y);
            this.brush = getRandomBrush();
        }
        
        Brush getRandomBrush()
        {
            var color = new Color();
            color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            return  new SolidBrush(color);
        }

        public void Update()
        {
            pos = transform(pos);
        }

        public int getElapsedTime()
        {
            int xDistance = Math.Abs(pos.X - endPos.X);
            int yDistance = Math.Abs(pos.Y - endPos.Y);
            int distance = Math.Max(xDistance,yDistance);

            return Math.Abs((distance * 1 /2 + 50) * 16);
        }

        public Point transform(Point p)
        {
            if (p.Y == endPos.Y) diffY = 0;
            if (p.X == endPos.X) diffX = 0;
            return new Point( (int) (p.X + diffX), (int) (p.Y + diffY));
        }

        public bool isDone()
        {
            return Math.Abs(pos.X - endPos.X) < 2 && Math.Abs(pos.Y - endPos.Y) < 2;
        }
    }
}