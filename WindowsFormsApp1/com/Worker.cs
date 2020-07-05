using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.com.Quues;

namespace WindowsFormsApp1.com
{
    using Queues = Dictionary<Damage, WorkShopQueues<Car>>;

    public class Worker
    {
        Queues queues = new Queues();
        public int prioQueCounter { get; set; }
        public int queCounter { get; set; }

        public Worker()
        {
            queues.Add(Damage.MECHANICZNE, new WorkShopQueues<Car>(2));
            queues.Add(Damage.DIAGNOSTYKA, new WorkShopQueues<Car>());
            queues.Add(Damage.LAKIER, new WorkShopQueues<Car>());
            for (int i = 0; i < 100; i++)
            {
                queCounter++;
                AssignToWorkShopQue(new Car(Damage.LAKIER));
                AssignToWorkShopQue(new Car(Damage.DIAGNOSTYKA));
                AssignToWorkShopQue(new Car(Damage.MECHANICZNE));
            }
        }

        public void AssignToWorkShopQue(Car car)
        {
            Monitor.Enter(this);
            {
                var workShopQueue = this.queues[car.damageType];
                workShopQueue.Insert(car);
                queCounter++;
            }
            Monitor.Exit(this);
        }

        public void AssignBackToWorkShopQue(Car car)
        {
            Monitor.Enter(this);
            {
                var workShopQueue = this.queues[car.damageType];
                workShopQueue.InsertPrio(car);
                prioQueCounter++;
            }
            Monitor.Exit(this);
        }

        public Car GetCar(Damage type, out bool isPrio)
        {
            Monitor.Enter(this);
            Car car = this.queues[type].GetElement(out isPrio);
            if (isPrio) prioQueCounter--;
            else queCounter--;
            Monitor.Exit(this);
            return car;
        }
        
        
    }
}