class Nathalie : Boss
{
    // konstanter
    public float moveSpeed;
    public float gravity = 2300f;

    Color color = new Color(60, 255, 125, 255);

    // bullet konstanter
    public float setShootCooldown = 1f;
    public float bulletWidth = 80;
    public float bulletHeight = 40;
    public float bulletDamage = 2;

    void MoveCycle(float value, float minValue, float maxValue)
    {
        if (value >= maxValue)
        {
            xSpeed = -Math.Abs(moveSpeed);
        }
        else if (value <= minValue)
        {
            xSpeed = Math.Abs(moveSpeed);
        }
    }

    void StraightLaser()
    {
        new EnemyBullet(x, y, bulletWidth, bulletHeight, -1000f, 0f, 0f, bulletDamage);
    }

    void CurvedLaser()
    {

        new EnemyBullet(x, y, bulletWidth, bulletHeight, -1000f, 500f, 1000f, bulletDamage);
    }

    async Task LaserAttack(float damage, CancellationToken ct)
    {
        await Task.Delay(1000, ct);
        StraightLaser();

        // eftersom bullets skapas medans gameobject listan iteraras så kraschar programmet ibland
        await Task.Delay(1000, ct);
        CurvedLaser();
    }

    async Task SpiralAttack(float damage, CancellationToken ct)
    {
        Color temp = color;
        color = new Color(255, 200, 0);
        await Task.Delay(800, ct);
        for (int i = 0; i < 16; i++)
        {
            new EnemyBullet(x, y, 20, 20, -800f, (float)Math.Cos(i) * 300f, 0f, damage);
            await Task.Delay(100, ct);
        }
        color = temp;
        await Task.Delay(400, ct);
    }

    void initializeDelegates()
    {
        bossAttacks.Add(LaserAttack);
        bossAttacks.Add(SpiralAttack);
    }

    public override void Update()
    {
        ChooseAttack();

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
        sprite = Raylib.LoadTexture(@"./Sprites/nathaliezack-scaled-600x469.jpg");
        sprite = ChangeSpriteSize(sprite, (int)width, (int)height);
    }

    public override void TakenDamage()
    {

    }

    public Nathalie()
    {
        x = 1400;
        y = (int)(Raylib.GetScreenHeight() * 0.5f);
        screenSizeX = 1600;
        screenSizeY = 900;
        width = 600 / 2;
        height = 469 / 2;
        moveSpeed = 500f;
        maxHP = 600;
        hp = maxHP;
        objectIdentifier = "enemy";
        contactDamage = 5;

        initializeDelegates();

        AddToGameList(this);
    }
}