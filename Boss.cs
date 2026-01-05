abstract class Boss : FightableObject
{
    public int screenSizeX;
    public int screenSizeY;
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
            
            await bossAttacks[r](5, token);
            await Task.Delay(250, token); // small cooldown between attacks
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
}