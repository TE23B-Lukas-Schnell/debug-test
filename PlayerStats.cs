class PlayerStat
{
    public float Stat
    {
        /*kiran*/ get => stat;
        set => stat = Math.Clamp(value, minValue, maxValue);
    }

    float stat;
    float minValue;
    float maxValue;

    public PlayerStat(float stat)
    {
        this.stat = stat;
        minValue = -1_000_000;
        maxValue = 1_000_000;
    }

    public PlayerStat(float stat, float minValue, float maxValue, List<PlayerStat> playerStats)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        Stat = stat;
        playerStats.Add(this);
    }
}

