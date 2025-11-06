class Player : MoveableObject
{
    //statiska variabler
    public static int score = 0;

    public List<Items> Inventory = new List<Items>();

    //player stats
    public float gravity = 2300f;
    public float moveSpeed = 900f;
    public float jumpForce = 1300f;
    public float setDashDuration = 0.2f;
    public float setDashCooldown = 0.43f;
    public float fastFallSpeed = 1400f;
    public float dashSpeed = 2000f;
    Color color = new Color(12, 0, 235, 255);


    //bullet stats
    // Projectile playerProjectile = PlayerBullet;
    public float setShootCooldown = 0.5f;
    public float bulletWidth = 40;
    public float bulletHeight = 20;
    public float bulletDamage = 50;
    public float bulletSpeed = 1800;
    public float bulletGravity = 0;

    //variabler
    float dashDuration = 0;
    float dashCooldown = 0;
    float shootCooldown = 0;


    public void PrintPlayerStats()
    {
        Console.WriteLine(@$"Stats:
gravity:                 {gravity}
move speed:              {moveSpeed}
jump force:              {jumpForce}
dash duration:           {setDashDuration}
dash cooldown:           {setDashCooldown}
fastfall speed:          {fastFallSpeed}
shoot cooldown:          {setShootCooldown}");

        Console.WriteLine("inventory:");
        foreach (Items items in Inventory)
        {
            Console.WriteLine(items.name);
        }
    }

    //keybinds
    bool LeftKeyPressed() => Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left);
    bool RightKeyPressed() => Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right);
    bool DownKeyPressed() => Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down);
    bool UpKeyPressed() => Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up);
    bool JumpKeyPressed() => Raylib.IsKeyDown(KeyboardKey.Space) || Raylib.IsKeyDown(KeyboardKey.Z);
    bool DashKeyPressed() => Raylib.IsKeyDown(KeyboardKey.LeftShift) || Raylib.IsKeyDown(KeyboardKey.C);
    bool ShootKeyPressed() => Raylib.IsKeyDown(KeyboardKey.L) || Raylib.IsKeyDown(KeyboardKey.X);


    //moves the player
    void MovingLeftAndRight(/*HEJ JAG HETER  ANTON*/)
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
    void FastFalling(/*HEJ JAG HETER  ANTON*/)
    {
        if (DownKeyPressed() && !Grounded())
        {
            ySpeed = -fastFallSpeed;
        }
    }
    //makes the player jump
    void Jumping(/*HEJ JAG HETER  ANTON*/)
    {
        if (JumpKeyPressed() && Grounded())
        {
            ySpeed = jumpForce;
        }
    }
    //makes the player dash
    void Dashing(/*HEJ JAG HETER  ANTON*/)
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
    void Shooting(/*HEJ JAG HETER  ANTON*/)
    {
        if (ShootKeyPressed() && shootCooldown <= 0 && !UpKeyPressed())
        {
            shootCooldown = setShootCooldown;
            new PlayerBullet(x, y, bulletWidth, bulletHeight, bulletSpeed, 0, bulletGravity, bulletDamage);
        }
        else if (ShootKeyPressed() && shootCooldown <= 0 && UpKeyPressed())
        {
            shootCooldown = setShootCooldown;
            new PlayerBullet(x, y, bulletWidth, bulletHeight, 0, bulletSpeed, bulletGravity, bulletDamage);
        }
    }

    public void ApplyBuffsFromItem(/*HEJ JAG HETER  ANTON*/)
    {
        if (Inventory.Count > 0)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                Console.WriteLine($"now applying buffs to player: {Inventory[i].name}");
                Inventory[i].ApplyBuff();
            }
        }else Console.WriteLine("tomt inventory");
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

    public Player()
    {
        objectIdentifier = "player";
        x = 800;
        y = 450;
        width = Setup.windowWidth * 0.05f;
        height = Setup.windowWidth * 0.05f;
        gameList.Add(this);
        maxHP = 20;
        hp = maxHP;

    }
}