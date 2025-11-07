class Karim : Boss
{
    float shootCooldown = 0;

    // konstanter
    readonly float moveSpeed;
    readonly float gravity = 2300f;

    readonly Color color = new Color(60, 255, 125, 255);

    // bullet konstanter
    readonly float setShootCooldown = 1f;
    readonly float bulletWidth = 80;
    readonly float bulletHeight = 40;
    readonly float bulletDamage = 2;



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
            new EnemyBullet(x, y, bulletWidth, bulletHeight, -1000f, 0f, 0f,bulletDamage);
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
    public override void Despawn()
    {
        Setup.currentlyGibbing = false; 
    }

    public Karim(int x, int y)
    {
        this.x = x;
        this.y = y;
        screenSizeX = 1600;
        screenSizeY = 900;
        width = 250;
        height = 250;
        gameList.Add(this);
        moveSpeed = 500f;
        maxHP = 600;
        hp = maxHP;
        sprite = Raylib.LoadTexture(@"./Sprites/karimryde-scaled-600x600.jpg");
        sprite = ChangeSpriteSize(sprite,(int)width,(int)height);
        objectIdentifier = "enemy";
    }


}