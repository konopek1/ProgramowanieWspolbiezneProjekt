using System.Drawing;
using System.Threading;

namespace WindowsFormsApp1.com.WorkShops
{
    public class WorkShopDiagnostyk : WorkShop
    {
        public WorkShopDiagnostyk(Damage type, Worker worker, Painter painter, Point pos) : base(type, worker, painter,
            pos)
        {
        }

        public override void CallNMethod()
        {
            int time = r.Next(1000, 5000);
            int operation = r.Next(1, 3);
            Thread.Sleep(time);

            switch (operation)
            {
                case 1:
                    finishReparingCar();
                    break;
                case 2:
                    sendCarToWorkshop();
                    break;
            }
            
        }

        private void sendCarToWorkshop()
        {
                decCounter();
                
                var a = new Animation(pos,Board.prioQue.pos,new Size(100,100));
                _painter.AddAnimation(a);
                getCarFromQueue();
                WaitUntilAnimationDone(a);

                car.damageType = Damage.MECHANICZNE;
                _worker.AssignBackToWorkShopQue(car);
                
        }


}
}