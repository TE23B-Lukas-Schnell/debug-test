class Items
{
    public string name;
    public string description;
    public enum playerStats
    {
        gravity,
        moveSpeed,
        jumpForce,
        setDashDuration,
        setDashCooldown,
        fastFallSpeed,
        dashSpeed,

        setShootCooldown,
        bulletWidth,
        bulletHeight,
        bulletDamage,
        bulletSpeed,
        bulletGravity
    }

    
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