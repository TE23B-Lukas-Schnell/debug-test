abstract class Boss : FightableObject
{
    public int screenSizeX;
    public int screenSizeY;

    protected bool notAttacking;

    //this is used for a boss that is instantiated but not the current boss you fighting
    //not implemented yet
    protected bool isActiveBoss = false;

    public bool Active
    {
        get => isActiveBoss;

        set => isActiveBoss = value;
    }

    protected float contactDamage;

    protected delegate Task BossAttack(float damage, CancellationToken ct);
    protected List<BossAttack> bossAttacks = new List<BossAttack>();

    protected CancellationTokenSource cancellationToken;

    protected async Task AttackLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (bossAttacks.Count == 0) { await Task.Delay(100, token); continue; }

            int r = Random.Shared.Next(bossAttacks.Count);

            /*try
            {
                await bossAttacks[r](5, token);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception) { }
            */

            notAttacking = false;
            await bossAttacks[r](5, token);

            notAttacking = true;
            // make the boss move a little before the next attack
            await Task.Delay(2150, token);
        }
    }

    protected void ChooseAttack()
    {
        if (cancellationToken == null)
        {
            cancellationToken = new CancellationTokenSource();
            _ = AttackLoop(cancellationToken.Token);
        }
    }

    public Texture2D sprite;

    public Texture2D ChangeSpriteSize(Texture2D texture, int width, int height)
    {
        Image image = Raylib.LoadImageFromTexture(texture);
        Raylib.ImageResize(ref image, width, height);
        return Raylib.LoadTextureFromImage(image);
    }

    public void DrawTexture(Texture2D texture, Color color)
    {
        Raylib.DrawTexture(texture, (int)x, (int)y, color);
    }

    abstract public void MoveCycle();
}