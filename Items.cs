class Items
{
    public string name;
    public string description;
    MoveableObject? target;
    

    public void ApplyBuff()
    {
        
    }

    public void PrintStats()
    {
        Console.WriteLine(name);
        Console.WriteLine(description);
    }

    public static void GetPlayerStat(string statToChange)
    {
        
    }

    public Items(string name, string description, MoveableObject targetToModify)
    {
        target = targetToModify;
        this.name = name;
        this.description = description;
    }
}