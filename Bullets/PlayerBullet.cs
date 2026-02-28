class PlayerBullet : Projectile
{
    float gravity = 0;
    Color color = new Color(255, 0, 0, 255);

    public override void Update()
    {
        OnHit(damage, "enemy");
        MoveObject(gravity);
        UpdateHitboxPosition(x, y, width, height);
    }

    public override void Draw()
    {
        Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, color);
    }

    public PlayerBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)
    {
        ignoreGround = true;
        canGoOffscreen = true;
        this.x = x;
        this.y = y;
        this.xSpeed = xSpeed;
        this.ySpeed = ySpeed;
        this.width = width;
        this.height = height;
        this.gravity = gravity;
        this.damage = damage;
    }
}