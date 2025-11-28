//Name: Rachel Sanchez
//Course: ENGR 115
//Assignment: Aerobatic Maneuver Calculations (Checkpoint 4)
//Description: This program calculates the load factor and velocity at the top of a loop
//             for multiple aerobatic maneuvers. This version uses arrays and methods for
//             cleaner input validation and repeated calculations.
//Key features for this module: Arrays, methods, input validation.

using System;

class Program
{
    // ------------------- INPUT VALIDATION METHODS -------------------

    // Method to get a valid integer
    static int GetIntInput(string message)
    {
        int value;
        Console.Write(message);
        while (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive whole number.");
            Console.Write(message);
        }
        return value;
    }

    // Method to get a valid double
    static double GetDoubleInput(string message)
    {
        double value;
        Console.Write(message);
        while (!double.TryParse(Console.ReadLine(), out value) || value <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive numeric value.");
            Console.Write(message);
        }
        return value;
    }

    // ------------------- CALCULATION METHOD -------------------

    static double CalculateTopVelocity(double entrySpeedFtPerSec, double radius, double gravity)
    {
        double underSqrt = (entrySpeedFtPerSec * entrySpeedFtPerSec) - (4.0 * gravity * radius);
        return Math.Sqrt(underSqrt);
    }

    // ------------------- PRINT METHOD -------------------

    static void PrintResults(
        double[] speeds,
        double[] radii,
        double[] circumferences,
        double[] loadFactors,
        double[] topSpeeds)
    {
        Console.WriteLine("\nResults:");
        Console.WriteLine(new string('-', 125));
        Console.WriteLine("  Entry Speed (mi/hr)   Radius (ft)   Loop Circumference (ft)   Load Factor (g)   Top Velocity (mi/hr)");
        Console.WriteLine(new string('-', 125));

        for (int i = 0; i < speeds.Length; i++)
        {
            Console.WriteLine($"{speeds[i],18:F2}{radii[i],14:F2}{circumferences[i],28:F2}{loadFactors[i],18:F2}{topSpeeds[i],20:F2}");
        }

        Console.WriteLine(new string('-', 125));
    }

    // ------------------- MAIN PROGRAM -------------------

    static void Main()
    {
        bool runAgain = true;

        while (runAgain)
        {
            Console.Clear();
            Console.WriteLine("Hello! Let's do some math!");

            const double gravity = 32.2;

            // Ask how many maneuvers to calculate
            int numberOfManeuvers = GetIntInput("Enter the number of aerobatic maneuvers to calculate: ");
            Console.WriteLine($"You chose {numberOfManeuvers} maneuvers.\n");

            // Arrays to store values
            double[] entrySpeeds = new double[numberOfManeuvers];
            double[] radii = new double[numberOfManeuvers];
            double[] circumferences = new double[numberOfManeuvers];
            double[] loadFactors = new double[numberOfManeuvers];
            double[] topVelocities = new double[numberOfManeuvers];

            // Loop through maneuvers and calculate each one
            for (int i = 0; i < numberOfManeuvers; i++)
            {
                Console.WriteLine($"\n--- Maneuver {i + 1} ---");

                // Get user input with validation method
                double entrySpeedMiPerHr = GetDoubleInput("Enter the entry speed (mi/hr): ");
                double radiusFt = GetDoubleInput("Enter the loop radius (ft): ");

                // Store input
                entrySpeeds[i] = entrySpeedMiPerHr;
                radii[i] = radiusFt;

                // Convert speed
                double entrySpeedFtPerSec = entrySpeedMiPerHr * 1.467;

                // Loop circumference
                circumferences[i] = 2.0 * Math.PI * radiusFt;

                // Load factor
                double entrySpeedSquared = entrySpeedFtPerSec * entrySpeedFtPerSec;
                loadFactors[i] = entrySpeedSquared / (gravity * radiusFt) + 1.0;

                // Calculate top velocity
                double topVelocityFtPerSec = CalculateTopVelocity(entrySpeedFtPerSec, radiusFt, gravity);
                topVelocities[i] = topVelocityFtPerSec / 1.467;
            }

            // Print results using method
            PrintResults(entrySpeeds, radii, circumferences, loadFactors, topVelocities);

            // Run again?
            Console.Write("\nWould you like to calculate another set of maneuvers? (yes/no): ");
            string? response = Console.ReadLine()?.ToLower();

            runAgain = (response == "yes" || response == "y");

            if (!runAgain)
            {
                Console.WriteLine("\nThank you for using the math program!");
            }
        }
    }
}

