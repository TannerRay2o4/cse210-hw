using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    static void Main()
    {
        Journal journal = new Journal();

        // Load autosave file if it exists
        if (File.Exists("journal_autosave.txt"))
        {
            journal.LoadFromFile("journal_autosave.txt");
            Console.WriteLine("Autosaved journal loaded.\n");
        }

        bool running = true;

        while (running)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to default autosave file");
            Console.WriteLine("4. Load journal from default autosave file");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option (1-5): ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    string prompt = GetRandomPrompt();
                    Console.WriteLine($"\nPrompt: {prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    journal.AddEntry(prompt, response);
                    break;
                case "2":
                    journal.DisplayAll();
                    break;
                case "3":
                    journal.SaveToFile("journal_autosave.txt");
                    Console.WriteLine("Journal saved to journal_autosave.txt");
                    break;
                case "4":
                    journal.LoadAutoSave();
                    Console.WriteLine("Journal loaded from journal_autosave.txt");
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static string GetRandomPrompt()
    {
        Random rand = new Random();
        return prompts[rand.Next(prompts.Count)];
    }
}
