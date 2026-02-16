class ChristianBoss : Boss
{

    public float jumpLine = 500f;
    public float moveLine = 207f;


    void Moving(float value, float minValue, float maxValue, ref float changeValue)
    {
        if (changeValue == 0) changeValue = moveSpeed;

        if (value >= maxValue)
        {
            changeValue = -Math.Abs(moveSpeed);
        }
        else if (value <= minValue)
        {
            changeValue = Math.Abs(moveSpeed);
        }
    }

    async Task DashAttack(CancellationToken ct)
    {
        xSpeed = 0;
        ySpeed = 0;
        Color temp = color;
        color = new Color(12, 170, 200);

        while (x <= screenSizeX - width)
        {
            ySpeed = 0;
            xSpeed = moveSpeed;
        }
        await Wait(1200, ct);
        contactDamage *= 2;

        KemiBullet.EnemyShoot(bulletWidth * 2.5f, 0, bulletWidth * 2.5f, bulletHeight * 2, 0, 0, 1800f, bulletDamage, true);

        KemiBullet.EnemyShoot(screenSizeX / 2 - bulletWidth * 2.5f, 0, bulletWidth * 2.5f, bulletHeight * 2, 0, 0, 1800f, bulletDamage, true);

        KemiBullet.EnemyShoot(screenSizeX - bulletWidth * 2.5f, 0, bulletWidth * 2.5f, bulletHeight * 2, 0, 0, 1800f, bulletDamage, true);

        await Wait(230, ct);

        xSpeed = -moveSpeed * 2;

        while (x >= 0)
        {

        }

        color = temp;
        await Wait(600, ct);
        contactDamage /= 2;
        ySpeed += jumpForce;
    }

    async Task BombPlan(CancellationToken ct)
    {
        float tempJumpLine = jumpLine;

        jumpLine = 569;

        Color temp = color;
        color = new Color(128, 0, 128);

        {
            float tempXspeed = xSpeed;
            float tempGravity = gravity;

            gravity = 0;
            xSpeed = 0;
            ySpeed = 0;

            await Wait(1000, ct);

            xSpeed = tempXspeed;
            gravity = tempGravity;
        }

        int attacks = 6;
        bool jumpLeft = true;

        for (int i = 0; i < attacks; i++)
        {

            await Wait(100, ct);

            KemiBullet.EnemyShoot(x, y, bulletWidth * 2.5f, bulletHeight * 2, 1 + xSpeed / 2, 112, 2300f, bulletDamage, false);

            await Wait(267, ct);

            if (y > jumpLine) ySpeed = jumpForce;
            else ySpeed = jumpForce / 2;

            if (jumpLeft) xSpeed = -moveSpeed * 1.2f;
            else xSpeed = moveSpeed * 1.2f;


            jumpLeft = !jumpLeft;

            await Wait(450, ct);
        }

        {
            float tempGravity = gravity;

            gravity = 0;
            xSpeed = 0;
            ySpeed = 0;

            await Wait(550, ct);

            KemiBullet.ChristianShoot(x, y, bulletWidth * 10, bulletHeight * 6, 0, 0, 670f, bulletDamage * 1.5f, true);

            await Wait(800, ct);
            gravity = tempGravity;
        }
        color = temp;
        jumpLine = tempJumpLine;
    }

    async Task TeknikarDuschen(CancellationToken ct)
    {
        xSpeed = 0;
        ySpeed = 0;
        Color temp = color;
        color = new Color(0, 234, 14);
        await Wait(600, ct);

        int amountOfBullets = 15;

        xSpeed = 0;
        ySpeed = 0;
        xSpeed -= moveSpeed * 1.5f;
        ySpeed += jumpForce * 1.5f;

        while (x >= screenSizeX / 2)
        {

        }
        await Wait(367, ct, false);
        ySpeed = 0;

        float tempGravity = gravity;
        gravity = 0;

        for (int i = 0; i < amountOfBullets; i++)
        {
            new BossBullet(x, y, bulletWidth, bulletHeight, (float)Math.Cos(i) * 100, 0, 1700f, bulletDamage * 1.6f, true);
            await Wait(50, ct, false);
        }

        color = temp;
        gravity = tempGravity;
        await Wait(400, ct);
    }

    void InitializeDelegates()
    {
        bossAttacks.Add(DashAttack);
        bossAttacks.Add(BombPlan);
        // bossAttacks.Add(TeknikarDuschen);
    }

    public override void MoveCycle()
    {
        Moving(x, moveLine, screenSizeX - width, ref xSpeed);

        if (y > jumpLine && ySpeed <= 0)
        {
            ySpeed += jumpForce;
        }
    }

    public ChristianBoss()
    {
        screenSizeX = 1200;
        screenSizeY = 1000;
        width = 280;
        height = 280;
        x = screenSizeX;
        y = screenSizeY / 2;
        maxHP = 500;
        hp = maxHP;
        objectIdentifier = "enemy";
        contactDamage = 2.6f;
        contactDamageHitboxSizeRatio = 0.6f;
        moveSpeed = 469;
        gravity = 2100;
        jumpForce = 1500;

        InitializeDelegates();

        bulletWidth = 25;
        bulletHeight = 60;
        bulletDamage = 4.4f;
        waitMultiplier = 1;
        name = "Christian the Killiner";
        attackDelay = 1700;

        spriteFilePath = @"./Sprites/christiankilliner-scaled-600x600.jpg";
    }
}