class Player : MoveableObject
{
    //statiska variabler
    public static int score = 0;

    public List<Items> Inventory = new List<Items>();

    public static List<PlayerStat> playerStats = new List<PlayerStat>();

    //player stats 
    public PlayerStat gravity = new PlayerStat(playerStats,2300f);
    public PlayerStat moveSpeed = new PlayerStat(playerStats,900f);
    public PlayerStat jumpForce = new PlayerStat(playerStats,1300f);
    public PlayerStat setDashDuration = new PlayerStat(playerStats,0.2f);
    public PlayerStat setDashCooldown = new PlayerStat(playerStats,0.43f);
    public PlayerStat fastFallSpeed = new PlayerStat(playerStats,1400f);
    public PlayerStat dashSpeed = new PlayerStat(playerStats,2000f);
   

    //bullet stats
    // Projectile playerProjectile = PlayerBullet;
    public PlayerStat setShootCooldown = new PlayerStat(playerStats,0.5f);
    public PlayerStat bulletWidth = new PlayerStat(playerStats,40f);
    public PlayerStat bulletHeight = new PlayerStat(playerStats,20f);
    public PlayerStat bulletDamage = new PlayerStat(playerStats,50f);
    public PlayerStat bulletSpeed = new PlayerStat(playerStats,1800f);
    public PlayerStat bulletGravity = new PlayerStat(playerStats,0f);

    //färger
    public PlayerStat colorR = new PlayerStat(playerStats,12);
    public PlayerStat colorG = new PlayerStat(playerStats, 0);
    public PlayerStat colorB = new PlayerStat(playerStats, 235);
    public PlayerStat colorA = new PlayerStat(playerStats,254);


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
        Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, new Color(colorR.Stat,colorG.Stat,colorB.Stat,colorA.Stat));
        ShowHitboxes();
    }
    public override void Despawn()
    {
        Console.WriteLine("köttigaste inputen");
        GibbManager.currentlyGibbing = false;
    }

    public Player()
    {
        objectIdentifier = "player";
        x = 800;
        y = 450;
        width = GibbManager.windowWidth * 0.05f;
        height = GibbManager.windowWidth * 0.05f;
        gameList.Add(this);
        maxHP = 20;
        hp = maxHP;
    }
}