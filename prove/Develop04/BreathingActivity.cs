public class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        Name = "Breathing Activity";
        Description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    protected override int GetRecommendedDuration() => 30;

    public override void Run()
    {
        int elapsed = 0;
        while (elapsed < Duration)
        {
            Console.Write("Breathe in...");
            SimpleCountdown(3);
            Console.Write("Breathe out...");
            SimpleCountdown(3);
            elapsed += 6;
        }
    }
}
