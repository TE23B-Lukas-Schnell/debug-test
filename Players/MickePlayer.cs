class MickePlayer : Player
{
    public MickePlayer(ControlLayout controlLayout) : base(controlLayout)
    {
        width = 85;
        height = 80;
        maxHP = 20;
        hp = maxHP;
        fastFallSpeed = 1400f;
        dashSpeed = 1900f;

        dashCooldown = 0.43f;
        bulletxSpeed = 1800f;
        bulletDamage = 10f;

        spriteFilePath = @"./Sprites/mikaelbergstrom-scaled-600x600.jpg";

        name = "Köttig Micke";
    }
}