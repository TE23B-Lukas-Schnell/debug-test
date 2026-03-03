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
  
    async Task ShootArrows(CancellationToken ct)
    {
        xSpeed = 0;
        ySpeed = 0;
        Color temp = color;
        color = new Color(0, 234, 14);
        await Wait(600, ct);
        int side = 0;
        int xDirection = 1;
        float bulletSpeed = 1300;

        if (GibbManager.currentRun.playerReference.x < moveLine)
        {
            side = Raylib.GetScreenWidth();
            xDirection = -1;
        }

        new BossBullet(side, bulletHeight, bulletWidth, bulletHeight, bulletSpeed * xDirection, 0, 0, bulletDamage);

        new BossBullet(side, Raylib.GetScreenHeight() / 2 - bulletHeight / 2, bulletWidth, bulletHeight, bulletSpeed * xDirection, 0, 0, bulletDamage);

        new BossBullet(side, Raylib.GetScreenHeight() - bulletHeight * 2, bulletWidth, bulletHeight, bulletSpeed * xDirection, 0, 0, bulletDamage);

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