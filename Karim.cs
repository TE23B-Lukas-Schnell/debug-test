class Karim : Boss
{
    float shootCooldown = 0;

    // konstanter
    public float moveSpeed;
    public float gravity = 2300f;

    readonly Color color = new Color(60, 255, 125, 255);

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
        shootCooldown = setShootCooldown;
        new EnemyBullet(x, y, bulletWidth, bulletHeight, -1000f, 0f, 0f, bulletDamage);
    }

    void CurvedLaser()
    {
        shootCooldown = setShootCooldown;
        new EnemyBullet(x, y, bulletWidth, bulletHeight, -1000f, 500f, 1000f, bulletDamage);
    }

    CancellationTokenSource laserCts;

    async Task HandleLasers(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            CurvedLaser();
            System.Console.WriteLine("curved bullet spawned");
            await Task.Delay(1000, token);

            StraightLaser();
            System.Console.WriteLine("straight bullet spawned");
            await Task.Delay(1000, token);
        }
    }

    public override void Update()
    {
        if (laserCts == null)
        {
            laserCts = new CancellationTokenSource();
            _ = HandleLasers(laserCts.Token);
        }

        ContactDamage(contactDamage, "player");
        MoveObject(gravity);
    }

    public override void Despawn()
    {
        GibbManager.currentlyGibbing = false;
        laserCts?.Cancel();
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

    public Karim()
    {
        x = 1400;
        y = (int)(Raylib.GetScreenHeight() * 0.5f);
        screenSizeX = 1600;
        screenSizeY = 900;
        width = 250;
        height = 250;
        gameList.Add(this);
        moveSpeed = 500f;
        maxHP = 600;
        hp = maxHP;
        objectIdentifier = "enemy";
        contactDamage = 5;
    }


}