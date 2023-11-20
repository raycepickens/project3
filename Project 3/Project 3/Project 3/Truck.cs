using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3
{
    public class Truck
    {
        public string Driver { get; set; }
        public string DeliveryCompany { get; set; }
        public Stack<Crate> Trailer { get; set; } = new Stack<Crate>();

        /// <summary>
        /// Loads crate
        /// </summary>
        /// <param name="crate"></param>
        public void Load(Crate crate)
        {
            Trailer.Push(crate);
        }

        /// <summary>
        /// Unloads crates
        /// </summary>
        /// <returns>Trailer</returns>
        public Crate Unload()
        {
            return Trailer.Pop();
        }
    }
}
