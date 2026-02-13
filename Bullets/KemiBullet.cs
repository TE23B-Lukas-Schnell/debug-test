class KemiBullet : Projectile
{
    string spriteFilePath = "./Sprites/assets/kemi.png";
    readonly Color color = Color.White;

    string target;

    SpriteDrawer spriteDrawer = new();

    public override void Draw()
    {
        Raylib.DrawRectangle(R(x), R(y), R(width), R(height), color);
        spriteDrawer.DrawTexture(color, x, y); ;
    }

    public override void Update()
    {
        OnHit(damage, target);
        MoveObject(gravity);
        UpdateHitboxPosition(x, y, width, height);
    }

    public override void BeginDraw()
    {
        spriteDrawer.LoadSprite(Raylib.LoadTexture(spriteFilePath), width, height);
    }

    KemiBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage, bool ignoreGround)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.xSpeed = xSpeed;
        this.ySpeed = ySpeed;
        this.gravity = gravity;
        this.damage = damage;
        this.canGoOffscreen = true;
        this.ignoreGround = ignoreGround;
    }

    static public void PlayerShoot(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage, bool ignoreGround)
    {
        new KemiBullet(x, y, width, height, xSpeed, ySpeed, gravity, damage, ignoreGround)
        {
            target = "enemy"
        };
    }

    public static void EnemyShoot(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage, bool ignoreGround)
    {
        new KemiBullet(x, y, width, height, xSpeed, ySpeed, gravity, damage, ignoreGround)
        {
            target = "player"
        };
    }

     public static void ChristianShoot(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage, bool ignoreGround)
    {
        new KemiBullet(x, y, width, height, xSpeed, ySpeed, gravity, damage, ignoreGround)
        {
            target = "player",
            spriteFilePath = "./Sprites/assets/killerMeme.jpg",
        };
    }
}