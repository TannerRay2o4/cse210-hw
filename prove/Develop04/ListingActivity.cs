using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "List as many things as you can that you are grateful for.",
        "List the people who have helped you recently.",
        "List your personal strengths.",
        "List the things that made you smile this week."
    };

    public ListingActivity()
    {
        _name = "Listing Activity";
        _description = "This activity will help you reflect by listing things that bring positivity or strength to your life.";
    }

    public override void Run()
    {
        Random rand = new Random();
        Console.WriteLine(_prompts[rand.Next(_prompts.Count)]);
        Console.WriteLine("Start listing! Press Enter after each item.");

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        int count = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            Console.ReadLine();
            count++;
        }

        Console.WriteLine($"\nYou listed {count} items!");
    }
}
