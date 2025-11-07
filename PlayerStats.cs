class PlayerStat
{
    public float Stat
    { /*kiran*/
        get => stat;
        set => stat = Math.Clamp(value, minValue, maxValue);
    }

    float stat;
    float minValue;
    float maxValue;

    public PlayerStat(List<PlayerStat> listToAddTo, float stat)
    {
        this.stat = stat;
        minValue = -1_000_000;
        maxValue = 1_000_000;
        listToAddTo.Add(this);
    }

    public PlayerStat(List<PlayerStat> listToAddTo, float stat, float minValue, float maxValue)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        Stat = stat;
        listToAddTo.Add(this);
    }
}

