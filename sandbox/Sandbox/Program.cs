using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        // This is a program to compute the area of a circle.

        // Get the radius from the user.
        Console.Write("\nPlease enter the radius: ");
        string text = Console.ReadLine();
        double radius = double.Parse(text);

        // Compute the area of the circle
        double area = Math.PI * radius * radius;

        // Display the area for the user to see.
        Console.WriteLine($"\nThe area of the circle is: {area} \n");
     
    }
}