using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("\nWhat is your first name? ");
        string first = Console.ReadLine();
        Console.Write("What is your last name? ");
        string last = Console.ReadLine();

        Console.WriteLine($"\nYour name is {last}, {first} {last}. \n");
    }
}