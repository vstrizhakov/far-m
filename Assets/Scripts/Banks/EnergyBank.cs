using System.Linq;

public class EnergyBank : Bank
{
    public override int GetCapacity(Context context)
    {
        var producers = context.buildings.OfType<IEnergyProducer>();
        return producers.Sum(x => x.ProducedEnergy);
    }

    public override int GetAmount(Context context)
    {
        var consumers = context.buildings.OfType<IEnergyConsumer>().ToList();
        var count = consumers.Count();
        return consumers.Sum(x => x.ConsumedEnergy);
    }

    public bool CanUse(int amount, Context context)
    {
        var currentAmount = GetAmount(context);
        var capacity = GetCapacity(context);
        return currentAmount + amount <= capacity;
    }
}
