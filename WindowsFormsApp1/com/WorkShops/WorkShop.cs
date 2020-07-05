using System;
using System.Drawing;
using System.Threading;

namespace WindowsFormsApp1.com.WorkShops
{
    public class WorkShop
    {
        protected Thread t;
        public Damage type;
        protected Worker _worker;
        protected Painter _painter;
        protected Point pos;
        protected Car car;
        protected Random r;
        protected int counter;

        public WorkShop(Damage type, Worker worker, Painter painter, Point pos)
        {
            this.t = new Thread(this.Threaded);
            this.type = type;
            this.pos = pos;
            this._worker = worker;
            this._painter = painter;
            this.counter = 0;
            
            r = new Random(pos.Y);
            
            _painter.AddString(pos,this.counter.ToString());
        }

        protected void finishReparingCar()
        {
            decCounter();   
            var a = new Animation(pos, Board.finish.pos, new Size(100, 100));
            _painter.AddAnimation(a);
            getCarFromQueue();
        }

        protected void incCounter()
        {
            _painter.UpadateString(pos,(++counter).ToString());
        }

        protected void decCounter()
        {
            _painter.UpadateString(pos,(--counter).ToString());
        }
        
        protected void getCarFromQueue()
        {
            bool isPrio;
            car = _worker.GetCar(type,out isPrio);
            StaticObject<Size> from = isPrio ? Board.prioQue : Board.que;
            
            var a = new Animation(from.pos, pos, new Size(100, 100));
            _painter.AddAnimation(a);
            WaitUntilAnimationDone(a);
            
            incCounter();
        }

        public virtual void CallNMethod()
        {
            int time = r.Next(1000, 5000);
            Thread.Sleep(time);
            
            finishReparingCar();
        }

        protected void WaitUntilAnimationDone(Animation animation)
        {
            while (true)
            {
                if(animation.isDone()) break;
            }
        }

        public void Run()
        {
            t.Start();
        }

        public virtual void Threaded()
        {
            getCarFromQueue();
            while (true)
            {
                CallNMethod();
            }
        }
    }
}