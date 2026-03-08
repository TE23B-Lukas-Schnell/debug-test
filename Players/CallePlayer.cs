class CallePlayer : Player
{

    public override void Update()
    {
        bulletDamageMultiplier = width / 100;
        base.Update();
    }

    public CallePlayer(ControlLayout controlLayout) : base(controlLayout)
    {
        width = 100;
        height = 80;
        maxHP = 27;
        hp = maxHP;
        bulletDamage = 10f;

        moveSpeed = 850f;
        dashDuration = 0.19f;
        dashSpeed = 1600;

        spriteFilePath = @"./Sprites/lärare/carlengman-scaled-600x600.jpg";
        spriteDrawer.DefineSprite(spriteFilePath,width,height);

        name = "Carl Engman";
    }
}