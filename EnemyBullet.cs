class EnemyBullet : Projectile
{
    public float gravity;

    readonly Color color = new Color(200, 50, 0, 255);

    public override void Update()
    {
        OnHit(damage, "player");
        MoveObject(gravity);
        UppdateHitbox(x,y,width,height);
    }

    public override void Draw()
    {
        Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, color);
    }
    

    public EnemyBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)
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
        InitializeHitbox();
    }
    public EnemyBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage, bool ignoreGround)
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
        InitializeHitbox();
    }
}