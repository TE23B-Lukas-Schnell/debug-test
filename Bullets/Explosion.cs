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


    public Explosion(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage) : base(x, y, width, height, xSpeed, ySpeed, gravity, damage)
    {
     
        piercing = true;

        canGoOffscreen = false;
    }
}