abstract class Player : FightableObject
{
    //statiska variabler

    //sparar alla olika actions spelaren kan göra och om knappen för den actionen är ned tryckt
    public static Dictionary<string, bool> keyPressed = new Dictionary<string, bool>()
    {
        {"up", false },
        {"down", false },
        {"left", false },
        {"right", false },
        {"jump", false },
        {"dash", false },
        {"shoot", false },
    };

    public string name = "";
    public string description = "";
    public ControlLayout currentLayout;
    public int score = 0;
    protected SpriteDrawer spriteDrawer = new SpriteDrawer();
    protected string spriteFilePath;
    //start items behövs nog inte
    // protected Item startItem;

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
    public float setDashCooldown = 0.47f;
    public float fastFallSpeed = 1367f;
    public float dashSpeed = 1800f;
    public float invincibilityTime = 1f;
    public Color color = new Color(0, 0f, 235f, 254f);

    // hur gör man det här på ett bra sätt?

    public Type Projectile = typeof(PlayerBullet);

    //bullet stats
    public float setShootCooldown = 0.5f;
    public float bulletWidth = 40f;
    public float bulletHeight = 20f;
    public float bulletDamage = 10f; //vanligtvis 5, borde vara 50 när man debuggar
    public float bulletxSpeed = 1600f;
    public float bulletySpeed = 0f;
    public float bulletGravity = 0f;

    //variabler
    float dashDuration = 0;
    float dashCooldown = 0;
    float shootCooldown = 0;

    public string PrintPlayerStats()
    {
        string output = @$"Stats:
pos                      {x} {y}
velocity                 {xSpeed} {ySpeed}
size                     {width} {height}
max hp                   {maxHP}
current hp               {hp}
gravity                  {gravity}
move speed               {moveSpeed}
jump force               {jumpForce}
dash duration            {setDashDuration}
dash cooldown            {setDashCooldown}
fastfall speed           {fastFallSpeed}
shoot cooldown           {setShootCooldown}
damage multiplier        {damageMultiplier}
bullet width:            {bulletWidth}
bullet height            {bulletHeight}
bullet damage            {bulletDamage}
bullet speed             {bulletxSpeed} {bulletySpeed}
bullet gravity           {bulletGravity}";

        output += "\ninventory:";
        foreach (Item items in Inventory)
        {
            output += "\n   " + items.name;
        }
        return output;
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
            new PlayerBullet(x, y, bulletWidth, bulletHeight, bulletxSpeed, bulletySpeed, bulletGravity, bulletDamage, false);
        };
        upShoot += () =>
        {
            shootCooldown = setShootCooldown;
            new PlayerBullet(x, y, bulletWidth, bulletHeight, bulletxSpeed, bulletySpeed, bulletGravity, bulletDamage * 1.23f   , true);
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
            else if (keyPressed["right"])
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

    public void InitializePlayer()
    {
        // GibbManager.currentRun.AddToGameList(this);
        GibbManager.currentRun.AddToHitboxList(hitbox);
    }

    public override void Update()
    {
        // System.Console.WriteLine(köttig );

        dashCooldown = MathF.Max(dashCooldown - Raylib.GetFrameTime(), 0);
        dashDuration = MathF.Max(dashDuration - Raylib.GetFrameTime(), 0);
        shootCooldown = MathF.Max(shootCooldown - Raylib.GetFrameTime(), 0);
        invincibilityDuration = MathF.Max(invincibilityDuration - Raylib.GetFrameTime(), 0);

        //debug cheat mode för båtig purpose
        if (Raylib.IsKeyPressed(KeyboardKey.LeftControl)) invincibilityDuration = 5;

        Checkinputs();

        MoveCheck();
        FastFallCheck();
        JumpCheck();
        DashCheck();
        ShootCheck();

        UpdateHitboxPosition(x, y, width, height);

        MoveObject(gravity);
    }

    public override void Draw()
    {
        maxTrailSize = R(Raylib.GetFPS() * 0.16666666667f);

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
        spriteDrawer.DrawTexture(color, x, y);
    }

    public override void BeginDraw()
    {
        spriteDrawer.LoadSprite(Raylib.LoadTexture(spriteFilePath), width, height);
    }

    public override void TakenDamage()
    {
        takenDamage();
    }

    public override void Despawn()
    {
        hitbox.DeleteHitbox();
        // Console.WriteLine("spelaren har despawnat");
        GibbManager.currentlyGibbing = false;

    }

    public Player(ControlLayout controlLayout)
    {
        objectIdentifier = "player";
        x = 400;
        y = 450;
        currentLayout = controlLayout;
        InitializeDelegates();
    }
}