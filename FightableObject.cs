abstract class FightableObject : MoveableObject
{
    protected float maxHP;
    protected float hp;
    public bool healthy = true;
    protected float invincibilityDuration = 0;
    public static Color healthBarColor = new Color(100, 100, 100);

    // protected float spriteWidth;
    // protected float spriteHeight;
    // protected Texture2D sprite;

    public List<Items> Inventory = new List<Items>();

    protected void DisplayHealthBar(float x, float y, float sizeMultiplier)
    {
        Raylib.DrawRectangle(R(x),R(y), R((maxHP * sizeMultiplier) + 10), 60, Color.Gray);
        Raylib.DrawRectangle(R(x + 5), R(y + 5), R(hp * sizeMultiplier), 50, Color.Green);
    }

    protected void DisplayHealthBar(float x, float y, float sizeMultiplier, string text, float textSize)
    {
        Raylib.DrawRectangle(R(x), R(y), R((maxHP * sizeMultiplier) + 10), 60, Color.Gray);
        Raylib.DrawRectangle(R(x + 5), R(y + 5), R(hp * sizeMultiplier), 50, Color.Green);
        Raylib.DrawText(text, R(x + 10), R(y - textSize), R(textSize), Color.Black);
    }

    bool ChangeHp(FightableObject target, float changeAmount, float changeMultiplier, float limit, bool isDamage)
    {
        bool limitReached;

        if (isDamage)
        {
            target.hp -= changeAmount * changeMultiplier;
            if (limit >= hp)
            {
                hp = limit;
                limitReached = true;
            }
            else limitReached = false;
        }
        else
        {
            target.hp += changeAmount * changeMultiplier;
            if (limit <= hp)
            {
                hp = limit;
                limitReached = true;
            }
            else limitReached = false;
        }
        return limitReached;
    }

    //objektet hp minskar, tas bort om det är < 0
    public void TakeDamage(float damage, FightableObject target)
    {
        if (target.invincibilityDuration <= 0)
        {

            if (ChangeHp(target, damage, damageMultiplier, 0, true))
            {
                target.remove = true;
                Despawn();
            }
            target.TakenDamage();
        }
    }

    public void HealDamage(float healAmount, FightableObject target)
    {
        if (ChangeHp(target, healAmount, healMultiplier, maxHP, false))
        {
            target.healthy = true;
        }
    }

    //😂😂😂 𝼀𰻝Ꮸ𐃵
    protected void CheckDamagingHitbox(float damage, string objectIdentifier, Hitbox newHitbox)
    {
        FightableObject? target;

        MoveableObject? träffatObjekt = CheckCollisions(newHitbox);
        if (träffatObjekt is FightableObject)
        {
            target = träffatObjekt as FightableObject;
            if (target != null)
            {
                if (target.objectIdentifier == objectIdentifier)
                {
                    //ok båtig
                    target.TakeDamage(damage, target);
                }
            }
        }
    }


    public void ApplyBuffsFromItem()
    {
        if (Inventory.Count > 0)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (!Inventory[i].buffActivated)
                {
                    Inventory[i].buffActivated = true;
                    Inventory[i].ApplyStatChangesFunction(this);
                }
            }
        }
        else Console.WriteLine("tomt inventory");
    }

    public abstract void TakenDamage();

    public override void AddToGameList()
    {
         AddToGameList(this);
    }
       
    protected FightableObject()
    {
        //detta sätter hp till null vilket😡 det borde inte funka så tycker jag
        // måste ändå skriva detta i varje konstruktor
        hp = maxHP;
    }
}