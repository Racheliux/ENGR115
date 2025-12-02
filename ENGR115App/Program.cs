//Name: Rachel Sanchez
//Course: ENGR 115
//Assignment: Aerobatic Maneuver Calculations – Checkpoint 5
//Description: This program calculates the load factor and velocity at the top of a
// loop for an aerobatic maneuver based on user input for loop entry speed and loop radius.
//Key features: User input, mathematical calculations, formatted output, classes, methods, arrays.

using System;

// CLASS 1: LoopData (object class)
public class LoopData
{
    public double EntrySpeedMiHr { get; set; }
    public double RadiusFt { get; set; }
    public double LoopCircumference { get; set; }
    public double LoadFactor { get; set; }
    public double TopSpeedMiHr { get; set; }

    public LoopData(double entrySpeed, double radius, double circumference, double loadFactor, double topSpeed)
    {
        EntrySpeedMiHr = entrySpeed;
        RadiusFt = radius;
        LoopCircumference = circumference;
        LoadFactor = loadFactor;
        TopSpeedMiHr = topSpeed;
    }
}

// CLASS 2: MathFunctions (static)
public static class MathFunctions
{
    public const double gravity = 32.2; // ft/s^2

    public static double ConvertSpeedToFtPerSec(double speedMiHr)
    {
        return speedMiHr * 1.467;
    }

    public static double CalculateCircumference(double radiusFt)
    {
        return 2.0 * Math.PI * radiusFt;
    }

    public static double CalculateVelocityAtTop(double entrySpeedFtSec, double radiusFt)
    {
        double underSqrt = entrySpeedFtSec * entrySpeedFtSec - 4.0 * gravity * radiusFt;

        if (underSqrt < 0)
        {
            return -1; 
        }

        return Math.Sqrt(underSqrt);
    }

    public static double CalculateLoadFactor(double entrySpeedFtSec, double radiusFt)
    {
        return (entrySpeedFtSec * entrySpeedFtSec) / (gravity * radiusFt) + 1.0;
    }
}

// MAIN PROGRAM
class Program
{
    static void Main()
    {
        bool runAgain = true;

        LoopData[] pastResults = new LoopData[10];
        int resultIndex = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("Hello! Let's do some math!");

            // Number of loops
            Console.Write("How many loops would you like to calculate? ");
            string? numInput = Console.ReadLine();

            if (!int.TryParse(numInput, out int numLoops) || numLoops <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a positive whole number.");
                Console.WriteLine("\nPress Enter to try again...");
                Console.ReadLine();
                continue;
            }

            // Loop calculations
            for (int i = 1; i <= numLoops; i++)
            {
                while (true)   
                {
                    Console.WriteLine($"\n--- Loop {i} of {numLoops} ---");

                    // SPEED INPUT
                    Console.Write("Enter the speed at which you will enter the loop (in mi/hr): ");
                    string? input = Console.ReadLine();

                    if (!double.TryParse(input, out double loopEntrySpeedMiPerHr))
                    {
                        Console.WriteLine("Invalid speed input.");
                        Console.WriteLine("\nPress Enter and re-enter Loop " + i + "...");
                        Console.ReadLine();
                        continue; 
                    }

                    Console.WriteLine($"This is what you entered: {loopEntrySpeedMiPerHr} mi/hr");

                    // RADIUS INPUT
                    Console.Write("Enter the radius of the loop (in ft): ");
                    input = Console.ReadLine();

                    if (!double.TryParse(input, out double loopRadiusFt))
                    {
                        Console.WriteLine("Invalid radius input.");
                        Console.WriteLine("\nPress Enter and re-enter Loop " + i + "...");
                        Console.ReadLine();
                        continue; 
                    }

                    Console.WriteLine($"This is what you entered: {loopRadiusFt} ft");

                    // Calculations
                    double entrySpeedFtSec = MathFunctions.ConvertSpeedToFtPerSec(loopEntrySpeedMiPerHr);
                    double loopCircumference = MathFunctions.CalculateCircumference(loopRadiusFt);
                    double velocityAtTopFtSec = MathFunctions.CalculateVelocityAtTop(entrySpeedFtSec, loopRadiusFt);

                    if (velocityAtTopFtSec < 0)
                    {
                        Console.WriteLine("Error: parameters produce an impossible loop.");
                        Console.WriteLine("Try a larger entry speed or a smaller radius.");
                        Console.WriteLine("\nPress Enter to re-enter Loop " + i + "...");
                        Console.ReadLine();
                        continue; 
                    }

                    double velocityAtTopMiHr = velocityAtTopFtSec / 1.467;
                    double loadFactor = MathFunctions.CalculateLoadFactor(entrySpeedFtSec, loopRadiusFt);

                    // STORE RESULTS
                    if (resultIndex < 10)
                    {
                        pastResults[resultIndex] = new LoopData(
                            loopEntrySpeedMiPerHr,
                            loopRadiusFt,
                            loopCircumference,
                            loadFactor,
                            velocityAtTopMiHr
                        );
                        resultIndex++;
                    }

                    // PRINT RESULTS
                    Console.WriteLine("\nResults:");
                    Console.WriteLine(new string('-', 125));
                    Console.WriteLine(" Starting velocity (mi/hr)    Loop radius (ft)    Loop Circumference (ft)    Load factor (g)    Velocity at top (mi/hr)");

                    Console.WriteLine(
                        $"{loopEntrySpeedMiPerHr,24:F2}" +
                        $"{loopRadiusFt,20:F2}" +
                        $"{loopCircumference,26:F2}" +
                        $"{loadFactor,20:F2}" +
                        $"{velocityAtTopMiHr,21:F2}"
                    );

                    Console.WriteLine(new string('-', 125));

                    break; 
                }
            }

            // Run again?
            Console.Write("\nWould you like to run the program again? (yes/no): ");
            string? response = Console.ReadLine()?.ToLower();
            runAgain = response == "yes" || response == "y";

        } while (runAgain);
        // Exit message
        Console.WriteLine("\nThank you for using the math program!");
    }
}
