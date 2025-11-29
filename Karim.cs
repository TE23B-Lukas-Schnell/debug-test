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

    void moveCycle(float value, float minValue, float maxValue)
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

    void straightLaser()
    {
        if (shootCooldown <= 0)
        {
            shootCooldown = setShootCooldown;
            new EnemyBullet(x, y, bulletWidth, bulletHeight, -1000f, 0f, 0f, bulletDamage);
        }

    }

    void curvedLaser()
    {
        if (shootCooldown <= 0)
        {
            shootCooldown = setShootCooldown;
            new EnemyBullet(x, y, bulletWidth, bulletHeight, -1000f, 500f, 1000f, bulletDamage);
        }
    }

    public override void Update()
    {
        shootCooldown = MathF.Max(shootCooldown - Raylib.GetFrameTime(), 0);

        curvedLaser();
        // straightLaser();

        ContactDamage(1, "player");

        moveCycle(x, Raylib.GetScreenWidth() * 0.7f, Raylib.GetScreenWidth() - width);
        MoveObject(gravity);
    }
    public override void Draw()
    {
        // Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, color);
        DrawTexture(sprite, color);
        ShowHitboxes();
        Raylib.DrawRectangle(50, 50, (int)hp, 50, Color.Green);
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
    public override void Despawn()
    {
        GibbManager.currentlyGibbing = false;
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
    }


}