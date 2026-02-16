class CallePlayer : Player
{

    public override void Update()
    {
        bulletDamageMultiplier = width / 100;
        base.Update();
    }

    public CallePlayer(ControlLayout controlLayout) : base(controlLayout)
    {
        x = 400;
        y = 450;
        width = 100;
        height = 80;
        maxHP = 23;
        hp = maxHP;
        bulletDamage = 10f;

        moveSpeed = 850f;
        dashDuration = 0.19f;
        dashSpeed = 1600;

        spriteFilePath = @"./Sprites/carlengman-scaled-600x600.jpg";

        name = "Carl Engman";

        // Inventory.Add(startItem);
    }
}