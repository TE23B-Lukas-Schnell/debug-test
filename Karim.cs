class Karim : Boss
{
    // konstanter
    public float moveSpeed;
    public float gravity = 2300f;

    Color color = new Color(255, 255, 255, 255);

    // bullet konstanter
    public float setShootCooldown = 1f;
    public float bulletWidth = 80;
    public float bulletHeight = 40;
    public float bulletDamage = 2;

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

    async Task JumpingAttack(float damage, CancellationToken ct)
    {
        Color temp = color;
        color = new Color(200, 35, 35);
        await Task.Delay(700, ct);

        xSpeed = 0;
        ySpeed = 0;
        xSpeed -= 800;
        ySpeed += 1200;

        await Task.Delay(20, ct);
        while (x != 0)
        {

        }

        await Task.Delay(1000, ct);
        xSpeed = 0;

        xSpeed += 900;
        ySpeed += 1700;

        await Task.Delay(20, ct);
        while (x != screenSizeX - width)
        {

        }
        xSpeed = 0;
        ySpeed = 0;
        color = temp;
        await Task.Delay(400, ct);
    }

    async Task SpiralAttack(float damage, CancellationToken ct)
    {
        xSpeed = 0;
        ySpeed = 0;

        Color temp = color;
        await Task.Delay(400, ct);
        color = new Color(255, 200, 0);
        await Task.Delay(800, ct);
        for (int i = 0; i < 8; i++)
        {
            new EnemyBullet(x, y, 20, 20, -800f, (float)Math.Cos(i) * 300f, 0f, damage);
            await Task.Delay(100, ct);
        }
        color = temp;
        await Task.Delay(400, ct);
    }

    void InitializeDelegates()
    {
        bossAttacks.Add(JumpingAttack);
        bossAttacks.Add(SpiralAttack);
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
        Moving(x, 900, screenSizeX - width);
    }

    public Karim()
    {
        screenSizeX = 1800;
        screenSizeY = 1000;
        width = 250;
        height = 250;
        x = screenSizeX;
        y = screenSizeY / 2;
        moveSpeed = 500f;
        maxHP = 600;
        hp = maxHP;
        objectIdentifier = "enemy";
        contactDamage = 5;

        InitializeDelegates();

        AddToGameList(this);
    }
}