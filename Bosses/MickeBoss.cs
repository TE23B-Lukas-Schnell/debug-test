class MickeBoss : Boss
{
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
        color = new Color(200, 35, 35);
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
            new BossBullet(x, y, bulletWidth, bulletHeight, (float)Math.Cos(i) * 100, 0, 1700f, bulletDamage * 1.6f);
            await Wait(50, ct, false);
        }

        color = temp;
        gravity = tempGravity;
        await Wait(400, ct);
    }

    void InitializeDelegates()
    {
        bossAttacks.Add(JumpingAttack);
        bossAttacks.Add(TeknikarDuschen);
    }


    public override void MoveCycle()
    {
        Moving(x, 1200, screenSizeX - width, ref xSpeed);
    }

    public MickeBoss()
    {
        screenSizeX = 1800;
        screenSizeY = 900;
        width = 255;
        height = 255;
        x = screenSizeX;
        y = screenSizeY / 2;
        maxHP = 600;
        hp = maxHP;
        objectIdentifier = "enemy";
        contactDamage = 3;
        contactDamageHitboxSizeRatio = 0.72f;
        moveSpeed = 600;
        gravity = 2300;
        jumpForce = 1100;

        InitializeDelegates();

        bulletWidth = 23;
        bulletHeight = 60;
        bulletDamage = 4;
        waitMultiplier = 1;
        name = "Köttiga Mikael";
        attackDelay = 1750;

        spriteFilePath = "./Sprites/lärare/mikaelbergstrom-scaled-600x600.jpg";
    }
}