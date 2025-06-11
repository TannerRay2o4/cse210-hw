using System;
using System.Collections.Generic;
using System.Threading;

public abstract class Activity
{
    protected string Name { get; set; }
    protected string Description { get; set; }
    protected int Duration { get; set; }

    public void Start()
    {
        DisplayIntro();

        Console.WriteLine($"Recommended duration: {GetRecommendedDuration()} seconds.");
        int parsed;
        do
        {
            Console.Write("Enter duration in seconds: ");
        } while (!int.TryParse(Console.ReadLine(), out parsed) || parsed <= 0);
        Duration = parsed;

        Console.WriteLine("Prepare to begin...");
        PauseWithSpinner(3);
    }

    public void End()
    {
        Console.WriteLine($"\nWell done! You completed {Duration} seconds of {Name}.");
        PauseWithSpinner(3);
    }

    protected void DisplayIntro()
    {
        Console.WriteLine($"Activity: {Name}");
        Console.WriteLine(Description);
        Console.WriteLine();
    }

    protected void PauseWithSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("|");
            Thread.Sleep(250);
            Console.Write("\b/");
            Thread.Sleep(250);
            Console.Write("\b-");
            Thread.Sleep(250);
            Console.Write("\b\\");
            Thread.Sleep(250);
            Console.Write("\b");
        }
        Console.WriteLine();
    }

    // ASCII countdown progress bar
    protected void Countdown(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("[");
            Console.Write(new string('#', i + 1));
            Console.Write(new string('-', seconds - i - 1));
            Console.Write($"] {seconds - i}s\r");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    // Simple numeric countdown for breathing
    protected void SimpleCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    protected void ShuffleList<T>(List<T> list)
    {
        Random rand = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    protected abstract int GetRecommendedDuration();
    public abstract void Run();
    public int GetDuration() => Duration;
}
