abstract class Boss : FightableObject
{
    public int screenSizeX;
    public int screenSizeY;
    public string name;
    protected bool notAttacking;

    //stats that could potentially change with items 
    public Color color = new Color(255, 255, 255, 255);
    public float moveSpeed;
    public float jumpHeight;
    public float gravity;
    public float bulletWidth;
    public float bulletHeight;
    public float bulletDamage;
    public float waitMultiplier = 1;
    public float attackDelay = 2000;

    public bool Active
    {
        get => isActiveBoss;

        set => isActiveBoss = value;
    }

    //this is used for a boss that is instantiated but not the current boss you fighting
    //not implemented yet
    protected bool isActiveBoss = false;

    protected float contactDamage;

    protected delegate Task BossAttack(CancellationToken ct);
    protected List<BossAttack> bossAttacks = new List<BossAttack>();

    protected CancellationTokenSource cancellationToken;

    //this function makes the wait amount multiplied by the wait multiplier
    protected async Task Wait(float time, CancellationToken ct) => await Task.Delay((int)MathF.Round(time * waitMultiplier), ct);

    protected async Task Wait(float time, CancellationToken ct, bool UseMultiplier)
    {
        if (UseMultiplier) await Task.Delay((int)MathF.Round(time * waitMultiplier), ct);
        else await Task.Delay((int)MathF.Round(time), ct);
    }

    protected async Task AttackLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            notAttacking = true;
            await Wait(attackDelay, token, true);

            notAttacking = false;
            await bossAttacks[Random.Shared.Next(bossAttacks.Count)](token);
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

    public void InitializePlayableBoss()
    {
        Active = true;
    }

    abstract public void MoveCycle();
}