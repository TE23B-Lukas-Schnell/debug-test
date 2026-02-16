abstract class FightableObject : MoveableObject
{
    public float maxHP;
    public Color healthBarColor = new Color(100, 100, 100);
    public float damageMultiplier = 1;
    public float healMultiplier = 1;
    public bool healthy = true;

    protected float invincibilityDuration = 0;
    protected float hp;

    public List<Item> Inventory = new List<Item>();

    protected void Displaybar(float x, float y, float w, float h, float value, float horizontalPadding, float verticalPadding, Color fillColor)
    {
        Raylib.DrawRectangle(R(x), R(y), R(w), R(h), Color.Gray); // drrawing the outline rec first
        Raylib.DrawRectangle(R(x + horizontalPadding), R(y + verticalPadding), R(value), R(h - verticalPadding * 2), fillColor);
    }

    protected void DisplayHealthBar(float x, float y, float sizeMultiplier)
    {
        // Raylib.DrawRectangle(R(x), R(y), R((maxHP * sizeMultiplier) + 10), 60, Color.Gray);
        // Raylib.DrawRectangle(R(x + 5), R(y + 5), R(hp * sizeMultiplier), 50, Color.Green);
        Displaybar(x, y, (maxHP * sizeMultiplier) + 10, 60, hp * sizeMultiplier, 5, 5, Color.Green);
    }

    protected void DisplayHealthBar(float x, float y, float sizeMultiplier, string text, float textSize)
    {
        Displaybar(x, y, (maxHP * sizeMultiplier) + 10, 60, hp * sizeMultiplier, 5, 5, Color.Green);
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

    //objektets hp minskar, tas bort om det är < 0
    public void TakeDamage(float damage, FightableObject target)
    {
        if (target.invincibilityDuration <= 0)
        {

            if (ChangeHp(target, damage, damageMultiplier, 0, true))
            {
                target.remove = true;
                Despawn();
            }
            target.TakenDamage(damage);
        }
    }

    public void HealDamage(float healAmount, FightableObject target)
    {
        if (ChangeHp(target, healAmount, healMultiplier, maxHP, false))
        {
            target.healthy = true;
        }
    }

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
    //😂😂😂 𝼀𰻝Ꮸ𐃵
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
        // else Console.WriteLine("tomt inventory");
    }

    public abstract void TakenDamage(float damage);

    public override void AddToGameList()
    {
       
    }

    protected FightableObject()
    {
        //detta sätter hp till null vilket😡 det borde inte funka så tycker jag
        // måste ändå skriva detta i varje konstruktor
        hp = maxHP;
    }
}