using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1.com.Quues
{
    class WorkShopQueues<T>
    {
        private List<DoubleQueue<T>> workShopQueues;

        public WorkShopQueues(uint howMany = 1)
        {
            this.workShopQueues = new List<DoubleQueue<T>>();
            this.FeedWorkShopQueues(howMany);
        }

        private void FeedWorkShopQueues(uint howMany)
        {
            for (uint i = 0; i < howMany; i++)
                this.workShopQueues.Add(new DoubleQueue<T>());
        }

        /**
         * Insert to the smallest normal queue
         */
        public void Insert(T element)
        {
            DoubleQueue<T> smallest = null;

            foreach (var que in workShopQueues)
            {
                if (smallest == null) smallest = que;
                if (smallest.queue.Count() < que.queue.Count())
                    smallest = que;
            }

            smallest.queue.Enqueue(element);
        }

        /**
         * Insert to the smallest prio queue
         */
        public void InsertPrio(T element)
        {
            DoubleQueue<T> smallest = null;

            foreach (var que in workShopQueues)
            {
                if (smallest == null) smallest = que;
                if (smallest.prioQueue.Count() < que.prioQueue.Count())
                    smallest = que;
            }

            smallest.prioQueue.Enqueue(element);
        }

        /**
         * Tries to find the biggest queues where prio ques are more importatn then normal
         */
        public T GetElement(out bool isPrio)
        {
            DoubleQueue<T> biggest = null;
            isPrio = false;
            foreach (var que in workShopQueues)
            {
                if (biggest == null) biggest = que;
                if (que.prioQueue.Any() || biggest.prioQueue.Any())
                {
                    isPrio = true;
                    if (que.prioQueue.Count() > biggest.prioQueue.Count())
                        biggest = que;
                }
                else
                {
                    if (que.queue.Count() > biggest.queue.Count())
                        biggest = que;
                }
            }


            if (biggest.prioQueue.Any()) return biggest.prioQueue.Dequeue();
            return biggest.queue.Dequeue();
        }
    }
}