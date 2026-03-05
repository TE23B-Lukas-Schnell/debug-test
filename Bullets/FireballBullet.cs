class FireballBullet : Projectile
{
    string spriteFilePath = "./Sprites/assets/fireball.png";
    readonly Color color = Color.White;
    SpriteDrawer spriteDrawer = new();
    float hitboxSizeOffset = 0.4f;

    public override void Update()
    {
        float w = width * hitboxSizeOffset;
        float h = height * hitboxSizeOffset;
        float xpos = x + (width - w) / 2;
        float ypos = y + (height - h) / 2;

        OnHit(damage, "player");
        MoveObject();
        UpdateHitboxPosition(xpos, ypos, w, h);
    }

    public override void Draw()
    {
        spriteDrawer.Rotation = spriteDrawer.RotateAccordingToMovement(new(xSpeed, ySpeed));

        spriteDrawer.DrawTexture(color, x, y);
        // Raylib.DrawRectangle(R(hitbox.hitbox.X), R(hitbox.hitbox.Y), R(hitbox.hitbox.Width), R(hitbox.hitbox.Height), Color.Red);
        // hitbox.DrawHitbox();
    }

    public override void BeginDraw()
    {
        spriteDrawer.InitializeSprite(spriteFilePath, width, height);
    }

    // new FireballBullet(x(), y2, 3.5f * speedMultiplier, 3 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);

    public FireballBullet(float x, float y, float xSpeed, float ySpeed, float gravity, float width, float height, float damage) : base(x, y, width, height, xSpeed, ySpeed, gravity, damage)
    {

    }
}