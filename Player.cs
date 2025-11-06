class Player : MoveableObject
{
    //statiska variabler
    public static int score = 0;

    public List<Items> Inventory = new List<Items>();

    //player stats
    public Property gravity = new Property(2300f);
    public Property moveSpeed = new Property(900f);
    public Property jumpForce = new Property(1300f);
    public Property setDashDuration = new Property(0.2f);
    public Property setDashCooldown = new Property(0.43f);
    public Property fastFallSpeed = new Property(1400f);
    public Property dashSpeed = new Property(2000f);
    Color color = new Color(12, 0, 235, 255);

    //bullet stats
    // Projectile playerProjectile = PlayerBullet;
    public Property setShootCooldown = new Property(0.5f);
    public Property bulletWidth = new Property(40f);
    public Property bulletHeight = new Property(20f);
    public Property bulletDamage = new Property(50f);
    public Property bulletSpeed = new Property(1800f);
    public Property bulletGravity = new Property(0f);


    //variabler
    float dashDuration = 0;
    float dashCooldown = 0;
    float shootCooldown = 0;


    public void PrintPlayerStats()
    {
        Console.WriteLine(@$"Stats:
gravity:                 {gravity.Stat}
move speed:              {moveSpeed.Stat}
jump force:              {jumpForce.Stat}
dash duration:           {setDashDuration.Stat}
dash cooldown:           {setDashCooldown.Stat}
fastfall speed:          {fastFallSpeed.Stat}
shoot cooldown:          {setShootCooldown.Stat}");

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
            xSpeed = -moveSpeed.Stat;
        }
        else if (RightKeyPressed())
        {
            xSpeed = moveSpeed.Stat;
        }
        else xSpeed = 0;
    }
    //makes the player fastfall
    void FastFalling(/*HEJ JAG HETER  ANTON*/)
    {
        if (DownKeyPressed() && !Grounded())
        {
            ySpeed = -fastFallSpeed.Stat;
        }
    }
    //makes the player jump
    void Jumping(/*HEJ JAG HETER  ANTON*/)
    {
        if (JumpKeyPressed() && Grounded())
        {
            ySpeed = jumpForce.Stat;
        }
    }
    //makes the player dash
    void Dashing(/*HEJ JAG HETER  ANTON*/)
    {
        if (DashKeyPressed() && dashCooldown == 0)
        {
            if (LeftKeyPressed())
            {
                dashSpeed.Stat = -dashSpeed.Stat;
                dashDuration = setDashDuration.Stat;
            }
            else if (RightKeyPressed())
            {
                dashSpeed.Stat = Math.Abs(dashSpeed.Stat);
                dashDuration = setDashDuration.Stat;
            }
        }
        if (dashDuration > 0)
        {
            xSpeed = dashSpeed.Stat;
            ySpeed = 0;
            dashCooldown = setDashCooldown.Stat;
        }
    }
    //makes the player shoot
    void Shooting(/*HEJ JAG HETER  ANTON*/)
    {
        if (ShootKeyPressed() && shootCooldown <= 0 && !UpKeyPressed())
        {
            shootCooldown = setShootCooldown.Stat;
            new PlayerBullet(x, y, bulletWidth.Stat, bulletHeight.Stat, bulletSpeed.Stat, 0, bulletGravity.Stat, bulletDamage.Stat);
        }
        else if (ShootKeyPressed() && shootCooldown <= 0 && UpKeyPressed())
        {
            shootCooldown = setShootCooldown.Stat;
            new PlayerBullet(x, y, bulletWidth.Stat, bulletHeight.Stat, 0, bulletSpeed.Stat, bulletGravity.Stat, bulletDamage.Stat);
        }
    }

    public void ApplyBuffsFromItem()
    {
        if (Inventory.Count > 0)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                Console.WriteLine($"now applying buffs to player: {Inventory[i].name}");
                Inventory[i].ApplyBuff();
            }
        }
        else Console.WriteLine("tomt inventory");
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

        MoveObject(gravity.Stat);
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