//Name: Rachel Sanchez
//Course: ENGR 115
//Assignment: Aerobatic Maneuver Calculations – Checkpoint 6 (Module 6)
//Description: This program calculates the load factor and velocity at the top of a
// loop for an aerobatic maneuver. It uses object-oriented design with arrays (size 10),
// a non-static ManeuverDesign class containing data and calculation methods, and a
// static DataManager class to get the number of designs, display results, and write results to a CSV file.
//Key features: User input (mi/hr and ft), internal conversion to ft/s for calculations,
// mathematical calculations, formatted table output, classes, methods, arrays, and file writing.

using System;
using System.IO;

public class ManeuverDesign
{
    public float[] entrySpeed = new float[10];
    public float[] loopRadius = new float[10];
    public float[] topSpeed = new float[10];
    public float[] loadFactor = new float[10];
    public float[] loopCircumference = new float[10];
    public bool[] loadFactorExceeded = new bool[10];

    private const float gravity = 32.2f;

    public float SetEntrySpeed()
    {
        while (true)
        {
            Console.Write("Enter the speed at which you will enter the loop (in mi/hr): ");
            string? input = Console.ReadLine();
            if (!float.TryParse(input, out float speedMiHr))
            {
                Console.WriteLine("Invalid speed input.");
                Console.WriteLine("\nPress Enter and re-enter this loop...");
                Console.ReadLine();
                continue;
            }

            float speedFtSec = speedMiHr * 1.467f;
            Console.WriteLine($"This is what you entered: {speedMiHr} mi/hr");
            return speedFtSec;
        }
    }

    public float SetLoopRadius()
    {
        while (true)
        {
            Console.Write("Enter the radius of the loop (in ft): ");
            string? input = Console.ReadLine();
            if (!float.TryParse(input, out float radiusFt))
            {
                Console.WriteLine("Invalid radius input.");
                Console.WriteLine("\nPress Enter and re-enter this loop...");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine($"This is what you entered: {radiusFt} ft");
            return radiusFt;
        }
    }

    public float CalcTopSpeed(float entrySpeedFtSec, float loopRadiusFt)
    {
        double underSqrt = (double)entrySpeedFtSec * entrySpeedFtSec - 4.0 * gravity * loopRadiusFt;
        if (underSqrt < 0)
        {
            return -1f;
        }
        return (float)Math.Sqrt(underSqrt);
    }

    public float CalcLoopCircumference(float loopRadiusFt)
    {
        return (float)(2.0 * Math.PI * loopRadiusFt);
    }

    public float CalcLoadFactor(float entrySpeedFtSec, float loopRadiusFt)
    {
        return (entrySpeedFtSec * entrySpeedFtSec) / (gravity * loopRadiusFt) + 1.0f;
    }

    public bool CheckOverGLimit(float loadFactorValue)
    {
        return loadFactorValue > 6.0f;
    }
}

public static class DataManager
{
    public static int SetNumberOfLoopDesigns()
    {
        while (true)
        {
            Console.Write("How many loops would you like to calculate? ");
            string? numInput = Console.ReadLine();
            if (!int.TryParse(numInput, out int numLoops) || numLoops <= 0 || numLoops > 10)
            {
                Console.WriteLine("Invalid amount. Please enter a positive whole number (1-10).");
                Console.WriteLine("\nPress Enter to try again...");
                Console.ReadLine();
                continue;
            }
            return numLoops;
        }
    }

    public static void DisplayOutput(ManeuverDesign design, int count)
    {
        Console.WriteLine();
        Console.WriteLine("Maneuver Design   Starting Velocity(mph)   Loop Radius(ft)   Velocity Top (mph)   Loop Circumference(ft)   Load Factor(g)   n exceeded");

        for (int i = 0; i < count; i++)
        {
            float startSpeedMph = design.entrySpeed[i] / 1.467f;
            float topSpeedMph = design.topSpeed[i] / 1.467f;

            string line = string.Format(
                "{0,-18} {1,-24:F0} {2,-16:F0} {3,-19:F0} {4,-24:F0} {5,-14:F0} {6}",
                i + 1,
                startSpeedMph,
                design.loopRadius[i],
                topSpeedMph,
                design.loopCircumference[i],
                design.loadFactor[i],
                design.loadFactorExceeded[i] ? "True" : "False"
            );

            Console.WriteLine(line);
        }
    }

    // CSV Writing Method
    public static void WriteDataToFile(ManeuverDesign design, int count)
    {
        string filePath = "results.csv";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Design,StartingVelocity_mph,LoopRadius_ft,TopVelocity_mph,LoopCircumference_ft,LoadFactor_g,nExceeded");

            for (int i = 0; i < count; i++)
            {
                float startSpeedMph = design.entrySpeed[i] / 1.467f;
                float topSpeedMph = design.topSpeed[i] / 1.467f;

                writer.WriteLine(
                    $"{i + 1}," +
                    $"{startSpeedMph:F0}," +
                    $"{design.loopRadius[i]:F0}," +
                    $"{topSpeedMph:F0}," +
                    $"{design.loopCircumference[i]:F0}," +
                    $"{design.loadFactor[i]:F0}," +
                    $"{design.loadFactorExceeded[i]}"
                );
            }
        }

        Console.WriteLine("\nCSV file saved as: " + filePath);
    }
}

class Program
{
    static void Main()
    {
        ManeuverDesign design = new ManeuverDesign();
        bool runAgain = true;

        do
        {
            Console.Clear();
            Console.WriteLine("Hello! Let's do some math!");

            int numLoops = DataManager.SetNumberOfLoopDesigns();
            Console.Clear();

            int storedCount = 0;

            for (int i = 0; i < numLoops; i++)
            {
                Console.WriteLine($"\n--- Loop {i + 1} of {numLoops} ---");

                float entryFtSec = design.SetEntrySpeed();
                float radiusFt = design.SetLoopRadius();
                Console.Clear();

                float topFtSec = design.CalcTopSpeed(entryFtSec, radiusFt);
                if (topFtSec < 0)
                {
                    Console.WriteLine("Error: parameters produce an impossible loop (negative value under square root).");
                    Console.WriteLine("Try a larger entry speed or a smaller radius.");
                    Console.WriteLine("\nPress Enter to re-enter this loop...");
                    Console.ReadLine();
                    i--;
                    continue;
                }

                float circumference = design.CalcLoopCircumference(radiusFt);
                float nFactor = design.CalcLoadFactor(entryFtSec, radiusFt);
                bool exceeded = design.CheckOverGLimit(nFactor);

                if (storedCount < 10)
                {
                    design.entrySpeed[storedCount] = entryFtSec;
                    design.loopRadius[storedCount] = radiusFt;
                    design.topSpeed[storedCount] = topFtSec;
                    design.loopCircumference[storedCount] = circumference;
                    design.loadFactor[storedCount] = nFactor;
                    design.loadFactorExceeded[storedCount] = exceeded;
                    storedCount++;
                }

                DataManager.DisplayOutput(design, storedCount);

                Console.WriteLine("\nPress Enter to continue to the next loop or to finish...");
                Console.ReadLine();
                Console.Clear();
            }

            // CALL CSV METHOD HERE ✔
            DataManager.WriteDataToFile(design, storedCount);

            Console.Write("\nWould you like to run the program again? (yes/no): ");
            string? response = Console.ReadLine()?.ToLower();
            runAgain = response == "yes" || response == "y";

        } while (runAgain);

        Console.WriteLine("\nThank you for using the math program!");
    }
}
