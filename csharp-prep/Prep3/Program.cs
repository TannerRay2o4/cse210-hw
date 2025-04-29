using System;

class Program
{
    static void Main(string[] args)
    {
        // Console.Write("What is the magic number? ");
        // int magicNumber = int.Parse(Console.ReadLine());

        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);

        int guess = -1;

        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            if (magicNumber > guess)
            {
                Console.WriteLine("Try again but higher. ");
            }
            else if (magicNumber < guess)
            {
                Console.WriteLine("Try again but lower. ");
            }
            else
            {
                Console.WriteLine("Nailed it! \n");
            }
        }
    }
}