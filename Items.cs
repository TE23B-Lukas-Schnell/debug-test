class Items
{
    public static Dictionary<string, Items> AllItems = [];

    public string name;
    public string description;
    public bool buffActivated = false;

    public delegate void ApplyStatChanges(FightableObject objectToBuff);
    public ApplyStatChanges ApplyStatChangesFunction;

    public void PrintStats()
    {
        Console.WriteLine(name);
        Console.WriteLine(description);
    }
    public Items(string name, string description, ApplyStatChanges applier)
    {
        ApplyStatChangesFunction = applier;
        this.name = name;
        this.description = description;
        AllItems.Add(this.name,this);
    }

}