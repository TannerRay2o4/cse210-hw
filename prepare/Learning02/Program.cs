using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        job1._jobTitle = "Electrical Engineer Intern";
        job1._company = "EMC";
        job1._startYear = 2023;
        job1._endYear = 2024;

        Job job2 = new Job();
        job2._jobTitle = "Heavy Equipment Operator";
        job2._company = "Buller's Custom Hay";
        job2._startYear = 2016;
        job2._endYear = 2020;

        Resume myResume = new Resume();
        myResume._name = "Tanner Ray";

        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        myResume.Display();
        Console.WriteLine();
    }
}