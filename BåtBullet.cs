class BåtBullet : EnemyBullet
{
    string spriteFilePath = "./Sprites/assets/karimsbåt.jpg";
    readonly Color color = Color.White;

    SpriteDrawer spriteDrawer = new();

    public override void Draw()
    {
        // Raylib.DrawRectangle(R(x), R(y), R(width), R(height), color);
        spriteDrawer.DrawTexture(color, x, y); ;
    }

    public override void BeginDraw()
    {
        spriteDrawer.LoadSprite(Raylib.LoadTexture(spriteFilePath), width, height);
    }

    public BåtBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)  : base(x, y, width, height, xSpeed, ySpeed, gravity, damage)
    {
        this.canGoOffscreen = true;
    }


}