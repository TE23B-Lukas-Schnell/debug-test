class PlayerBullet : Projectile
{
    float gravity = 0;
    Color color = new Color(255, 0, 0, 255);

    public override void Update()
    {
        OnHit(damage, "enemy");
        MoveObject(gravity);
    }

    public override void Draw()
    {
        Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, color);
        ShowHitboxes();
    }
    public override void Despawn()
    {

    }

    public PlayerBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)
    {
        this.x = x;
        this.y = y;

        gameList.Add(this);
        this.xSpeed = xSpeed;
        this.ySpeed = ySpeed;
        canGoOffscreen = true;
        this.damage = damage;

        if (ySpeed > xSpeed)
        {
            this.width = height;
            this.height = width;
        }
        else
        {
            this.width = width;
            this.height = height;
        }
        this.gravity = gravity;
    }
}