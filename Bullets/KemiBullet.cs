class KemiBullet : Projectile
{
    string spriteFilePath = "./Sprites/assets/kemi.png";
    string killerSpriteFilePath = "./Sprites/assets/killerMeme.jpg";

    public string target = "player";
    public string sprite = "sprite";

    SpriteDrawer spriteDrawer = new();

    public override void Draw()
    {
        // Raylib.DrawRectangle(R(x), R(y), R(width), R(height), color);
        spriteDrawer.DrawTexture(Color.White, x, y); ;
    }

    public override void Update()
    {
        OnHit(damage, target);
        MoveObject();
        UpdateHitboxPosition(x, y, width, height);
    }

    public override void BeginDraw()
    {
        spriteDrawer.currentSprite = sprite;
        spriteDrawer.InitializeSprite();
    }

    public KemiBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage) : base(x, y, width, height, xSpeed, ySpeed, gravity, damage)
    {
        spriteDrawer.DefineSprite(spriteFilePath, width, height);
        spriteDrawer.DefineSprite(killerSpriteFilePath, width, height, "killer");

    }
}