abstract class Projectile : MoveableObject
{
    protected float damage;
    protected bool piercing = false;

    // when you collide with an enemy, check whether 
    public void OnHit(float damage, string objectIdentifier)
    {
        FightableObject? target;

        if (CheckCollisions() is FightableObject)
        {
            target = CheckCollisions() as FightableObject;
            if (target != null)
            {
                if (target.objectIdentifier == objectIdentifier)
                {
                    //ok båtig
                    target.TakeDamage(damage, target);
                    if (!piercing)
                    {
                        hitbox.DeleteHitbox();
                        remove = true;
                    }
                }
            }
        }
    }

    public override void Despawn()
    {
        
    }
    public override void BeginDraw()
    {

    }

    protected Projectile()
    {
        objectIdentifier = "projectile";
    }
}