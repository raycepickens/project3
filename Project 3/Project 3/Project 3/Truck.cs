using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 //////////////
 William Pickens
 Project 3
 Data Structures
 Due 11/19/23
 A Simulation of a Warehouse
 ///////////////
 */
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
