abstract class FightableObject : MoveableObject
{
    protected float maxHP;
    protected float hp;
    public bool healthy = true;
    protected float invincibilityDuration = 0;
    public static Color healthBarColor = new Color(100, 100, 100);

    protected float spriteWidth;
    protected float spriteHeight;

    public List<Items> Inventory = new List<Items>();

    protected void DisplayHealthBar(float xpos, float ypos, float sizeMultiplier)
    {
        Raylib.DrawRectangle((int)xpos, (int)ypos, (int)(maxHP * sizeMultiplier) + 10, 60, Color.Gray);
        Raylib.DrawRectangle((int)xpos + 5, (int)ypos + 5, (int)(hp * sizeMultiplier), 50, Color.Green);
    }

    protected void DisplayHealthBar(float xpos, float ypos, float sizeMultiplier, string text, float textSize)
    {
        Raylib.DrawRectangle((int)xpos, (int)ypos, (int)(maxHP * sizeMultiplier) + 10, 60, Color.Gray);
        Raylib.DrawRectangle((int)xpos + 5, (int)ypos + 5, (int)(hp * sizeMultiplier), 50, Color.Green);
        Raylib.DrawText(text, (int)xpos + 10, (int)(ypos - textSize), (int)textSize, Color.Black);
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

    public void ApplyBuffsFromItem()
    {
        if (Inventory.Count > 0)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (!Inventory[i].buffActivated)
                {
                    Inventory[i].buffActivated = true;
                    Inventory[i].båt(this);
                }
            }
        }
        else Console.WriteLine("tomt inventory");
    }

    public abstract void TakenDamage();
}