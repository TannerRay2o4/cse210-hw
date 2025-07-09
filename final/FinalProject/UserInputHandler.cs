using System;

public static class UserInputHandler
{
    public static int GetInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (int.TryParse(input, out value) && value >= min && value <= max)
                return value;

            Console.WriteLine("Invalid input. Try again.");
        }
    }

    public static double GetDouble(string prompt, double min = double.MinValue, double max = double.MaxValue)
    {
        double value;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (double.TryParse(input, out value) && value >= min && value <= max)
                return value;

            Console.WriteLine("Invalid input. Try again.");
        }
    }

    public static string GetString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}