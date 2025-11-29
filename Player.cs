class Player : FightableObject
{
    //statiska variabler
    public static int score = 0;

    public Dictionary<string, bool> keyPressed = new Dictionary<string, bool>()
    {
        {"up", false },
        {"down", false },
        {"left", false },
        {"right", false },
        {"jump", false },
        {"dash", false },
        {"shoot", false },
    };

    public Action moveLeft;

    ControlLayout currentLayout;

    //player stats 
    public float gravity = 2300f;
    public float moveSpeed = 900f;
    public float jumpForce = 1300f;
    public float setDashDuration = 0.2f;
    public float setDashCooldown = 0.43f;
    public float fastFallSpeed = 1400f;
    public float dashSpeed = 2000f;
    public Color color = new Color(0, 0f, 235f, 254f);

    //bullet stats
    public float setShootCooldown = 0.5f;
    public float bulletWidth = 40f;
    public float bulletHeight = 20f;
    public float bulletDamage = 50f;
    public float bulletxSpeed = 1800f;
    public float bulletySpeed = 0f;
    public float bulletGravity = 0f;

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
bullet speed             {bulletxSpeed} {bulletySpeed}
bullet gravity           {bulletGravity}");

        Console.WriteLine("inventory:");
        foreach (Items items in Inventory)
        {
            Console.WriteLine(items.name);
        }
    }




    //moves the player
    void MovingLeftAndRight(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["left"])
        {
            xSpeed = -moveSpeed;
        }
        else if (keyPressed["right"])
        {
            xSpeed = moveSpeed;
        }
        else xSpeed = 0;
    }
    //makes the player fastfall
    void FastFalling(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["down"] && !Grounded())
        {
            ySpeed = -fastFallSpeed;
        }
    }
    //makes the player jump
    void Jumping(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["jump"] && Grounded())
        {
            ySpeed = jumpForce;
        }
    }
    //makes the player dash
    void Dashing(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["dash"] && dashCooldown == 0)
        {
            dashSpeed = Math.Abs(dashSpeed);
            if (keyPressed["left"]) dashSpeed = -dashSpeed;
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
        if (keyPressed["shoot"] && shootCooldown <= 0 && !keyPressed["up"])
        {
            shootCooldown = setShootCooldown;
            new PlayerBullet(x, y, bulletWidth, bulletHeight, bulletxSpeed, bulletySpeed, bulletGravity, bulletDamage);
        }
        else if (keyPressed["shoot"] && shootCooldown <= 0 && keyPressed["up"])
        {
            shootCooldown = setShootCooldown;
            new PlayerBullet(x, y, bulletWidth, bulletHeight, bulletySpeed, bulletxSpeed, bulletGravity, bulletDamage);
        }
    }

    public override void Update()
    {
        // System.Console.WriteLine(köttig );

        // inputigt värre här
        for (int i = 0; i < currentLayout.keybinds.Keys.Count; i++)
        {
            string currentKey = keyPressed.Keys.ToArray()[i];

            if (Raylib.IsKeyDown(currentLayout.keybinds[currentKey]))
            {
                Console.WriteLine(currentKey + " key pressed");
                keyPressed[currentKey] = true;
            }
            else
            {
                keyPressed[currentKey] = false;
            }

        }

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

    public override void BeginDraw()
    {

    }

    public override void Despawn()
    {
        Console.WriteLine("spelaren har despawnat");
        GibbManager.currentlyGibbing = false;
    }

    public Player(ControlLayout controlLayout)
    {
        objectIdentifier = "player";
        x = 400;
        y = 450;
        width = 85;
        height = 80;
        gameList.Add(this);
        maxHP = 20;
        hp = maxHP;
        currentLayout = controlLayout;

    }
}