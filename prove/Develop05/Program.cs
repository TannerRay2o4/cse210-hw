using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==============================");
            Console.WriteLine($"Current Score: {score} points");
            Console.WriteLine("==============================");
            Console.WriteLine("Main Menu:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Record an Event");
            Console.WriteLine("  4. Save Goals to File");
            Console.WriteLine("  5. Load Goals from File");
            Console.WriteLine("  6. Quit");

            int option = GetValidatedOption("Select an option (1–6): ", 1, 6);
            Console.WriteLine();

            switch (option)
            {
                case 1: CreateGoal(); break;
                case 2: ListGoals(); break;
                case 3: RecordEvent(); break;
                case 4: SaveGoals(); break;
                case 5: LoadGoals(); break;
                case 6:
                    Console.WriteLine("\nThanks for using Eternal Quest! Goodbye!\n");
                    return;
            }
        }
    }

    static void CreateGoal()
    {
        Console.Clear();
        Console.WriteLine("What type of goal would you like to create?");
        Console.WriteLine("  1. Simple Goal (e.g., run a marathon)");
        Console.WriteLine("  2. Eternal Goal (e.g., read scriptures repeatedly)");
        Console.WriteLine("  3. Checklist Goal (e.g., attend temple 10 times)");
        int type = GetValidatedOption("Enter your choice (1–3): ", 1, 3);
        Console.WriteLine();

        Console.Write("Enter the NAME of the goal (e.g., 'Run Marathon'): ");
        string name = Console.ReadLine();

        Console.Write("Enter a short DESCRIPTION of the goal: ");
        string description = Console.ReadLine();

        Console.WriteLine("\nChoose how many POINTS this goal is worth based on effort:");
        Console.WriteLine("  • 100 points → Small task (e.g., say a prayer)");
        Console.WriteLine("  • 300 points → Medium task (e.g., read scriptures for 15 min)");
        Console.WriteLine("  • 500 points → Challenging task (e.g., attend church)");
        Console.WriteLine("  • 1000+ points → Major achievement (e.g., run a marathon)");
        int points = GetPositiveInt("Enter the number of points to award: ");
        Console.WriteLine();

        switch (type)
        {
            case 1:
                goals.Add(new SimpleGoal(name, description, points));
                break;
            case 2:
                goals.Add(new EternalGoal(name, description, points));
                break;
            case 3:
                int target = GetPositiveInt("How many times must this goal be completed? ");
                int bonus = GetPositiveInt("How many BONUS points on final completion? ");
                Console.WriteLine();
                goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
        }

        Console.WriteLine("✅ Goal created successfully!\n");
        Pause();
    }

    static void ListGoals()
    {
        Console.Clear();
        Console.WriteLine("Your Current Goals:\n");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
        }
        Console.WriteLine();
        Pause();
    }

    static void RecordEvent()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("⚠️ No goals available to record. Please create a goal first.\n");
            Pause();
            return;
        }

        Console.Clear();
        Console.WriteLine("Which goal did you accomplish?");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
        }
        Console.WriteLine("\n  0. Cancel");

        int choice = GetValidatedOption("Enter the number of the goal you completed (or 0 to cancel): ", 0, goals.Count);

        if (choice == 0)
        {
            Console.WriteLine("\nReturning to main menu...");
            return;
        }

        int index = choice - 1;

        goals[index].RecordEvent();
        score += goals[index].GetPoints();

        if (goals[index] is ChecklistGoal cg)
            score += cg.GetBonus();

        Console.WriteLine($"\n Event recorded! +{goals[index].GetPoints()} points earned.");
        Console.WriteLine($"Updated Score: {score}\n");
        Pause();
    }

    static void SaveGoals()
    {
        Console.Clear();
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(score);
            foreach (var goal in goals)
            {
                writer.WriteLine(goal.SaveData());
            }
        }
        Console.WriteLine("Goals saved successfully to 'goals.txt'.\n");
        Pause();
    }

    static void LoadGoals()
    {
        Console.Clear();

        if (!File.Exists("goals.txt"))
        {
            Console.WriteLine("No save file found.\n");
            Pause();
            return;
        }

        string[] lines = File.ReadAllLines("goals.txt");

        if (lines.Length == 0)
        {
            Console.WriteLine("Save file is empty. No goals loaded.\n");
            Pause();
            return;
        }

        try
        {
            goals.Clear();
            score = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                switch (parts[0])
                {
                    case "Simple":
                        var simple = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                        simple.LoadData(lines[i]);
                        goals.Add(simple);
                        break;
                    case "Eternal":
                        var eternal = new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
                        eternal.LoadData(lines[i]);
                        goals.Add(eternal);
                        break;
                    case "Checklist":
                        var checklist = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[6]));
                        checklist.LoadData(lines[i]);
                        goals.Add(checklist);
                        break;
                }
            }

            Console.WriteLine("Goals loaded successfully.\n");
        }
        catch
        {
            Console.WriteLine("Save file is empty or invalid. No goals loaded.\n");
        }

        Pause();
    }


    static void Pause()
    {
        Console.WriteLine("Press Enter to return to the main menu...");
        Console.ReadLine();
    }

    static int GetValidatedOption(string prompt, int min, int max)
    {
        int input;
        bool valid;
        do
        {
            Console.Write(prompt);
            valid = int.TryParse(Console.ReadLine(), out input) && input >= min && input <= max;
            if (!valid)
                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
        } while (!valid);
        return input;
    }

    static int GetPositiveInt(string prompt)
    {
        int input;
        bool valid;
        do
        {
            Console.Write(prompt);
            valid = int.TryParse(Console.ReadLine(), out input) && input > 0;
            if (!valid)
                Console.WriteLine("Invalid input. Please enter a positive number.");
        } while (!valid);
        return input;
    }
}
