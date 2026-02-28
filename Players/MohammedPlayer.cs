class MohammedPlayer : Player
{
    public int bulletsShot = 0;
    public int explodeThreshold = 100;

    public override void Draw()
    {
        base.Draw();
        Displaybar(50, 145 + 95, (explodeThreshold * 2) + 10, 60, bulletsShot * 2, 5, 5, Color.Red);
    }

    void ImBoutToBlow()
    {
        bulletsShot = Math.Clamp(bulletsShot + 1, 0, explodeThreshold);
    }

    void BlåsaUpp()
    {
        float damage = bulletDamage * 0.5f * bulletsShot;
        new Explosion(x, y, 2500 / 4, 1248 / 4, damage);
    }

    public override void Despawn()
    {
        BlåsaUpp(); 
        base.Despawn();
    }

    public MohammedPlayer(ControlLayout controlLayout) : base(controlLayout)
    {
        width = 600 / 6;
        height = 489 / 6;
        maxHP = 27;
        hp = maxHP;
        name = "Mohammed cisco";

        gravity = 2400f;
        moveSpeed = 950f;
        jumpForce = 1200f;
        dashSpeed = 1800f;
        dashDuration = 0.21f;
        dashCooldown = 0.5f;
        fastFallSpeed = 1300f;
        bulletxSpeed = 1800;
        bulletySpeed = 100;
        
        //bullet stats
        shootCooldown = 0.7f;
        bulletWidth = 30f;
        bulletHeight = 30f;

        spriteFilePath = @"./Sprites/mohammed-scaled-600x489.jpg";

        bulletDamage = 10;

        shoot += ImBoutToBlow;
        upShoot += ImBoutToBlow;
    }
}