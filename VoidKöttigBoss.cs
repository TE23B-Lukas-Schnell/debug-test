class VoidKöttigBoss : Boss
{
    void Moving(float value, float minValue, float maxValue)
    {
        if (xSpeed == 0) xSpeed = moveSpeed;

        if (value >= maxValue)
        {
            xSpeed = -Math.Abs(moveSpeed);
        }
        else if (value <= minValue)
        {
            xSpeed = Math.Abs(moveSpeed);
        }
    }

    async Task JumpingAttack(CancellationToken ct)
    {
        Color temp = color;
        float contactDamageTemp = contactDamage;
        contactDamage = 6;
        for (int i = 0; i < 255; i++)
        {
            color = new Color(i, 67, 67);

        }
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

    async Task SläckaNerLockenAttackenPlusBåtigInputPåslutet(CancellationToken ct)
    {
        xSpeed = 0;
        ySpeed = 0;
        Color temp = color;
        color = new Color(0, 128, 128);
        await Wait(600, ct);

        int amountOfBullets = 67;

        xSpeed = 0;

        await Wait(400, ct);

        for (int i = 0; i < amountOfBullets; i++)
        {
            new BåtBullet(x + width / 2, y + height / 2, bulletHeight * 2, bulletWidth * 1.8f, -(1100 - (i * 70)), 1000, 1700, bulletDamage);
            await Wait(140, ct, false);
        }

        color = temp;

        await Wait(400, ct);
        Console.ReadKey(true); // Checker för båtig input på slutet
    }

    async Task TeknikarDuschen(CancellationToken ct)
    {
        xSpeed = 0;
        ySpeed = 0;
        Color temp = color;
        color = new Color(0, 234, 14);
        await Wait(367, ct);

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
            await Wait(20, ct, false);
        }

        color = temp;
        gravity = tempGravity;
        await Wait(100, ct);
    }

    async Task DåKanViSläckaNerLocken(CancellationToken ct)
    {
        Console.WriteLine("Då kan ni va snälla och släcka ner locken");
        Environment.Exit(0);
    }

    void InitializeDelegates()
    {
        bossAttacks.Add(JumpingAttack);
        bossAttacks.Add(SpiralAttack);
        bossAttacks.Add(SläckaNerLockenAttackenPlusBåtigInputPåslutet);
        bossAttacks.Add(TeknikarDuschen);
        bossAttacks.Add(DåKanViSläckaNerLocken);
    }

    public override void MoveCycle()
    {
        Moving(x, 1200, screenSizeX - width);

        Console.WriteLine("Hej jag heter anton");
        if (MathF.Log10(5) == MathF.Log10(5))
        {
            Console.WriteLine("Köttig text som skrivs här hej jag håller på att heta anton");
        }
    }

    public VoidKöttigBoss()
    {
        screenSizeX = (int)(1700 / 3);
        screenSizeY = (int)(867 / 3);
        width = 255;
        height = 200;
        x = screenSizeX;
        y = screenSizeY / 2;
        maxHP = 600;
        hp = maxHP;
        objectIdentifier = "enemy";
        contactDamage = 3;
        contactDamageHitboxSizeRatio = 0.72f;
        moveSpeed = 1200;
        gravity = 2300;
        jumpForce = 6767;

        InitializeDelegates();

        bulletWidth = 67;
        bulletHeight = 67;
        bulletDamage = 67;
        waitMultiplier = 0.6f;
        name = "Ulf Beck";
        attackDelay = 20;

        spriteFilePath = @"./Sprites/ulfbeck-scaled-600x427.jpg";
    }
}