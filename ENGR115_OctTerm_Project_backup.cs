//Name: Rachel Sanchez
//Course: ENGR 115
//Assignment: Aerobatic Maneuver Calculations
//Description: This program calculates the load factor and velocity at the top of a loop for an aerobatic maneuver based on user input for loop entry speed and loop radius.  
//Key features: User input, mathematical calculations, formatted output.
using System;
class Program
{
    static void Main()
    {
        // Greeting
        Console.WriteLine("Hello! Let's do some math!");
        // AAsk for loop entry speed and radius
        float loopEntrySpeed, loopRadius;

        Console.Write("Enter the speed at which you will enter the loop (in mi/hr): ");
        loopEntrySpeed = float.Parse(Console.ReadLine());
        Console.WriteLine("This is what you entered: " + loopEntrySpeed + " mi/hr");

        Console.Write("Enter the radius of the loop (in ft): ");
        loopRadius = float.Parse(Console.ReadLine());
        Console.WriteLine("This is what you entered: " + loopRadius + " ft");

        //Loop Circumference Calculation
        float loopCircumference = 2 * (float)Math.PI * loopRadius;
        
        //Constants
        const float gravity = 32.2f; // ft/s^2
        //Convert speed from mi/hr to ft/s
        float loopEntrySpeedFtPerSec = loopEntrySpeed * 1.467f;
        //entry speed squared
        float entrySpeedSquared = loopEntrySpeedFtPerSec * loopEntrySpeedFtPerSec;
        //load factor calculation
        float loadFactor = entrySpeedSquared / (gravity * loopRadius) + 1;
        //velocity at top of loop calculation
        float velocityAtTopOfLoop = (float)Math.Sqrt(entrySpeedSquared - 4 * gravity * loopRadius);
        Console.WriteLine("Velocity at the top of the loop (ft/s): " + velocityAtTopOfLoop);
        //Starting velocity
        float startingVelocity = (float)Math.Sqrt(entrySpeedSquared - 4 * gravity * loopRadius);
        //conversion of velocity at top of loop to mi/hr
        float velocityAtTopOfLoopMiPerHr = velocityAtTopOfLoop / 1.467f;


        //Output results in horizontal format
        Console.WriteLine("Results: HIII");
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine(" Starting velocity (mi/hr)          Loop radius (ft)         Loop Circumference (ft)         Load factor(g)           velocity at top of loop (mi/hr)");
        Console.WriteLine(   loopEntrySpeed + "                                 " + loopRadius + "                  " + loopCircumference + "                   " + loadFactor + "                             " + velocityAtTopOfLoopMiPerHr);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("Thank you for using the math program!");
    }
}
