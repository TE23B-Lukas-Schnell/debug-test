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

    ControlLayout currentLayout;

    //player actions
    public Action moveLeft;
    public Action moveRight;
    public Action notmoving;
    public Action fastFall;
    public Action jump;
    public Action leftDash;
    public Action rightDash;
    public Action duringDash;
    public Action shoot;
    public Action upShoot;
    public Action takenDamage;

    //player stats 
    public float gravity = 2300f;
    public float moveSpeed = 900f;
    public float jumpForce = 1300f;
    public float setDashDuration = 0.2f;
    public float setDashCooldown = 0.43f;
    public float fastFallSpeed = 1400f;
    public float dashSpeed = 2000f;
    public float invincibilityTime = 1f;
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
    //makes all the delegates work 
    void InitializeDelegates(/*HEJ JAG HETER  ANTON*/)
    {
        moveLeft += () => xSpeed = -moveSpeed;
        moveRight += () => xSpeed = moveSpeed;
        notmoving += () => xSpeed = 0;
        fastFall += () => ySpeed = -fastFallSpeed;
        jump += () => ySpeed = jumpForce;
        leftDash += () => dashSpeed = -Math.Abs(dashSpeed);
        rightDash += () => dashSpeed = Math.Abs(dashSpeed);
        duringDash += () =>
        {
            xSpeed = dashSpeed;
            ySpeed = 0;
            dashCooldown = setDashCooldown;
        };
        shoot += () =>
        {
            shootCooldown = setShootCooldown;
            new PlayerBullet(x, y, bulletWidth, bulletHeight, bulletxSpeed, bulletySpeed, bulletGravity, bulletDamage);
        };
        upShoot += () =>
        {
            shootCooldown = setShootCooldown;
            new PlayerBullet(x, y, bulletWidth, bulletHeight, bulletySpeed, bulletxSpeed, bulletGravity, bulletDamage);
        };
        takenDamage += () => invincibilityDuration = invincibilityTime;

    }
    //checks if the player is pressing the move keys
    void MoveCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["left"])
        {
            moveLeft();
        }
        else if (keyPressed["right"])
        {
            moveRight();
        }
        else notmoving();
    }
    // checks if the player is fastfalling
    void FastFallCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["down"] && !Grounded())
        {
            fastFall();
        }
    }
    // checks if hte player is jumping 
    void JumpCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["jump"] && Grounded())
        {
            jump();
        }
    }
    //checks if the player is pressing the dash key
    void DashCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["dash"] && dashCooldown == 0)
        {
            if (keyPressed["left"])
            {
                leftDash();
            }
            else
            {
                rightDash();
            }
            dashDuration = setDashDuration;
        }
        if (dashDuration > 0)
        {
            duringDash();
        }
    }
    //checks if the shoot key is pressed 
    void ShootCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["shoot"] && shootCooldown <= 0 && !keyPressed["up"])
        {
            shoot();
        }
        else if (keyPressed["shoot"] && shootCooldown <= 0 && keyPressed["up"])
        {
            upShoot();
        }
    }
    //check the players inputs every frame
    void Checkinputs(/*HEJ JAG HETER  ANTON*/)
    {
        // inputigt värre här
        for (int i = 0; i < currentLayout.keybinds.Keys.Count; i++)
        {
            string currentKey = keyPressed.Keys.ToArray()[i];

            if (Raylib.IsKeyDown(currentLayout.keybinds[currentKey]))
            {
                // Console.WriteLine(currentKey + " key pressed");
                keyPressed[currentKey] = true;
            }
            else
            {
                keyPressed[currentKey] = false;
            }

        }

    }

    public override void Update()
    {
        // System.Console.WriteLine(köttig );

        dashCooldown = MathF.Max(dashCooldown - Raylib.GetFrameTime(), 0);
        dashDuration = MathF.Max(dashDuration - Raylib.GetFrameTime(), 0);
        shootCooldown = MathF.Max(shootCooldown - Raylib.GetFrameTime(), 0);
        invincibilityDuration = MathF.Max(invincibilityDuration - Raylib.GetFrameTime(), 0);

        Checkinputs();

        MoveCheck();
        FastFallCheck();
        JumpCheck();
        DashCheck();
        ShootCheck();

        MoveObject(gravity);
    }

    public override void Draw()
    {
        maxTrailSize = (int)MathF.Round(Raylib.GetFPS() * 0.16666666667f);

        if (invincibilityDuration > 0)
        {
            float t = Math.Clamp(1f - (invincibilityDuration / invincibilityTime), 0f, 1f);
            color.A = (byte)(t * 255);

            ClearLastPositions();
        }
        else
        {
            color.A = 255;
            if (dashDuration > 0)
            {
                AddTrailEffects(maxTrailSize, new Color(22, 15, 55, 255), 100, 100, 0, -1);
            }
            else
            {
                AddTrailEffects(maxTrailSize, new Color(0, 88, 255, 0), 100, 100, 0, 130);
            }
        }

        DisplayHealthBar(50, 145, 10);
        Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, color);
        ShowHitboxes();
    }

    public override void BeginDraw()
    {

    }

    public override void TakenDamage()
    {
        takenDamage();
    }

    public override void Despawn()
    {
        Console.WriteLine("spelaren har despawnat");
        GibbManager.currentlyGibbing = false;
    }

    public Player(ControlLayout controlLayout)
    {
        objectIdentifier = "player";
        {
            x = 400;
            y = 450;
            width = 85;
            height = 80;
            int båt = 3;
        }

        {

           AddToGameList(this);

        }
        maxHP = 20;
        {
            {
                {
                    {
                        {
                            {
                                {
                                    {
                                        {
                                            ; ; ; ; ; ;
                                            {; {; {; {; } } } }
                                            //hej jag heter anton
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        hp = maxHP;
        { }
        currentLayout = controlLayout;

        InitializeDelegates();
    }
}