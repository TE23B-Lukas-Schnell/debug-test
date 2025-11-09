class PlayerStat
{
    public float Stat
    { 
        /*kiran*/get => stat;
        set => stat = Math.Clamp(value, minValue, maxValue);
    }

    public string name;
    float stat;
    readonly float minValue;
    readonly float maxValue;

    public PlayerStat(List<PlayerStat> listToAddTo, float stat, string name)
    {
        this.stat = stat;
        minValue = float.MinValue;
        maxValue = float.MaxValue;
        listToAddTo.Add(this);
        this.name = name;
    }

    public PlayerStat(List<PlayerStat> listToAddTo, float stat, float minValue, float maxValue, string name)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        Stat = stat;
        listToAddTo.Add(this);
        this.name = name;
    }
}

