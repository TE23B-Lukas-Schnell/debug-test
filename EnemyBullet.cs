class EnemyBullet : Projectile
{
    public float gravity;

    readonly Color color = new Color(255, 0, 0, 255);

    public override void Update()
    {
        OnHit(damage, "player");
        MoveObject(gravity);
    }

    public override void Draw()
    {
        Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, color);
        ShowHitboxes();
    
    }

    public override void BeginDraw()
    {

    }
    public override void Despawn()
    {

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
        canGoOffscreen = true;
        objectIdentifier = "båt";
        AddToGameList(this);
    }
}