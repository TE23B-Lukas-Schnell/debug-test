class MatteBoss : Boss
{
    public float jumpLine = 500f;
    public float moveLine = 800f;

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

    int PlayerDirection()
    {
        if (GibbManager.currentRun.playerReference.x + GibbManager.currentRun.playerReference.width / 2 <= x) return -1;
        else return 1;

    }

    async Task DashAttack(CancellationToken ct)
    {
        xSpeed = 0;
        ySpeed = 0;
        Color temp = color;
        color = new Color(12, 170, 200);

        while (x <= screenSizeX - width)
        {
            ySpeed = 0;
            xSpeed = moveSpeed;
        }
        await Wait(1200, ct);
        contactDamage *= 2;

        KemiBullet.EnemyShoot(bulletWidth * 2.5f, 0, bulletWidth * 2.5f, bulletHeight * 2, 0, 0, 1800f, bulletDamage, true);

        KemiBullet.EnemyShoot(screenSizeX / 2 - bulletWidth * 2.5f, 0, bulletWidth * 2.5f, bulletHeight * 2, 0, 0, 1800f, bulletDamage, true);

        KemiBullet.EnemyShoot(screenSizeX - bulletWidth * 2.5f, 0, bulletWidth * 2.5f, bulletHeight * 2, 0, 0, 1800f, bulletDamage, true);

        await Wait(230, ct);

        xSpeed = -moveSpeed * 2;

        while (x >= 0)
        {

        }

        color = temp;
        await Wait(600, ct);
        contactDamage /= 2;
        ySpeed += jumpForce;
    }

    async Task BombPlan(CancellationToken ct)
    {
        float tempJumpLine = jumpLine;

        jumpLine = 569;

        Color temp = color;
        color = new Color(128, 0, 128);

        {
            float tempXspeed = xSpeed;
            float tempGravity = gravity;

            gravity = 0;
            xSpeed = 0;
            ySpeed = 0;

            await Wait(1000, ct);

            xSpeed = tempXspeed;
            gravity = tempGravity;
        }

        int attacks = 6;
        bool jumpLeft = true;

        for (int i = 0; i < attacks; i++)
        {

            await Wait(100, ct);

            KemiBullet.EnemyShoot(x, y, bulletWidth * 2.5f, bulletHeight * 2, 1 + xSpeed / 2, 112, 2300f, bulletDamage, false);

            await Wait(267, ct);

            if (y > jumpLine) ySpeed = jumpForce;
            else ySpeed = jumpForce / 2;

            if (jumpLeft) xSpeed = -moveSpeed * 1.2f;
            else xSpeed = moveSpeed * 1.2f;


            jumpLeft = !jumpLeft;

            await Wait(450, ct);
        }

        {
            float tempGravity = gravity;

            gravity = 0;
            xSpeed = 0;
            ySpeed = 0;

            await Wait(550, ct);

            KemiBullet.ChristianShoot(x, y, bulletWidth * 10, bulletHeight * 6, 0, 0, 670f, bulletDamage * 1.5f, true);

            await Wait(800, ct);
            gravity = tempGravity;
        }
        color = temp;
        jumpLine = tempJumpLine;
    }

    async Task ShootArrows(CancellationToken ct)
    {
        xSpeed = 0;
        ySpeed = 0;
        Color temp = color;
        color = new Color(0, 234, 14);
        await Wait(600, ct);
        int side = 0;
        int xDirection = 1;
        float bulletSpeed = 1500;

        if (GibbManager.currentRun.playerReference.x < moveLine)
        {
            side = Raylib.GetScreenWidth();
            xDirection = -1;
        }

        new BossBullet(side, bulletHeight, bulletWidth, bulletHeight, bulletSpeed * xDirection, 0, 0, bulletDamage, true);

        new BossBullet(side, Raylib.GetScreenHeight() / 2 - bulletHeight / 2, bulletWidth, bulletHeight, bulletSpeed * xDirection, 0, 0, bulletDamage, true);

        new BossBullet(side, Raylib.GetScreenHeight() - bulletHeight * 2, bulletWidth, bulletHeight, bulletSpeed * xDirection, 0, 0, bulletDamage, true);

        await Wait(400, ct);
        color = temp;
    }

    void InitializeDelegates()
    {
        bossAttacks.Add(ShootArrows);
    }

    public override void MoveCycle()
    {
        Moving(x, moveLine - 200 - width / 2, moveLine + 200 - width / 2, ref xSpeed);
    }

    public MatteBoss()
    {
        screenSizeX = 1600;
        screenSizeY = 800;
        width = 186 * 1.3f;
        height = 450;
        x = screenSizeX;
        y = 0;
        maxHP = 500;
        hp = maxHP;
        objectIdentifier = "enemy";
        contactDamage = 4f;
        contactDamageHitboxSizeRatio = 0.8f;
        moveSpeed = 469;
        gravity = 0;
        jumpForce = 1500;

        InitializeDelegates();

        bulletWidth = 60;
        bulletHeight = 25;
        bulletDamage = 4.4f;
        waitMultiplier = 1;
        name = "matte läraren";
        attackDelay = 1700;

        spriteFilePath = @"./Sprites/båt.png";
    }
}