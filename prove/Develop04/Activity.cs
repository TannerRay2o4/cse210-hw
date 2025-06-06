using System;
using System.Threading;

public abstract class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"Starting {_name}...");
        Console.WriteLine(_description);
        Console.Write("Enter duration in seconds: ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        PauseWithSpinner(3);
    }

    public void End()
    {
        Console.WriteLine($"\nWell done! You completed {_duration} seconds of {_name}.");
        PauseWithSpinner(3);
    }

    public void PauseWithSpinner(int seconds)
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

    public void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    public abstract void Run();
}
