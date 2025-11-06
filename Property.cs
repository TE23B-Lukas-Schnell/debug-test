class Property
{

    public float Stat
    {
        /*kiran*/get => stat;
        set => stat = Math.Clamp(value, minValue, maxValue);
    }

    float stat;
    float minValue;
    float maxValue;

    public Property(float stat)
    {
        this.stat = stat;
        minValue = -1_000_000;
        maxValue = 1_000_000;
    }

    public Property(float stat, float minValue, float maxValue)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        Stat = stat;
    }
}