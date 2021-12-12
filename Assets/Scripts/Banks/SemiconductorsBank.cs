using System;
using System.Linq;

public class SemiconductorsBank : Bank
{
    private int _amount;

    public override int GetCapacity(Context context)
    {
        var scrapyards = context.buildings.OfType<ISemiconductorsStorage>();
        return scrapyards.Sum(x => x.SemiconductorsCapacity);
    }

    public int Add(int amount, Context context)
    {
        if (!CanAdd(amount, context))
        {
            throw new InvalidOperationException();
        }
        _amount += amount;
        return _amount;
    }

    public int Substract(int amount, Context context)
    {
        if (!CanSubstract(amount, context))
        {
            throw new InvalidOperationException();
        }
        _amount -= amount;
        return _amount;
    }

    public override int GetAmount(Context context)
    {
        return _amount;
    }

    public bool CanAdd(int amount, Context context)
    {
        return _amount + amount <= GetCapacity(context);
    }

    public bool CanSubstract(int amount, Context context)
    {
        return _amount - amount >= 0;
    }
}
