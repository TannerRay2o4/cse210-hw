using System;

public static class UserInputHandler
{
    public static int GetInt(string prompt, int min, int max)
    {
        int value;
        do
        {
            Console.Write(prompt);
        } while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max);
        return value;
    }

    public static double GetDouble(string prompt, double min)
    {
        double value;
        do
        {
            Console.Write(prompt);
        } while (!double.TryParse(Console.ReadLine(), out value) || value < min);
        return value;
    }

    public static string GetString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}
