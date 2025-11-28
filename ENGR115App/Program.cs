//Name: Rachel Sanchez
//Course: ENGR 115
//Assignment: Aerobatic Maneuver Calculations (Checkpoint 4)
//Description: Simple version of the program that calculates load factor 
//             and top velocity for aerobatic loops.

using System;

class Program
{
    static void Main()
    {
        bool runAgain = true;

        while (runAgain)
        {
            Console.Clear();
            Console.WriteLine("Hello! Let's do some math!");

            double gravity = 32.2;

            Console.Write("How many maneuvers would you like to calculate? ");
            int total = int.Parse(Console.ReadLine());

            Console.WriteLine("\nResults:");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Entry Speed | Radius | Circumference | Load Factor | Top Speed");
            Console.WriteLine("---------------------------------------------------------------");

            for (int i = 1; i <= total; i++)
            {
                Console.WriteLine($"\n--- Maneuver {i} ---");

                Console.Write("Enter entry speed (mi/hr): ");
                double entrySpeedMiHr = double.Parse(Console.ReadLine());

                Console.Write("Enter loop radius (ft): ");
                double radius = double.Parse(Console.ReadLine());

                // Convert speed to ft/sec
                double entrySpeedFtSec = entrySpeedMiHr * 1.467;

                // Circumference
                double circumference = 2 * Math.PI * radius;

                // Load factor
                double loadFactor = (entrySpeedFtSec * entrySpeedFtSec) / (gravity * radius) + 1;

                // Top velocity
                double underSqrt = (entrySpeedFtSec * entrySpeedFtSec) - (4 * gravity * radius);
                double topVelFtSec = Math.Sqrt(underSqrt);
                double topVelMiHr = topVelFtSec / 1.467;

                Console.WriteLine($"Speed: {entrySpeedMiHr:F2} | Radius: {radius:F2} | Circ: {circumference:F2} | Load: {loadFactor:F2} | Top: {topVelMiHr:F2}");
            }

            Console.Write("\nWould you like to run the program again? (yes/no): ");
            string answer = Console.ReadLine().ToLower();

            if (answer != "yes" && answer != "y")
            {
                runAgain = false;
                Console.WriteLine("\nThank you for using the math program!");
            }
        }
    }
}