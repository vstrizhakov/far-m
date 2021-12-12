using System.Collections.Generic;

public class Context
{
    public EnergyBank energy;
    public SemiconductorsBank semiconductors;
    public PeopleBank people;
    public List<Building> buildings;

    public Context()
    {
        energy = new EnergyBank();
        semiconductors = new SemiconductorsBank();
        people = new PeopleBank();
        buildings = new List<Building>();
    }
}
