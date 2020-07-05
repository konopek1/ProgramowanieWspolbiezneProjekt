using System.Collections.Generic;

namespace WindowsFormsApp1.com.Quues
{
    class DoubleQueue<T> 
    {
        public Queue<T> queue;
        public Queue<T> prioQueue;
        
        public DoubleQueue()
        {
            this.queue = new Queue<T>();
            this.prioQueue = new Queue<T>();
        }
    }
}
