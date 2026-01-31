class CallePlayer : Player, ISprite
{
    public override void BeginDraw()
    {
        spriteWidth = width;
        spriteHeight = height;

        sprite = Raylib.LoadTexture(@"./Sprites/carlengman-scaled-600x600.jpg");
        sprite = ((ISprite)this).ChangeSpriteSize(sprite, R(spriteWidth), R(spriteHeight));
    }


    public CallePlayer(ControlLayout controlLayout) : base(controlLayout)
    {
        x = 400;
        y = 450;
        width = 85;
        height = 80;
        maxHP = 20; hp = maxHP;

        spriteWidth = 85;
        spriteHeight = 80;
    }
}