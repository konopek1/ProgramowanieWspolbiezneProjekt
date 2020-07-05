using System;
using System.Collections.Generic;
using System.Drawing;

namespace WindowsFormsApp1.com
{
    public class Board
    {
        public static StaticObject<Size> que = new StaticObject<Size>(new Point(100,350),new Size(200,100));
        public static StaticObject<Size> parking = new StaticObject<Size>(new Point(100,100),new Size(200,200));
        public static StaticObject<Size> finish = new StaticObject<Size>(new Point(1400,350),new Size(10,100));
        public static StaticObject<Size> prioQue = new StaticObject<Size>(new Point(100,500),new Size(200,100));
        
        public static readonly List<StaticObject<Size>> workshops = new List<StaticObject<Size>>()
        {
            new StaticObject<Size>(new Point(800, 100), new Size(250, 100)),
            new StaticObject<Size>(new Point(800, 350), new Size(250, 100)),
            new StaticObject<Size>(new Point(800, 500), new Size(250, 100)),
            new StaticObject<Size>(new Point(800, 650), new Size(250, 100)),
        };
        
        public static readonly List<StaticObject<string>> staticStrings = new List<StaticObject<string>>()
        {
            new StaticObject<string>(new Point(que.pos.X,que.pos.Y + que.value.Height - 30), "Główna kolejka" ),
            new StaticObject<string>(new Point(parking.pos.X,parking.pos.Y + parking.value.Height - 30), "Parking" ),
            new StaticObject<string>(new Point(prioQue.pos.X,prioQue.pos.Y + prioQue.value.Height - 30), "Priorytetowa Kolejka"),
            new StaticObject<string>(new Point(workshops[0].pos.X,workshops[0].pos.Y + workshops[0].value.Height - 30), "Warsztat lakierniczy"),
            new StaticObject<string>(new Point(workshops[1].pos.X,workshops[1].pos.Y + workshops[1].value.Height - 30), "Warsztat mechaniczny"),
            new StaticObject<string>(new Point(workshops[2].pos.X,workshops[2].pos.Y + workshops[2].value.Height - 30), "Warsztat diagnostyczny"),
            new StaticObject<string>(new Point(workshops[3].pos.X,workshops[3].pos.Y + workshops[3].value.Height - 30), "Warsztat mechaniczny"),
        };


    }

    public struct StaticObject<T>
    {
        public Point pos { get; }
        public T  value { get; }

        public StaticObject(Point p, T s)
        {
            pos = p;
            value = s;
        }
    }
}