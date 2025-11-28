class Player : FightableObject
{
    //statiska variabler
    public static int score = 0;
    ControlLayout currentLayout;
    Dictionary<KeyboardKey, bool> keyActions = new Dictionary<KeyboardKey, bool>()
    {
        
    };
    public static List<PlayerStat> playerStats = new List<PlayerStat>();

    //player stats 
    // public PlayerStat gravity = new PlayerStat(playerStats, 2300f, 0, float.MaxValue, "gravity");
    public float gravity = 2300f;
    // public PlayerStat moveSpeed = new PlayerStat(playerStats, 900f, "moveSpeed");
    public float moveSpeed = 900f;
    // public PlayerStat jumpForce = new PlayerStat(playerStats, 1300f, "jumpForce");
    public float jumpForce = 1300f;
    // public PlayerStat setDashDuration = new PlayerStat(playerStats, 0.2f, "dashDuration");
    public float setDashDuration = 0.2f;
    // public PlayerStat setDashCooldown = new PlayerStat(playerStats, 0.43f, "dashCooldown");
    public float setDashCooldown = 0.43f;
    // public PlayerStat fastFallSpeed = new PlayerStat(playerStats, 1400f, "fastFallSpeed");
    public float fastFallSpeed = 1400f;
    // public PlayerStat dashSpeed = new PlayerStat(playerStats, 2000f, "dashSpeed");
    public float dashSpeed = 2000f;

    //bullet stats
    // Projectile playerProjectile = PlayerBullet;
    // public PlayerStat setShootCooldown = new PlayerStat(playerStats, 0.5f, "shootCooldown");
    public float setShootCooldown = 0.5f;
    // public PlayerStat bulletWidth = new PlayerStat(playerStats, 40f, "bulletWidth");
    public float bulletWidth = 40f;
    // public PlayerStat bulletHeight = new PlayerStat(playerStats, 20f, "bulletHeight");
    public float bulletHeight = 20f;
    // public PlayerStat bulletDamage = new PlayerStat(playerStats, 50f, "bulletDamage");
    public float bulletDamage = 50f;
    // public PlayerStat bulletSpeed = new PlayerStat(playerStats, 1800f, "bulletSpeed");
    public float bulletSpeed = 1800f;
    // public PlayerStat bulletGravity = new PlayerStat(playerStats, 0f, "bulletGravity");
    public float bulletGravity = 0f;

    //färger
    public Color color = new Color(0, 0f, 235f, 254f);
    // public PlayerStat colorR = new PlayerStat(playerStats, 12f, "r");
    // public PlayerStat colorG = new PlayerStat(playerStats, 0f, "g");
    // public PlayerStat colorB = new PlayerStat(playerStats, 235f, "b");
    // public PlayerStat colorA = new PlayerStat(playerStats, 254f, "a");


    //variabler
    float dashDuration = 0;
    float dashCooldown = 0;
    float shootCooldown = 0;


    public void PrintPlayerStats()
    {
        Console.WriteLine(@$"Stats:
pos                      {x} {y}
size                     {width} {height}
velocity                 {xSpeed} {ySpeed}
gravity:                 {gravity}
move speed:              {moveSpeed}
jump force:              {jumpForce}
dash duration:           {setDashDuration}
dash cooldown:           {setDashCooldown}
fastfall speed:          {fastFallSpeed}
shoot cooldown:          {setShootCooldown}
bullet width             {bulletWidth}
bullet height            {bulletHeight}
bullet damage            {bulletDamage}
bullet speed             {bulletSpeed}
bullet gravity           {bulletGravity}");

        Console.WriteLine("inventory:");
        foreach (Items items in Inventory)
        {
            Console.WriteLine(items.name);
        }
    }

    //keybinds

    KeyboardKey anton = KeyboardKey.Backspace;
    string köttig = KeyboardKey.Backspace.ToString();

    bool LeftKeyPressed() => Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left);
    bool RightKeyPressed() => Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right);
    bool DownKeyPressed() => Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down);
    bool UpKeyPressed() => Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up);
    bool JumpKeyPressed() => Raylib.IsKeyDown(KeyboardKey.Space) || Raylib.IsKeyDown(KeyboardKey.Z);
    bool DashKeyPressed() => Raylib.IsKeyDown(KeyboardKey.LeftShift) || Raylib.IsKeyDown(KeyboardKey.C);
    bool ShootKeyPressed() => Raylib.IsKeyDown(KeyboardKey.L) || Raylib.IsKeyDown(KeyboardKey.X);

    // List<Dictionary<string, KeyboardKey>> controlSchemes

  

   

    //     gör en skiss och schema på papper innan du gör något dumt!!!11111

    /*

            bool LeftKeyPressed = false;
            bool RightKeyPressed = false;
            bool DownKeyPressed = false;
            bool UpKeyPressed = false;
            bool JumpKeyPressed = false;
            bool DashKeyPressed = false;
            bool ShootKeyPressed = false;

    /*
            Dictionary<KeyboardKey, Action> keybinds = new Dictionary<KeyboardKey, Action>()
            {
                {KeyboardKey.A, () =>
                {

                } },
            };*/

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
            dashSpeed = Math.Abs(dashSpeed);
            if (LeftKeyPressed()) dashSpeed = -dashSpeed;
            dashDuration = setDashDuration;
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



    public override void Update()
    {
        // System.Console.WriteLine(köttig );

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
    //fixa delegatet actionms för alla skaer spelaraen kan göra
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

    public override void BeginDraw()
    {
        
    }

    public override void Despawn()
    {
        Console.WriteLine("spelaren har despawnat");
        GibbManager.currentlyGibbing = false;
    }

    public Player()
    {
        objectIdentifier = "player";
        x = 400;
        y = 450;
        width = 85;
        height = 80;
        gameList.Add(this);
        maxHP = 20;
        hp = maxHP;
    }
}