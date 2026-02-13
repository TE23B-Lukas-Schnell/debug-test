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

    public PlayerBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage, bool travelsUp)
    {
        ignoreGround = true;
        this.canGoOffscreen = true;
        this.x = x;
        this.y = y;

        this.xSpeed = xSpeed;
        this.ySpeed = ySpeed;
        canGoOffscreen = true;
        this.damage = damage;

        if (travelsUp) // travels vertically
        {
            this.width = height;
            this.height = width;
            this.xSpeed = ySpeed;
            this.ySpeed = xSpeed;
        }
        else // travels horizontaly
        {
            this.width = width;
            this.height = height;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
        }
        this.gravity = gravity;
    }
}