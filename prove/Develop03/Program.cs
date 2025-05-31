using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        // Scripture library
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new Reference("John 3:16"),
                "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            
            new Scripture(new Reference("Proverbs 3:5-6"),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
            
            new Scripture(new Reference("2 Nephi 2:25"),
                "Adam fell that men might be; and men are, that they might have joy.")
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose a scripture to memorize:");
            for (int i = 0; i < scriptures.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {scriptures[i].GetReference()}");
            }
            Console.WriteLine("Q. Quit");

            Console.Write("\nEnter a number (1-3) or 'Q' to quit: ");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "q")
                break;

            int choice;
            if (!int.TryParse(input, out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid input. Press Enter to continue...");
                Console.ReadLine();
                continue;
            }

            // Create a fresh copy so words reset
            Scripture selectedScripture = scriptures[choice - 1].Clone();

            while (true)
            {
                Console.Clear();
                selectedScripture.Display();
                Console.WriteLine("\n\nPress Enter to hide more words or type 'quit' to stop this scripture:");
                string userInput = Console.ReadLine().Trim().ToLower();

                if (userInput == "quit")
                    break;

                if (!selectedScripture.HideSome())
                {
                    Console.Clear();
                    selectedScripture.Display();
                    Console.WriteLine("\n\nAll words are hidden. Press Enter to return to main menu.");
                    Console.ReadLine();
                    break;
                }
            }
        }

        Console.WriteLine("\nGoodbye!");
    }
}
