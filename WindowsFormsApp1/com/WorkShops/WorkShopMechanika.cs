using System.CodeDom;
using System.Drawing;
using System.Threading;

namespace WindowsFormsApp1.com.WorkShops
{
    public class WorkShopMechanika : WorkShop
    {
        private WorkShopParking _parking;

        public WorkShopMechanika(Damage type, Worker worker, Painter painter, WorkShopParking parking, Point pos) :
            base(type, worker, painter,
                pos)
        {
            _parking = parking;
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
                    sendToParking();
                    break;
            }
        }

        private void sendToParking()
        {
            var a = new Animation(pos, Board.parking.pos, new Size(100, 100));
            _painter.AddAnimation(a);
            decCounter();
            getCarFromQueue();
            _parking.Add(car);
            WaitUntilAnimationDone(a);
        }
    }
}