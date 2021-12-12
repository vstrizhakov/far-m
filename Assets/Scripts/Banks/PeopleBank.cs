using System.Linq;

public class PeopleBank : Bank
{
    public override int GetCapacity(Context context)
    {
        var producers = context.buildings.OfType<IPeopleProducer>();
        return producers.Sum(x => x.ProducedPeople);
    }

    public override int GetAmount(Context context)
    {
        var consumers = context.buildings.OfType<IPeopleConsumer>();
        return consumers.Sum(x => x.ConsumedPeople);
    }

    public bool CanUse(int amount, Context context)
    {
        var currentAmount = GetAmount(context);
        var capacity = GetCapacity(context);
        return currentAmount + amount <= capacity;
    }
}
