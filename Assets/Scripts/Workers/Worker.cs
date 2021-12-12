using System;

public class Worker : IEnergyConsumer
{
    public int ConsumedEnergy { get; }
    public int ConsumedSemiconductors { get; }

    private Worker(int consumedEnergy, int consumedSemiconductors)
    {
        ConsumedEnergy = consumedEnergy;
        ConsumedSemiconductors = consumedSemiconductors;
    }

    public static bool CanCreate(int consumedEnergy, int consumedSemiconductors, Context context)
    {
        return context.energy.CanUse(consumedEnergy, context) && context.semiconductors.CanSubstract(consumedSemiconductors, context);
    }

    public static Worker Create(int consumedEnergy, int consumedSemiconductors, Context context)
    {
        if (!CanCreate(consumedEnergy, consumedSemiconductors, context))
        {
            throw new InvalidOperationException();
        }
        context.semiconductors.Substract(consumedSemiconductors, context);
        return new Worker(consumedEnergy, consumedSemiconductors);
    }
}
