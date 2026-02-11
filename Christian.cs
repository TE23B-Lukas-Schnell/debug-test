class Christian : Boss
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

    async Task JumpingAttack(CancellationToken ct)
    {
        Color temp = color;
        float contactDamageTemp = contactDamage;
        contactDamage = 6;
        color = new Color(240, 35, 35);
        xSpeed = 0;
        ySpeed = 0;
        await Wait(700, ct);

        xSpeed = 0;
        ySpeed = 0;
        xSpeed -= moveSpeed * 1.5f;
        ySpeed += jumpForce;


        while (x != 0)
        {

        }
        xSpeed = 0;
        await Wait(300, ct);


        xSpeed += moveSpeed * 1.5f;
        ySpeed += jumpForce * 1.5f;


        while (x != screenSizeX - width)
        {
            if (hp < maxHP / 2)
            {

            }
        }
        xSpeed = 0;
        ySpeed = 0;
        color = temp;
        contactDamage = contactDamageTemp;
        await Wait(300, ct, true);
    }

    async Task SpiralAttack(CancellationToken ct)
    {
        xSpeed = 0;
        ySpeed = 0;
        Color temp = color;
        if (hp < maxHP / 2)
        {
            color = new Color(230, 170, 0);
        }
        else
        {
            color = new Color(255, 200, 0);
        }

        int amountOfBullets = 8;
        if (hp < maxHP / 2)
        {
            amountOfBullets = 16;
        }

        await Wait(1000, ct);
        for (int i = 0; i < amountOfBullets; i++)
        {
            new EnemyBullet(x, y, bulletWidth, bulletWidth, -800f, (float)Math.Cos(i) * 300f, 0f, bulletDamage);
            await Wait(100, ct, false);
        }
        color = temp;
        await Wait(400, ct);
    }

    async Task BombPlan(CancellationToken ct)
    {
        float tempJumpLine = jumpLine;

        jumpLine = 567;

        Color temp = color;
        color = new Color(0, 128, 128);

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

            KemiBullet.EnemyShoot(x, y, bulletWidth * 2.5f, bulletHeight * 2, xSpeed / 2, 112, 2300f, bulletDamage, false);

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

            KemiBullet.ChristianShoot(x, y, bulletWidth * 10, bulletHeight * 6, 0, 0, 670f, bulletDamage * 1.5f, false);

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
            new EnemyBullet(x, y, bulletWidth, bulletHeight, (float)Math.Cos(i) * 100, 0, 1700f, bulletDamage * 1.6f, true);
            await Wait(50, ct, false);
        }

        color = temp;
        gravity = tempGravity;
        await Wait(400, ct);
    }

    async Task DåKanViSläckaNerLocken(CancellationToken ct)
    {
        //gör köttig attack här
    }

    void InitializeDelegates()
    {
        // bossAttacks.Add(JumpingAttack);
        // bossAttacks.Add(SpiralAttack);
        bossAttacks.Add(BombPlan);
        // bossAttacks.Add(TeknikarDuschen);
        // bossAttacks.Add(DåKanViSläckaNerLocken);
    }

    public override void MoveCycle()
    {
        Moving(x, moveLine, screenSizeX - width, ref xSpeed);

        if (y > jumpLine && ySpeed <= 0)
        {
            ySpeed += jumpForce;
        }
    }

    public Christian()
    {
        screenSizeX = 1200;
        screenSizeY = 1000;
        width = 255;
        height = 255;
        x = screenSizeX;
        y = screenSizeY / 2;
        maxHP = 500;
        hp = maxHP;
        objectIdentifier = "enemy";
        contactDamage = 3;
        contactDamageHitboxSizeRatio = 0.72f;
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