using System;
using System.Collections.Generic;

class Program
{
    static int totalTime = 0;
    static Dictionary<string, int> activityCounts = new();
    static string userName;

    static void Main(string[] args)
    {
        Console.Clear();
        Console.Write("Welcome! What's your name? ");
        userName = Console.ReadLine();
        Console.WriteLine($"Hello, {userName}! Press Enter to begin...");
        Console.ReadLine();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness App Menu");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");

            string input = GetMenuSelection();

            Activity activity = input switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => null,
                _ => null
            };

            if (input == "4") break;

            if (activity == null)
            {
                Console.WriteLine("Invalid option. Press Enter to try again.");
                Console.ReadLine();
                continue;
            }

            Console.Clear();
            Console.WriteLine($"Alright {userName}, let's begin your {activity.GetType().Name.Replace("Activity", "")}.");
            Console.WriteLine();

            activity.Start();
            activity.Run();
            activity.End();

            totalTime += activity.GetDuration();
            string name = activity.GetType().Name;
            if (!activityCounts.ContainsKey(name)) activityCounts[name] = 0;
            activityCounts[name]++;
        }

        // Exit summary
        Console.Clear();
        Console.WriteLine($"\nThanks for participating, {userName}!");
        Console.WriteLine($"Total time spent: {totalTime} seconds.");
        Console.WriteLine("Activities completed:");
        foreach (var kvp in activityCounts)
            Console.WriteLine($"- {kvp.Key.Replace("Activity", "")}: {kvp.Value} time(s)");

        Console.WriteLine("\nPress Enter to exit...");
        Console.ReadLine();
    }

    static string GetMenuSelection()
    {
        Console.Write("\nSelect an option (1-4): ");
        return Console.ReadLine()?.Trim();
    }
}
