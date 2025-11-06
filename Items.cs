class Items
{
    public string name;
    public string description;


    public void ApplyBuff()
    {
 
    }

    public void PrintStats()
    {
        Console.WriteLine(name);
        Console.WriteLine(description);
  
    }

    public Items(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

}