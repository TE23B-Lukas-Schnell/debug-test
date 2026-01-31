abstract class FightableObject : MoveableObject
{
    protected float maxHP;
    protected float hp;
    public bool healthy = true;
    protected float invincibilityDuration = 0;
    public static Color healthBarColor = new Color(100, 100, 100);

    protected float spriteWidth;
    protected float spriteHeight;
    protected Texture2D sprite;

    public List<Items> Inventory = new List<Items>();

    protected void DisplayHealthBar(float x, float y, float sizeMultiplier)
    {
        Raylib.DrawRectangle((int)x, (int)y, (int)(maxHP * sizeMultiplier) + 10, 60, Color.Gray);
        Raylib.DrawRectangle((int)x + 5, (int)y + 5, (int)(hp * sizeMultiplier), 50, Color.Green);
    }

    protected void DisplayHealthBar(float x, float y, float sizeMultiplier, string text, float textSize)
    {
        Raylib.DrawRectangle((int)x, (int)y, (int)(maxHP * sizeMultiplier) + 10, 60, Color.Gray);
        Raylib.DrawRectangle((int)x + 5, (int)y + 5, (int)(hp * sizeMultiplier), 50, Color.Green);
        Raylib.DrawText(text, (int)x + 10, (int)(y - textSize), (int)textSize, Color.Black);
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
    public void ContactDamage(float damage, string objectIdentifier)
    {
        FightableObject? target;
        if (CheckCollisions() is FightableObject)
        {
            target = CheckCollisions() as FightableObject;
            if (target.objectIdentifier == objectIdentifier)
            {
                target.TakeDamage(damage, target);
            }
        }
    }

    //detta är för contact damage med en annorlunda hitbox än collision hitboxen 
    public void ContactDamage(float damage, string objectIdentifier, Hitbox newHitbox)
    {
        FightableObject? target;

        MoveableObject? köttig = CheckCollisions(newHitbox);
        if (köttig is FightableObject)
        {
            target = köttig as FightableObject;
            if (target.objectIdentifier == objectIdentifier)
            {
                target.TakeDamage(damage, target);
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

    protected FightableObject()
    {
        //detta sätter hp till null vilket😡 det borde inte funka så tycker jag
        // måste ändå skriva detta i varje konstruktor
        hp = maxHP;
    }
}