class KemiBullet : Projectile
{
    public string spriteFilePath = "./Sprites/assets/kemi.png";
    readonly Color color = Color.White;

    public string target = "player";

    SpriteDrawer spriteDrawer = new();

    public override void Draw()
    {
        // Raylib.DrawRectangle(R(x), R(y), R(width), R(height), color);
        spriteDrawer.DrawTexture(color, x, y); ;
    }

    public override void Update()
    {
        OnHit(damage, target);
        MoveObject();
        UpdateHitboxPosition(x, y, width, height);
    }

    public override void BeginDraw()
    {
        spriteDrawer.InitializeSprite(spriteFilePath, width, height);
    }

    public KemiBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage) : base(x, y, width, height, xSpeed, ySpeed, gravity, damage)
    {
        spriteDrawer.DefineSprites(spriteFilePath, width, height);
    }

    static public void PlayerShoot(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)
    {
        new KemiBullet(x, y, width, height, xSpeed, ySpeed, gravity, damage)
        {
            target = "enemy",
            ignoreGround = false,
        };
    }

    public static void ChristianShoot(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)
    {
        new KemiBullet(x, y, width, height, xSpeed, ySpeed, gravity, damage)
        {
            target = "player",
            spriteFilePath = "./Sprites/assets/killerMeme.jpg",
            ignoreGround = true,
        };
    }
}