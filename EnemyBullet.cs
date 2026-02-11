class BossBullet : Projectile
{
    public float gravity;

    readonly Color color = new Color(200, 50, 0, 255);

    public override void Update()
    {
        OnHit(damage, "player");
        MoveObject(gravity);
        UpdateHitboxPosition(x,y,width,height);
    }

    public override void Draw()
    {
        Raylib.DrawRectangle(R(x), R(y), R(width), R(height), color);
    }
    

    public BossBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.xSpeed = xSpeed;
        this.ySpeed = ySpeed;
        this.gravity = gravity;
        this.damage = damage;
        canGoOffscreen = true;
        ignoreGround = true;
    }
    public BossBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage, bool ignoreGround)
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
}