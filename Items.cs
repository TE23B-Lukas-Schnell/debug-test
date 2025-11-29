class Items
{
    public string name;
    public string description;
    public bool buffActivated = false;

    public delegate void ApplyStatChanges(FightableObject objectToBuff);
    public ApplyStatChanges båt;

    public void PrintStats()
    {
        Console.WriteLine(name);
        Console.WriteLine(description);
    }
    public Items(string name, string description, ApplyStatChanges applier)
    {
        båt = applier;
        this.name = name;
        this.description = description;
    }

}