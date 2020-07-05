using System.Drawing;
using System.Threading;

namespace WindowsFormsApp1.com.WorkShops
{
    public class WorkShopParking : WorkShop
    {
        public WorkShopParking(Damage type, Worker worker, Painter painter, Point pos) : base(type, worker, painter, pos)
        {
        }

        public override void Threaded()
        {
            while (true)
            {
                
            }
        }

        private void onRecivedPart()
        {
            _worker.AssignBackToWorkShopQue(this.car);
            
            var a = new Animation(pos, Board.prioQue.pos, new Size(100,100));
            _painter.AddAnimation(a);
            decCounter();
            
            WaitUntilAnimationDone(a);
            
        }

        public void Add(Car car)
        {
            Monitor.Enter(this);
            
            incCounter();
            this.car = car;
            
            Monitor.Exit(this);

            
            int time = r.Next(5000, 15000);
            Thread.Sleep(time);
            
            onRecivedPart();
            
        }
    }
}