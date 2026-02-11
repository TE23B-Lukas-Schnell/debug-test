class KarimPlayer : Player
{
    public int bulletsShot = 0;

    public int båtThreshold = 10;

    public override void Draw()
    {
        base.Draw();
        Displaybar(50, 145 + 95, (båtThreshold * 13) + 10, 60, bulletsShot * 13, 5, 5, Color.Blue);
    }

    void CheckBåtigAttack()
    {

        if (bulletsShot >= båtThreshold)
        {
            bulletsShot = 0;

            float damage = bulletDamage + bulletDamage * (Math.Abs(xSpeed) / 100);
            BåtBullet.PlayerShoot(x, y, 110.2f, 50, (bulletxSpeed * 0.67f) + xSpeed * 0.67f, (bulletySpeed * 0.67f) + ySpeed, 1999, damage);
            System.Console.WriteLine(damage);
        }
        else
        {
            bulletsShot++;
        }

    }

    public KarimPlayer(ControlLayout controlLayout) : base(controlLayout)
    {
        x = 400;
        y = 450;
        width = 90;
        height = 90;
        maxHP = 19;
        hp = maxHP;

        setDashCooldown = 0.59f;
        bulletxSpeed = 1800f;
        bulletWidth = 30f;
        bulletHeight = 20f;
        jumpForce = 1450;
        dashSpeed = 2200;
        setDashDuration = 0.3f;

        spriteFilePath = @"./Sprites/karimryde-scaled-600x600.jpg";

        bulletDamage = 5;

        upShoot += CheckBåtigAttack;
        shoot += CheckBåtigAttack;

    }
}