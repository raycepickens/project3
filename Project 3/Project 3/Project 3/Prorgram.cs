using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3
{
    // Program.cs
    class Program
    {
        static void Main()
        {
            Warehouse warehouse = new Warehouse();

            // Add a random number of docks to the warehouse (between 1 and 15)
            Random rand = new Random();
            int numberOfDocks = rand.Next(1, 16);

            for (int i = 1; i <= numberOfDocks; i++)
            {
                warehouse.Docks.Add(new Dock { Id = $"Dock{i}" });
            }

            warehouse.Run();

            // Print summary or additional reports based on the simulation results
            Console.WriteLine("Simulation completed.");
        }
    }
}