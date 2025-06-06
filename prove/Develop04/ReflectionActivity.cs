using System;
using System.Collections.Generic;

public class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
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
        _name = "Reflection Activity";
        _description = "This activity will help you reflect on times in your life when you have shown strength and resilience.";
    }

    public override void Run()
    {
        Console.Clear();

        // Pick a unique prompt once
        List<string> promptPool = new List<string>(_prompts);
        ShuffleList(promptPool);
        Console.WriteLine("\n" + promptPool[0]);
        PauseWithSpinner(5);

        // Use unique questions (no repeats)
        List<string> questionPool = new List<string>(_questions);
        ShuffleList(questionPool);

        int elapsed = 0;
        int index = 0;

        while (elapsed < _duration && index < questionPool.Count)
        {
            Console.WriteLine($"> {questionPool[index]}");
            PauseWithSpinner(4);
            elapsed += 4;
            index++;
        }
    }

    private void ShuffleList(List<string> list)
    {
        Random rand = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
