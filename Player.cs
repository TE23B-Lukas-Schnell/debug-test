class Player : MoveableObject
{

    //statiska variabler
    public static int score = 0;

    // public static List<Items> Inventory;

    float dashDuration = 0;
    float dashCooldown = 0;
    float shootCooldown = 0;
    float dashSpeed = 2000f;

    //konstanter
    float gravity = 2300f;
    float moveSpeed = 900f;
    float jumpForce = 1300f;
    float setDashDuration = 0.2f;
    float setDashCooldown = 0.43f;
    float fastFallSpeed = 1400f;
    Color color = new Color(12, 0, 235, 255);

    //bullet konstanter
    float setShootCooldown = 0.5f;
    float bulletWidth = 40;
    float bulletHeight = 20;
    float bulletDamage = 50;
    float bulletSpeed = 1800;
    float bullryGravity = 0;

 
    //keybinds
    bool LeftKeyPressed() => Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left);
    bool RightKeyPressed() => Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right);
    bool DownKeyPressed() => Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down);
    bool UpKeyPressed() => Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up);
    bool JumpKeyPressed() => Raylib.IsKeyDown(KeyboardKey.Space) || Raylib.IsKeyDown(KeyboardKey.Z);
    bool DashKeyPressed() => Raylib.IsKeyDown(KeyboardKey.LeftShift) || Raylib.IsKeyDown(KeyboardKey.C);
    bool ShootKeyPressed() => Raylib.IsKeyDown(KeyboardKey.L) || Raylib.IsKeyDown(KeyboardKey.X);


    //moves the player
    void MovingLeftAndRight()
    {
        if (LeftKeyPressed())
        {
            xSpeed = -moveSpeed;
        }
        else if (RightKeyPressed())
        {
            xSpeed = moveSpeed;
        }
        else xSpeed = 0;
    }
    //makes the player fastfall
    void FastFalling()
    {
        if (DownKeyPressed() && !Grounded())
        {
            ySpeed = -fastFallSpeed;
        }
    }
    //makes the player jump
    public void Jumping()
    {
        if (JumpKeyPressed() && Grounded())
        {
            ySpeed = jumpForce;
        }
    }
    //makes the player dash
    void Dashing()
    {
        if (DashKeyPressed() && dashCooldown == 0)
        {
            if (LeftKeyPressed())
            {
                dashSpeed = -dashSpeed;
                dashDuration = setDashDuration;
            }
            else if (RightKeyPressed())
            {
                dashSpeed = Math.Abs(dashSpeed);
                dashDuration = setDashDuration;
            }
        }
        if (dashDuration > 0)
        {
            xSpeed = dashSpeed;
            ySpeed = 0;
            dashCooldown = setDashCooldown;
        }
    }

    //makes the player shoot
    void Shooting()
    {
        if (ShootKeyPressed() && shootCooldown <= 0 && !UpKeyPressed())
        {
            shootCooldown = setShootCooldown;
            new PlayerBullet(x, y, bulletWidth, bulletHeight, bulletSpeed, 0,bullryGravity, bulletDamage);
        }
        else if (ShootKeyPressed() && shootCooldown <= 0 && UpKeyPressed())
        {
            shootCooldown = setShootCooldown;
            new PlayerBullet(x, y, bulletWidth, bulletHeight, 0, bulletSpeed, bullryGravity, bulletDamage);
        }   
    }

    public override void Update()
    {
        dashCooldown = MathF.Max(dashCooldown - Raylib.GetFrameTime(), 0);
        dashDuration = MathF.Max(dashDuration - Raylib.GetFrameTime(), 0);
        shootCooldown = MathF.Max(shootCooldown - Raylib.GetFrameTime(), 0);

        MovingLeftAndRight();
        FastFalling();
        Jumping();
        Dashing();
        Shooting();

        MoveObject(gravity);
    
    }

    public override void Draw()
    {
        if (dashDuration > 0)
        {
            AddTrailEffects(new Color(22, 15, 55, 255), 100, 100, 0, -1);
        }
        else
        {
            AddTrailEffects(new Color(0, 88, 255, 0), 100, 100, 0, 130);
        }
        DisplayHealthBar(50, 145, 10);
        Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, color);
        ShowHitboxes();
    }
    public override void Despawn()
    {
        Console.WriteLine("köttigaste inputen");
        Setup.currentlyGibbing = false;
    }

    public Player(int x, int y)
    {
        objectIdentifier = "player";
        this.x = x;
        this.y = y;
        width = Setup.windowWidth * 0.05f;
        height = Setup.windowWidth * 0.05f;
        gameList.Add(this);
        maxHP = 20;
        hp = maxHP;
        
    }
}