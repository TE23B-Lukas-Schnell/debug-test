class Items
{
    public string name;
    public string description;
    public bool buffActivated = false;

    public Dictionary<string, float> playerStatsAndTheirChangeAmount = new Dictionary<string, float>();

    public void PrintStats()
    {
        Console.WriteLine(name);
        Console.WriteLine(description);
    }

    public PlayerStat? GetPlayerStat(string statToChange) => Player.playerStats.Find(stat => stat.name == statToChange);

    public void ApplyBuff()
    {
        string[] statNames = playerStatsAndTheirChangeAmount.Keys.ToArray();
        for(int i = 0; i < playerStatsAndTheirChangeAmount.Count; i++)
        {
            string statName = statNames[i];
            float changeAmount = playerStatsAndTheirChangeAmount[statName];
            
            PlayerStat? statToChange = GetPlayerStat(statName);
            if (statToChange != null)
            {
                statToChange.Stat *= changeAmount;
                // Console.WriteLine($"Changed {statName} by multiplying with {changeAmount}");
            }
        }
    }

    public Items(string name, string description, Dictionary<string, float> playerStatsAndTheirChangeAmount)
    {
        this.playerStatsAndTheirChangeAmount = playerStatsAndTheirChangeAmount;
        this.name = name;
        this.description = description;
    }

}