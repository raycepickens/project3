/*
 //////////////
 William Pickens
 Project 3
 Data Structures
 Due 11/19/23
 A Simulation of a Warehouse
 ///////////////
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3
{
    // Warehouse.cs
    public class Warehouse
    {
        public List<Dock> Docks { get; set; } = new List<Dock>();
        public Queue<Truck> Entrance { get; set; } = new Queue<Truck>();


        /// <summary>
        /// Calls all methods to run simulation
        /// </summary>
        private string csvFilePath = @"C:\Users\willi\OneDrive\Desktop\Project 3\simulation_log.csv";

        public void Run()
        {
            using (StreamWriter writer = new StreamWriter(csvFilePath))
            {
                // Write CSV header
                writer.WriteLine("Time Increment,Driver,Company,CrateId,Value,Scenario");

                // Simple simulation logic for demonstration purposes
                for (int timeIncrement = 0; timeIncrement < 48; timeIncrement++)
                {
                    // Simulate truck arrivals
                    if (timeIncrement % 3 == 0)
                    {
                        Truck newTruck = GenerateRandomTruck();
                        Entrance.Enqueue(newTruck);
                        Console.WriteLine($"Time: {timeIncrement}, Truck Arrived - Driver: {newTruck.Driver}, Company: {newTruck.DeliveryCompany}");
                    }

                    // Simulate dock assignments
                    foreach (var dock in Docks)
                    {
                        if (dock.Line.Count > 0 && dock.TimeNotInUse == 0)
                        {
                            dock.TimeInUse++;
                            dock.TotalTrucks++;
                            var currentTruck = dock.SendOff();
                            dock.TotalCrates += currentTruck.Trailer.Count;

                            foreach (var crate in currentTruck.Trailer)
                            {
                                dock.TotalSales += crate.Price;
                                string scenario = currentTruck.Trailer.Count > 1
                                    ? "More Crates to Unload"
                                    : dock.Line.Count > 0
                                        ? "Next Truck Already in Dock"
                                        : "Next Truck Not in Dock";
                                writer.WriteLine($"{timeIncrement},{currentTruck.Driver},{currentTruck.DeliveryCompany},{crate.Id},{crate.Price},{scenario}");
                            }

                            dock.TimeNotInUse = 3; // Simulate 3 time increments of non-use after unloading
                        }
                        else
                        {
                            dock.TimeNotInUse = Math.Max(0, dock.TimeNotInUse - 1);
                        }
                    }

                    // Simulate trucks entering the line
                    foreach (var truck in Entrance.ToList())
                    {
                        var availableDock = Docks.FirstOrDefault(d => d.TimeNotInUse == 0);
                        if (availableDock != null)
                        {
                            Entrance.Dequeue();
                            availableDock.JoinLine(truck);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            // Simulation is complete, generate the report
            GenerateReport();
        }




        /// <summary>
        /// Generates simulaiton report
        /// </summary>
        private void GenerateReport()
        {
            Console.WriteLine("Simulation Report:");

            // Track statistics
            int totalTrucksProcessed = 0;
            int totalCratesUnloaded = 0;
            double totalValueOfCrates = 0;
            double totalOperatingCost = 0;
            double totalRevenue = 0;
            int totalDockTimeInUse = 0;
            int totalDockTimeNotInUse = 0;
            int totalDocksOpen = 0;
            int longestLine = 0;

            foreach (var dock in Docks)
            {
                totalTrucksProcessed += dock.TotalTrucks;
                totalCratesUnloaded += dock.TotalCrates;
                totalValueOfCrates += dock.TotalSales;
                totalDockTimeInUse += dock.TimeInUse;
                totalDockTimeNotInUse += dock.TimeNotInUse;

                if (dock.TimeInUse > 0)
                {
                    totalDocksOpen++;
                }

                if (dock.Line.Count > longestLine)
                {
                    longestLine = dock.Line.Count;
                }

                totalOperatingCost += dock.TimeInUse * 100; // Assuming $100 operating cost per time increment
            }

            totalRevenue = totalValueOfCrates - totalOperatingCost;

            // Calculate averages
            double averageValueOfCrate = totalValueOfCrates / totalCratesUnloaded;
            double averageValueOfTruck = totalValueOfCrates / totalTrucksProcessed;
            double averageDockTimeInUse = (double)totalDockTimeInUse / Docks.Count;

            // Print the report
            Console.WriteLine($"Number of docks open during the simulation: {totalDocksOpen}");
            Console.WriteLine($"Longest line at any loading dock: {longestLine}");
            Console.WriteLine($"Total number of trucks processed: {totalTrucksProcessed}");
            Console.WriteLine($"Total number of crates unloaded: {totalCratesUnloaded}");
            Console.WriteLine($"Total value of crates unloaded: {totalValueOfCrates}");
            Console.WriteLine($"Average value of each crate unloaded: {averageValueOfCrate}");
            Console.WriteLine($"Average value of each truck unloaded: {averageValueOfTruck}");
            Console.WriteLine($"Total amount of time that a dock was in use: {totalDockTimeInUse}");
            Console.WriteLine($"Total amount of time that a dock was not in use: {totalDockTimeNotInUse}");
            Console.WriteLine($"Average amount of time that a dock was in use: {averageDockTimeInUse}");
            Console.WriteLine($"Total cost of operating each dock: {totalOperatingCost}");
            Console.WriteLine($"Total revenue of the warehouse: {totalRevenue}");
        }

        /// <summary>
        /// Generates Trucks
        /// </summary>
        
        private Truck GenerateRandomTruck()
        {
            Random rand = new Random();
            return new Truck
            {
                Driver = $"Driver_{rand.Next(1, 100)}",
                DeliveryCompany = $"Company_{rand.Next(1, 10)}",
                Trailer = GenerateRandomCrates(rand.Next(1, 5))
            };
        }
        /// <summary>
        /// Generates Crates
        /// </summary>
        /// <param name="count"></param>
        
        private Stack<Crate> GenerateRandomCrates(int count)
        {
            Random rand = new Random();
            var crates = new Stack<Crate>();
            for (int i = 0; i < count; i++)
            {
                crates.Push(new Crate
                {
                    Id = Guid.NewGuid().ToString(),
                    Price = rand.Next(50, 500)
                });
            }
            return crates;
        }
    }

}