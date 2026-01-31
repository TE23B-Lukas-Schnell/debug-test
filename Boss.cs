abstract class Boss : FightableObject
{
    public int screenSizeX;
    public int screenSizeY;
    public string name;
    protected bool notAttacking;

    protected float contactDamage;
    protected Hitbox contactDamageHitbox;
    protected float contactDamageHitboxSizeRatio;

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

    protected void CallThisInTheUpdateFunction()
    {
         ChooseAttack();

        if (notAttacking)
        {
            MoveCycle();
        }

        UpdateHitboxPosition(x, y, width, height);
        UpdateContactDamageHitbox();

        ContactDamage();

        MoveObject(gravity);
    }

    protected void ContactDamage()
    {
        UpdateContactDamageHitbox();
        CheckDamagingHitbox(contactDamage, "player", contactDamageHitbox);
    }

    protected void UpdateContactDamageHitbox()
    {
        float w = width * contactDamageHitboxSizeRatio;
        float h = height * contactDamageHitboxSizeRatio;
        float xpos = x + (width - w) / 2;
        float ypos = y + (height - h) / 2;

        contactDamageHitbox.hitbox = new Rectangle(R(xpos), R(ypos), R(w), R(h));
    }

    protected delegate Task BossAttack(CancellationToken ct);
    protected List<BossAttack> bossAttacks = new List<BossAttack>();

    protected CancellationTokenSource cancellationToken;

    //this function makes the wait amount multiplied by the wait multiplier
    protected async Task Wait(float time, CancellationToken ct) => await Task.Delay(R(time * waitMultiplier), ct);

    protected async Task Wait(float time, CancellationToken ct, bool UseMultiplier)
    {
        if (UseMultiplier) await Task.Delay(R(time * waitMultiplier), ct);
        else await Task.Delay(R(time), ct);
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

    public override void Update()
    {
         CallThisInTheUpdateFunction();
    }

    protected Boss()
    {
        contactDamageHitbox = new(new Rectangle(x, y, width, height), this);
    }
}