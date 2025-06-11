using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts = new()
    {
        "List as many things as you can that you are grateful for.",
        "List the people who have helped you recently.",
        "List your personal strengths.",
        "List the things that made you smile this week."
    };

    public ListingActivity()
    {
        Name = "Listing Activity";
        Description = "This activity will help you reflect by listing things that bring positivity or strength to your life.";
    }

    protected override int GetRecommendedDuration() => 60;

    public override void Run()
    {
        ShuffleList(_prompts);
        Console.WriteLine(_prompts[0]);
        Console.WriteLine("Start listing! Press Enter after each item.");

        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        int count = 0;

        while (DateTime.Now < endTime)
        {
            TimeSpan timeLeft = endTime - DateTime.Now;
            Console.Write($"[{timeLeft.Seconds}s left] > ");
            Console.ReadLine();
            count++;
        }

        Console.WriteLine($"\nYou listed {count} items!");
    }
}
