public static class NecComplianceChecker
{
    public static bool IsCompliant(Circuit circuit)
    {
        int receptacleCount = 0;

        foreach (var run in circuit.Components)
        {
            if (run.Component is Receptacle)
            {
                receptacleCount++;
            }
        }

        if (circuit.BreakerSize == 15 && receptacleCount > 10)
            return false;

        if (circuit.BreakerSize == 20 && receptacleCount > 12)
            return false;

        return true;
    }
}