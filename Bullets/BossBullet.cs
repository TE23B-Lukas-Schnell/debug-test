class BossBullet : Projectile
{
    readonly Color color = new Color(200, 50, 0, 255);

    public override void Update()
    {
        OnHit(damage, "player");
        MoveObject();
        UpdateHitboxPosition(x,y,width,height);
    }

    public override void Draw()
    {
        Raylib.DrawRectangle(R(x), R(y), R(width), R(height), color);
    }
    
    public BossBullet(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage) : base(x, y, width, height, xSpeed, ySpeed, gravity, damage)
    {

    }
   
}