abstract class Boss : FightableObject
{
    public int screenSizeX;
    public int screenSizeY;
    public string name;
    protected bool notAttacking;

    protected SpriteDrawer spriteDrawer = new SpriteDrawer();
    protected string spriteFilePath;

    protected float contactDamage;
    protected Hitbox contactDamageHitbox;
    protected float contactDamageHitboxSizeRatio;

    protected delegate Task BossAttack(CancellationToken ct);
    protected List<BossAttack> bossAttacks = new List<BossAttack>();
    protected CancellationTokenSource cancellationToken;

    //stats that could potentially change with items 
    public Color color = new Color(255, 255, 255, 255);
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    public float bulletWidth;
    public float bulletHeight;
    public float bulletDamage;
    public float waitMultiplier = 1;
    public float attackDelay = 2000;

    public string PrintBossStats()
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
wait multiplier          {waitMultiplier}
damage multiplier        {damageMultiplier}
bullet width:            {bulletWidth}
bullet height            {bulletHeight}
bullet damage            {bulletDamage}";


        output += "\ninventory:";
        foreach (Item items in Inventory)
        {
            output += "\n   " + items.name;
        }
        return output;
    }
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
        GibbManager.currentRun.AddToGameList(this);
        GibbManager.currentRun.AddToHitboxList(hurtbox);
        GibbManager.currentRun.AddToHitboxList(contactDamageHitbox);
    }

    public override void Update()
    {
        CallThisInTheUpdateFunction();
    }

    void DrawDamageNumbers(float damage)
    {
        Raylib.DrawText(damage.ToString(), R(x), R(y), 20, Color.Red);
    }

    public override void Draw()
    {
        spriteDrawer.DrawTexture(color, x, y); ;
        DisplayHealthBar(50, 50, 1, name, 30);
    }

    public override void BeginDraw()
    {
        spriteDrawer.LoadSprite(Raylib.LoadTexture(spriteFilePath), width, height);
    }

    public override void Despawn()
    {
        contactDamageHitbox.DeleteHitbox();
        hurtbox.DeleteHitbox();
        GibbManager.currentlyGibbing = false;
        cancellationToken?.Cancel();
    }

    public override void TakenDamage(float damage)
    {
    }

    abstract public void MoveCycle();

    public override void AddToGameList()
    {

    }

    protected Boss()
    {
        contactDamageHitbox = new(new Rectangle(x, y, width, height), this);
    }
}