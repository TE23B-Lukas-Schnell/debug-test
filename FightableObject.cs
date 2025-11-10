abstract class FightableObject : MoveableObject
{
    protected float maxHP;
    protected float hp;
    public bool healthy = true;

    protected void DisplayHealthBar(float xpos, float ypos, float sizeMultiplier)
    {
        Raylib.DrawRectangle((int)xpos, (int)ypos, (int)(maxHP * sizeMultiplier) + 10, 60, Color.Gray);
        Raylib.DrawRectangle((int)xpos + 5, (int)ypos + 5, (int)(hp * sizeMultiplier), 50, Color.Green);
    }

    bool changeHp(FightableObject target, float changeAmount, float changeMultiplier, float limit, bool isLimitFloorOrRoof/*true for floor, false for roof*/)
    {
        bool limitReached;

        if (isLimitFloorOrRoof)
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
        if (changeHp(target, damage, damageMultiplier, 0, true))
        {
            Despawn();
            target.remove = true;
        }
    }

    public void healDamage(float healAmount, FightableObject target)
    {
        if (changeHp(target, healAmount, healMultiplier, maxHP, false))
        {
            target.healthy = true;
        }
    }

    //since there is no invin frames after you take damage, make sure the damage is very small 😂😂😂 𝼀𰻝Ꮸ𐃵
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
}


