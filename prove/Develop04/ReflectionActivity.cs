using System;
using System.Collections.Generic;

public class ReflectionActivity : Activity
{
    private List<string> _prompts = new()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times?",
        "What is your favorite thing about this experience?",
        "What did you learn from this experience?",
        "What did you learn about yourself?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        Name = "Reflection Activity";
        Description = "This activity will help you reflect on times in your life when you have shown strength and resilience.";
    }

    protected override int GetRecommendedDuration() => 45;

    public override void Run()
    {
        Console.Clear();
        ShuffleList(_prompts);
        Console.WriteLine("\n" + _prompts[0]);
        PauseWithSpinner(5);

        ShuffleList(_questions);
        int elapsed = 0;
        int index = 0;

        while (elapsed < Duration && index < _questions.Count)
        {
            Console.WriteLine($"> {_questions[index]}");
            PauseWithSpinner(4);
            elapsed += 4;
            index++;
        }
    }
}
