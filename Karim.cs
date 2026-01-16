class Karim : Boss
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
        color = new Color(200, 35, 35);
        xSpeed = 0;
        ySpeed = 0;
        await Wait(700, ct);

        xSpeed = 0;
        ySpeed = 0;
        xSpeed -= moveSpeed * 1.5f;
        ySpeed += jumpHeight;


        while (x != 0)
        {

        }
        xSpeed = 0;
        await Wait(300, ct);


        xSpeed += moveSpeed * 1.5f;
        ySpeed += jumpHeight * 1.5f;


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
        color = new Color(255, 200, 0);


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

    async Task BåtAttack(CancellationToken ct)
    {
        xSpeed = 0;
        ySpeed = 0;
        Color temp = color;
        color = new Color(0, 128, 128);
        await Wait(600, ct);

        int amountOfBullets = 10;

        xSpeed = 0;

        // xSpeed -= moveSpeed * 1.3f;
    
        await Wait(400, ct);

        for (int i = 0; i < amountOfBullets; i++)
        {
            new EnemyBullet(x + width / 2, y + height / 2, bulletHeight, bulletWidth, -(1100 - (i * 30)), 1000, 1700, bulletDamage, true);
            await Wait(190, ct, false);
        }



        color = temp;
 
        await Wait(400, ct);
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
        ySpeed += jumpHeight * 1.5f;

        while (x >= screenSizeX / 2)
        {

        }
        await Wait(367, ct, false);
        ySpeed = 0;

        float tempGravity = gravity;
        gravity = 0;

        for (int i = 0; i < amountOfBullets; i++)
        {
            new EnemyBullet(x, y, bulletWidth, bulletHeight, (float)Math.Cos(i) * 100, 0, 1700f, bulletDamage * 2, true);
            await Wait(50, ct, false);
        }

        color = temp;
        gravity = tempGravity;
        await Wait(400, ct);
    }

    void InitializeDelegates()
    {
        bossAttacks.Add(JumpingAttack);
        bossAttacks.Add(SpiralAttack);
        bossAttacks.Add(BåtAttack);
        bossAttacks.Add(TeknikarDuschen);
    }

    public override void Update()
    {
        ChooseAttack();

        if (notAttacking)
        {
            MoveCycle();
        }

        ContactDamage(contactDamage, "player");
        MoveObject(gravity);
    }

    public override void Despawn()
    {
        GibbManager.currentlyGibbing = false;
        cancellationToken?.Cancel();
    }

    public override void Draw()
    {
        // Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, color);
        DrawTexture(sprite, color);
        ShowHitboxes();
        Raylib.DrawRectangle(50, 50, (int)hp, 50, Color.White);
        DisplayHealthBar(50, 50, 1);
    }

    public override void BeginDraw()
    {
        sprite = Raylib.LoadTexture(@"./Sprites/karimryde-scaled-600x600.jpg");
        sprite = ChangeSpriteSize(sprite, (int)width, (int)height);
    }

    public override void TakenDamage()
    {

    }

    public override void MoveCycle()
    {
        Moving(x, 1200, screenSizeX - width);
    }

    public Karim()
    {
        screenSizeX = 1800;
        screenSizeY = 930;
        width = 250;
        height = 250;
        x = screenSizeX;
        y = screenSizeY / 2;
        maxHP = 600;
        hp = maxHP;
        objectIdentifier = "enemy";
        contactDamage = 3;
        moveSpeed = 600;
        gravity = 2300;
        jumpHeight = 1200;

        InitializeDelegates();

        bulletWidth = 23;
        bulletHeight = 60;
        bulletDamage = 4;
        waitMultiplier = 1;

        attackDelay = 1750;

        AddToGameList(this);
    }
}