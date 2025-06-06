using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing Activity";
        _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public override void Run()
    {
        int elapsed = 0;
        while (elapsed < _duration)
        {
            Console.Write("Breathe in...");
            Countdown(5);
            Console.Write("Breathe out...");
            Countdown(5);
            elapsed += 6;
        }
    }
}
