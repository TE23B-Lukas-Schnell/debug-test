class MartinPlayer : Player
{
    public MartinPlayer(ControlLayout controlLayout) : base(controlLayout)
    {
        width = 130;
        height = 130;
        maxHP = 30;
        hp = maxHP;
        bulletDamage = 25f;
        fastFallSpeed = 1000;
        gravity = 2700;

        moveSpeed = 500f;
        dashDuration = 0.19f;
        dashCooldown = 67f;
        dashSpeed = 1300;

        spriteFilePath = @"./Sprites/martinsoderblom-scaled-600x600.jpg";

        name = "Martin";
    }
}