class MickePlayer : Player
{
    public MickePlayer(ControlLayout controlLayout) : base(controlLayout)
    {
        x = 400;
        y = 450;
        width = 85;
        height = 80;
        maxHP = 20;
        hp = maxHP;
        fastFallSpeed = 1400f;
        dashSpeed = 1900f;

        setDashCooldown = 0.43f;
        bulletxSpeed = 1800f;

        spriteFilePath = @"./Sprites/mikaelbergstrom-scaled-600x600.jpg";

        name = "Micke";
    }
}