//Name: Rachel Sanchez
//Course: ENGR 115
//Assignment: Aerobatic Maneuver Calculations – Checkpoint 5 (Module 4 & 5)
//Description: This program calculates the load factor and velocity at the top of a
// loop for an aerobatic maneuver. It uses object-oriented design with arrays (size 10),
// a non-static ManeuverDesign class containing data and calculation methods, and a
// static DataManager class to get the number of designs and display results.
//Key features: User input (mi/hr and ft), internal conversion to ft/s for calculations,
// mathematical calculations, formatted table output, classes, methods, arrays.

using System;

public class ManeuverDesign
{
    // Arrays of size 10 per module requirement (float)
    public float[] entrySpeed = new float[10];            // stored in ft/s (internal)
    public float[] loopRadius = new float[10];            // ft
    public float[] topSpeed = new float[10];              // ft/s
    public float[] loadFactor = new float[10];            // g
    public float[] loopCircumference = new float[10];     // ft
    public bool[] loadFactorExceeded = new bool[10];      // bool

    // Gravity constant (ft/s^2)
    private const float gravity = 32.2f;

    // Methods required by the assignment

    // Request and return the entry speed (mi/hr converted to ft/s)
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

            // convert to ft/s internally (1 mi/hr = 1.467 ft/s)
            float speedFtSec = speedMiHr * 1.467f;
            Console.WriteLine($"This is what you entered: {speedMiHr} mi/hr");
            return speedFtSec;
        }
    }

    // Request and return the loop radius (ft)
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

    // Calculate top speed (ft/s) using v_top = sqrt(v_entry^2 - 4*g*r)
    public float CalcTopSpeed(float entrySpeedFtSec, float loopRadiusFt)
    {
        double underSqrt = (double)entrySpeedFtSec * entrySpeedFtSec - 4.0 * gravity * loopRadiusFt;
        if (underSqrt < 0)
        {
            return -1f; // signal impossible loop
        }
        return (float)Math.Sqrt(underSqrt);
    }

    // Calculate loop circumference (ft)
    public float CalcLoopCircumference(float loopRadiusFt)
    {
        return (float)(2.0 * Math.PI * loopRadiusFt);
    }

    // Calculate load factor at bottom: n = v^2/(g*r) + 1  (v in ft/s)
    public float CalcLoadFactor(float entrySpeedFtSec, float loopRadiusFt)
    {
        return (entrySpeedFtSec * entrySpeedFtSec) / (gravity * loopRadiusFt) + 1.0f;
    }

    // Check if g-load exceeds a limit (example: > 6g is considered exceeded here)
    // Return true if exceeded, false otherwise.
    public bool CheckOverGLimit(float loadFactorValue)
    {
        // Use 6g as the example threshold (instructors sometimes use this)
        return loadFactorValue > 6.0f;
    }
}

public static class DataManager
{
    // Ask the user how many loop designs to evaluate and return int
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

    // DisplayOutput: displays the results table as shown in the sample (Fig 5)
    // It will print all results up to 'count' entries from the ManeuverDesign instance.
    public static void DisplayOutput(ManeuverDesign design, int count)
    {
        // Header row
        Console.WriteLine();
        Console.WriteLine("Maneuver Design   Starting Velocity(mph)   Loop Radius(ft)   Velocity Top (mph)   Loop Circumference(ft)   Load Factor(g)   n exceeded");
        
        for (int i = 0; i < count; i++)
        {
            // Convert stored ft/s speeds back to mi/hr for display: mph = ft/s / 1.467
            float startSpeedMph = design.entrySpeed[i] / 1.467f;
            float topSpeedMph = design.topSpeed[i] / 1.467f;

            // Format numbers similar to the sample output: integer-like for simplicity
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
}

class Program
{
    static void Main()
    {
        // Header comments are at the top of the file for submission requirements.
        ManeuverDesign design = new ManeuverDesign();

        bool runAgain = true;

        do
        {
            Console.Clear();

            Console.WriteLine("Hello! Let's do some math!");

            // Step 1: Ask number of designs
            int numLoops = DataManager.SetNumberOfLoopDesigns();

            // Clear screen as specified by the module flow
            Console.Clear();

            int storedCount = 0; // how many valid results we've saved

            // Step 2 & 3: For each design, get inputs and display cumulative results
            for (int i = 0; i < numLoops; i++)
            {
                Console.WriteLine($"\n--- Loop {i + 1} of {numLoops} ---");

                // Get entry speed (returns ft/s)
                float entryFtSec = design.SetEntrySpeed();

                // Get loop radius (ft)
                float radiusFt = design.SetLoopRadius();

                // Clear screen before showing table (module asks to clear and then show results)
                Console.Clear();

                // Calculate values (ft/s internally)
                float topFtSec = design.CalcTopSpeed(entryFtSec, radiusFt);
                if (topFtSec < 0)
                {
                    Console.WriteLine("Error: parameters produce an impossible loop (negative value under square root).");
                    Console.WriteLine("Try a larger entry speed or a smaller radius.");
                    Console.WriteLine("\nPress Enter to re-enter this loop...");
                    Console.ReadLine();
                    i--; // repeat this iteration
                    continue;
                }

                float circumference = design.CalcLoopCircumference(radiusFt);
                float nFactor = design.CalcLoadFactor(entryFtSec, radiusFt);
                bool exceeded = design.CheckOverGLimit(nFactor);

                // Store into arrays (up to 10)
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

                // Display cumulative table after this input (shows all previous entries)
                DataManager.DisplayOutput(design, storedCount);

                // Pause briefly so the user can see the table before next input
                Console.WriteLine("\nPress Enter to continue to the next loop or to finish...");
                Console.ReadLine();
                Console.Clear();
            }

            // After finishing the requested loops, ask if user wants to run full program again
            Console.Write("\nWould you like to run the program again? (yes/no): ");
            string? response = Console.ReadLine()?.ToLower();
            runAgain = response == "yes" || response == "y";

        } while (runAgain);

        Console.WriteLine("\nThank you for using the math program!");
    }
}
