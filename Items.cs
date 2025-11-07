class Items
{
    public string name;
    public string description;
    Player playerReference;
   
    public void ApplyBuff()
    {
        
    }

    public void PrintStats()
    {
        Console.WriteLine(name);
        Console.WriteLine(description);

    }

    public Items(string name, string description, Player playerReference)
    {
        this.playerReference = playerReference;
        this.name = name;
        this.description = description;
    }

}