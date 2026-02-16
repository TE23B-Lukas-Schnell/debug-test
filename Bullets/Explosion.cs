class Explosion : Projectile
{
    float gravity = 0;
    string spriteFilePath = "./Sprites/assets/blåsaUpp.png";
    Color color = Color.White;
    SpriteDrawer spriteDrawer = new();

    public override void Update()
    {
        OnHit(damage, "enemy");
        MoveObject(gravity);
        UpdateHitboxPosition(x, y, width, height);
    }

    public override void Draw()
    {
        spriteDrawer.DrawTexture(color, x, y); ;
    }

    public override void BeginDraw()
    {
        spriteDrawer.LoadSprite(Raylib.LoadTexture(spriteFilePath), width, height);
    }


    public Explosion(float x, float y, float width, float height, float damage)
    {
        ignoreGround = true;
        this.x = x;
        this.y = y;
        piercing = true;

        canGoOffscreen = false;
        this.damage = damage;
        this.width = width;
        this.height = height;
    }
}