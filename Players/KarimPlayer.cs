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
            BåtigBulletHorizontal(x, y, xSpeed, ySpeed, bulletxSpeed, bulletDamage);
        }
        else
        {
            bulletsShot++;
        }

    }

    void CheckBåtigAttackUp()
    {

        if (bulletsShot >= båtThreshold)
        {
            bulletsShot = 0;
            BåtigBulletVertical(x, y, xSpeed, ySpeed, bulletxSpeed, bulletDamage);
        }
        else
        {
            bulletsShot++;
        }

    }

    public static void BåtigBulletHorizontal(float x, float y, float xSpeed, float ySpeed, float bulletSpeed, float damage)
    {
        float calcSpeed = Math.Abs(bulletSpeed * 0.69f) + xSpeed;
        float calcDamage = damage + damage * (calcSpeed / 100);
        BåtBullet.PlayerShoot(x, y, 110.2f, 50, calcSpeed, ySpeed * 0.67f, 1999, calcDamage);
        // System.Console.WriteLine(calcDamage);
    }

    public static void BåtigBulletVertical(float x, float y, float xSpeed, float ySpeed, float bulletSpeed, float damage)
    {
        float calcSpeed = Math.Abs(bulletSpeed * 0.69f) + ySpeed;
        float calcDamage = damage + damage * (calcSpeed / 100);
        BåtBullet.PlayerShoot(x, y, 110.2f, 50, xSpeed * 0.67f, calcSpeed, 1999, calcDamage);
        // System.Console.WriteLine(calcDamage);
    }


    public KarimPlayer(ControlLayout controlLayout) : base(controlLayout)
    {
        x = 400;
        y = 450;
        width = 90;
        height = 90;
        maxHP = 19;
        hp = maxHP;
        name = "Karim Ryde";

        setDashCooldown = 0.59f;
        bulletxSpeed = 1800f;
        bulletWidth = 30f;
        bulletHeight = 20f;
        jumpForce = 1450;
        dashSpeed = 2000;
        setDashDuration = 0.21f;

        spriteFilePath = @"./Sprites/karimryde-scaled-600x600.jpg";

        bulletDamage = 5;

        shoot += CheckBåtigAttack;
        upShoot += CheckBåtigAttackUp;


    }
}