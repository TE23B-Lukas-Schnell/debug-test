abstract class Player : FightableObject
{
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
        {"upshoot",false}
    };

    public string name = "";
    public ControlLayout currentLayout;
    public int score = 0;
    public int facingDirection = 1;
    protected SpriteDrawer spriteDrawer = new SpriteDrawer();
    protected string spriteFilePath;
    protected PointingArrow arrow;
    float arrowSize = 2;
    float arrowRotation = 0;
    float upPointRotaion = -90;

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
    public Action up;

    //player stats 
    public float gravity = 2300f;
    public float moveSpeed = 900f;
    public float jumpForce = 1300f;
    public float dashSpeed = 1800f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.47f;
    public float fastFallSpeed = 1367f;
    public float invincibilityTime = 1f;
    public Color color = new Color(0, 0f, 235f, 254f);

    // hur gör man det här på ett bra sätt?
    public Type Projectile = typeof(PlayerBullet);

    //bullet stats
    public float shootCooldown = 0.5f;
    public float bulletWidth = 40f;
    public float bulletHeight = 20f;
    public float bulletDamage = 5f; //vanligtvis 5, borde vara 50 när man debuggar
    public float bulletDamageMultiplier = 1;
    public float bulletxSpeed = 1600f;
    public float bulletySpeed = 0f;
    public float bulletGravity = 0f;

    //variabler
    float _dashDuration = 0;
    float _dashCooldown = 0;
    float _shootCooldown = 0;

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
dash speed               {dashSpeed}
dash duration            {dashDuration}
dash cooldown            {dashCooldown}
fastfall speed           {fastFallSpeed}
shoot cooldown           {shootCooldown}
damage multiplier        {damageMultiplier}
bullet width:            {bulletWidth}
bullet height            {bulletHeight}
bullet damage multiplier {bulletDamageMultiplier}
bullet damage            {bulletDamage}
bullet speed             {bulletxSpeed} {bulletySpeed}
bullet gravity           {bulletGravity}";

        output += "\ninventory:";
        output += GibbManager.ListToString(Inventory);
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
            _dashCooldown = dashCooldown;
        };
        shoot += () =>
        {
            float damage = bulletDamage * bulletDamageMultiplier;
            _shootCooldown = shootCooldown;
            new PlayerBullet(x + width / 2, y + height / 2, bulletWidth, bulletHeight, bulletxSpeed * facingDirection, bulletySpeed, bulletGravity, damage);
        };
        upShoot += () =>
        {
            float damage = bulletDamage * 1.05f * bulletDamageMultiplier;
            _shootCooldown = shootCooldown;
            new PlayerBullet(x + width / 2, y + height / 2, bulletHeight, bulletWidth, bulletySpeed * facingDirection, bulletxSpeed, bulletGravity, damage);
        };
        up += () =>
        {

        };
        takenDamage += () => invincibilityDuration = invincibilityTime;
    }

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

    void FastFallCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["down"] && !Grounded())
        {
            fastFall();
        }
    }

    void JumpCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["jump"] && Grounded())
        {
            jump();
        }
    }

    void DashCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["dash"] && _dashCooldown == 0)
        {
            if (keyPressed["left"])
            {
                leftDash();
            }
            else if (keyPressed["right"])
            {
                rightDash();
            }
            _dashDuration = dashDuration;
        }
        if (_dashDuration > 0)
        {
            duringDash();
        }
    }

    void UpCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["up"])
        {
            up();
        }
    }

    void ShootCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["shoot"] && _shootCooldown <= 0 && !keyPressed["up"])
        {
            shoot();
        }
        else if (keyPressed["shoot"] && _shootCooldown <= 0 && keyPressed["up"])
        {
            upShoot();
        }
    }

    void UpShootMacroCheck(/*HEJ JAG HETER  ANTON*/)
    {
        if (keyPressed["upshoot"])
        {
            keyPressed["up"] = true;
            keyPressed["shoot"] = true;
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
        GibbManager.currentRun.AddToGameList(this);
        GibbManager.currentRun.AddToHitboxList(hitbox);
    }

    public override void Update()
    {
        // System.Console.WriteLine(köttig );

        _dashCooldown = MathF.Max(_dashCooldown - Raylib.GetFrameTime(), 0);
        _dashDuration = MathF.Max(_dashDuration - Raylib.GetFrameTime(), 0);
        _shootCooldown = MathF.Max(_shootCooldown - Raylib.GetFrameTime(), 0);
        if (GibbManager.currentRun.currentBoss.x + GibbManager.currentRun.currentBoss.width / 2 <= x)
        {
            facingDirection = -1;
        }
        else
        {
            facingDirection = 1;
        }
        invincibilityDuration = MathF.Max(invincibilityDuration - Raylib.GetFrameTime(), 0);

        //debug cheat mode för båtig purpose
        if (Raylib.IsKeyPressed(KeyboardKey.LeftControl)) invincibilityDuration = 5;

        Checkinputs();

        MoveCheck();
        FastFallCheck();
        JumpCheck();
        DashCheck();

        UpShootMacroCheck();

        UpCheck();
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
            if (_dashDuration > 0)
            {
                AddTrailEffects(maxTrailSize, new Color(22, 15, 55, 255), 100, 100, 0, -1);
            }
            else
            {
                AddTrailEffects(maxTrailSize, new Color(0, 88, 255, 0), 100, 100, 0, 130);
            }
        }

        DisplayHealthBar(50, 145, 10, name, 30);
        spriteDrawer.DrawTexture(color, x, y);


        if (keyPressed["up"])
        {
            arrowRotation = upPointRotaion;
        }
        else
        {
            if (facingDirection > 0) arrowRotation = 0;
            else arrowRotation = 180;
        }

        // float lerpedArrowRotation = 

        arrow.DrawArrow(x, y, -width, 0, arrowRotation);

        if (keyPressed["shoot"])
        {
            arrow.LightUpArrow(x, y, -width, 0, arrowRotation);
        }
    }

    public override void BeginDraw()
    {
        spriteDrawer.LoadSprite(Raylib.LoadTexture(spriteFilePath), width * facingDirection, height);
        arrow = new(Color.Red, 32 * arrowSize, 17 * arrowSize, true);
    }

    public override void TakenDamage(float damage)
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