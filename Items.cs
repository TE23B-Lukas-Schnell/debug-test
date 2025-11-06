class Items
{
    public string name;
    public string description;
    Player playerReference;
    public enum playerStats//𐛅ퟖ𓀧ɷꬤ 𐃵ꃔ𐛅 
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

    public Items(string name, string description, Player playerReference)
    {
        this.playerReference = playerReference;
        this.name = name;
        this.description = description;
    }

}