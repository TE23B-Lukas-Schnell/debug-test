class CalleBoss : Boss
{

    public float moveLine = 1920 * 0.79f - 220f;
    public float jumpLine = 850 * 0.45f;
    public bool isPhase2 = false;


    void Moving(float value, float minValue, float maxValue, ref float changeValue)
    {
        if (changeValue == 0) changeValue = moveSpeed;

        if (value >= maxValue)
        {
            changeValue = -Math.Abs(moveSpeed);
        }
        else if (value <= minValue)
        {
            changeValue = Math.Abs(moveSpeed);
        }
    }

    async Task HighAndLow(CancellationToken ct)
    {
        float speedMultiplier = 103;
        float gravity = 2300;

        await Wait(1000, ct);
        new FireballBullet(x + 70, y + 70, -10 * speedMultiplier, 9 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
        new FireballBullet(x + 70, y + 70, -8 * speedMultiplier, 8 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);

        await Wait(1000, ct);
        new FireballBullet(x + 70, y + 70, -7 * speedMultiplier, 6 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
        new FireballBullet(x + 70, y + 70, -4 * speedMultiplier, 3 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
    }

    async Task DropInCorner(CancellationToken ct)
    {
        float speedMultiplier = 100;
        float gravity = 2300;

        await Wait(2500, ct);
        new FireballBullet(30, 0, 0 * speedMultiplier, 2 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
    }

    async Task LowToTheGround(CancellationToken ct)
    {
        float speedMultiplier = 100;
        float gravity = 2300;
        float y = (screenSizeY + 330) * 0.62f;
        await Wait(1500, ct);

        new FireballBullet(screenSizeX, y, -8 * speedMultiplier, 0 * speedMultiplier, gravity, bulletWidth+20, bulletHeight, bulletDamage);
    }

    async Task Meteor(CancellationToken ct)
    {
        float bulletWidth = this.bulletWidth + 20;
        float speedMultiplier = 100;
        float gravity = 2300;
        float y = 100;
        float y2 = 0;
        float x() => p.x + (p.width / 2) - (bulletWidth / 2);

        if (isPhase2)
        {
            for (int i = 0; i < 5; i++)
            {
                await Wait(1250, ct);
                new FireballBullet(x(), y2, 3.5f * speedMultiplier, 3 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
                new FireballBullet(x(), y2, 2.5f * speedMultiplier, 3 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
                new FireballBullet(x(), y2, 1.33f * speedMultiplier, 3 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);

                new FireballBullet(x(), y2, -1.33f * speedMultiplier, 3 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
                new FireballBullet(x(), y2, -2.5f * speedMultiplier, 3 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
                new FireballBullet(x(), y2, -3.7f * speedMultiplier, 3 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);

                await Wait(1250, ct);
                new FireballBullet(x(), y, 3.7f * speedMultiplier, 10 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
                new FireballBullet(x(), y, 1.75f * speedMultiplier, 8 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);

                new FireballBullet(x(), y, 0, 6 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
                new FireballBullet(x(), y, 0, 8 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
                new FireballBullet(x(), y, 0, 10 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);

                new FireballBullet(x(), y, -1.75f * speedMultiplier, 8 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
                new FireballBullet(x(), y, -3.5f * speedMultiplier, 10 * speedMultiplier, gravity, bulletWidth, bulletHeight, bulletDamage);
            }
        }

    }

    async Task Seesaw(CancellationToken ct)
    {
        float speedMultiplier = 100;
        float y = (screenSizeY + 330) * 0.62f;
        float y2 = (screenSizeY + 330) * 0.35f;

        for (int i = 0; i < 5; i++)
        {
            new FireballBullet(screenSizeX, y, -9 * speedMultiplier, 0, 0, bulletWidth, bulletHeight, bulletDamage);
            await Wait(400, ct);
            new FireballBullet(screenSizeX, y2, -9 * speedMultiplier, 0, 0, bulletWidth, bulletHeight, bulletDamage);
            await Wait(400, ct);
        }
    }

    // void båt()
    // {
    //     if (cancellationToken == null)
    //     {
    //         cancellationToken = new CancellationTokenSource();
    //         _ = AttackLoop(cancellationToken.Token);

    //     }
    // }

    protected override async Task AttackLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (!isPhase2)
            {
                await HighAndLow(token);
                _ = DropInCorner(cancellationToken.Token);
                _ = LowToTheGround(cancellationToken.Token);
            }
            else
            {
                await Seesaw(token);
                await Meteor(token);
            }
        }
    }

    public override void Update()
    {
        MoveCycle();

        ChooseAttack();

        UpdateHitboxPosition(x, y, width, height);
        UpdateContactDamageHitbox();

        ContactDamage();

        MoveObject(gravity);
    }

    public override void MoveCycle()
    {
        Moving(x, moveLine, screenSizeX - width, ref xSpeed);

        if (!isPhase2)
        {
            if (y > jumpLine)
            {
                ySpeed = jumpForce;
            }
        }
    }

    public override void Despawn()
    {

        if (!isPhase2)
        {
            remove = false;
            isPhase2 = true;
            maxHP *= 2;
            HealDamage(maxHP, this);
        }
        else
        {
            base.Despawn();
        }

    }

    public CalleBoss()
    {
        screenSizeX = 1920;
        screenSizeY = 850;
        width = 300;
        height = 300;
        x = screenSizeX * 0.7f;
        y = screenSizeY * 0.1f;
        maxHP = 200;
        hp = maxHP;
        objectIdentifier = "enemy";
        contactDamage = 4;
        contactDamageHitboxSizeRatio = 0.5f;
        moveSpeed = 400;
        gravity = 2300;
        jumpForce = 1300;

        bulletWidth = 150;
        bulletHeight = 120;
        bulletDamage = 3f;
        waitMultiplier = 1;
        name = "Kung Carl";
        attackDelay = 1500;
        notAttacking = true;

        spriteFilePath = @"./Sprites/carlengman-scaled-600x600.jpg";
    }
}