using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3
{
    public class Dock
    {
        public string Id { get; set; }
        public Queue<Truck> Line { get; set; } = new Queue<Truck>();
        public double TotalSales { get; set; }
        public int TotalCrates { get; set; }
        public int TotalTrucks { get; set; }
        public int TimeInUse { get; set; }
        public int TimeNotInUse { get; set; }


        /// <summary>
        /// Ques trucks
        /// </summary>
        /// <param name="truck"></param>
        public void JoinLine(Truck truck)
        {
            Line.Enqueue(truck);
        }
        /// <summary>
        /// Deques trucks
        /// </summary>
        /// <returns>Line.Dequeue</returns>
        public Truck SendOff()
        {
            return Line.Dequeue();
        }
    }
}
