class BåtBullet : Projectile
{
    string spriteFilePath = "./Sprites/assets/karimsbåt.jpg";
    readonly Color color = Color.White;
    string target = "player";

    SpriteDrawer spriteDrawer = new();

    public override void Update()
    {
        OnHit(damage, target);
        MoveObject(gravity);
        UpdateHitboxPosition(x, y, width, height);
    }

    public override void Draw()
    {
        // Raylib.DrawRectangle(R(x), R(y), R(width), R(height), color);
        spriteDrawer.DrawTexture(color, x, y); ;
    }

    public override void BeginDraw()
    {
        spriteDrawer.LoadSprite(Raylib.LoadTexture(spriteFilePath), width, height);
    }

    BåtBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)
    {
        this.canGoOffscreen = true;
        this.ignoreGround = false;

        this.x = x;
        this.y = y;
        this.xSpeed = xSpeed;
        this.ySpeed = ySpeed;
        this.width = width;
        this.height = height;
        this.damage = damage;
        this.gravity = gravity;
    }

    public static void KarimShoot(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)
    {
        new BåtBullet(x, y, width, height, xSpeed, ySpeed, gravity, damage);
    }

    public static void PlayerShoot(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)
    {
        new BåtBullet(x, y, width, height, xSpeed, ySpeed, gravity, damage)
        {
            target = "enemy",
        };
    }

}