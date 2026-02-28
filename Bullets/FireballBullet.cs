using System.Numerics;
using System.Runtime.Intrinsics;

class
 FireballBullet : Projectile
{
    public float gravity;
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
        MoveObject(gravity);
        UpdateHitboxPosition(xpos, ypos, w, h);
    }

    public override void Draw()
    {
        spriteDrawer.Rotation =spriteDrawer.RotateAccordingToMovement(new(xSpeed, ySpeed));

        spriteDrawer.DrawTexture(color, x, y);
        // Raylib.DrawRectangle(R(hitbox.hitbox.X), R(hitbox.hitbox.Y), R(hitbox.hitbox.Width), R(hitbox.hitbox.Height), Color.Red);
        // hitbox.DrawHitbox();
    }

    public override void BeginDraw()
    {
        spriteDrawer.LoadSprite(Raylib.LoadTexture(spriteFilePath), width, height);
    }

    public FireballBullet(float x, float y, float xSpeed, float ySpeed, float gravity, float width, float height, float damage)
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
}