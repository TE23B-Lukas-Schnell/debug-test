abstract class Projectile : MoveableObject
{
    protected float damage;
    protected bool piercing = false;
    protected float gravity;

    // when you collide with an enemy, check whether 
    public void OnHit(float damage, string objectIdentifier)
    {
        FightableObject? target;

        MoveableObject? träffatObjekt = CheckCollisions(hurtbox);
        if (träffatObjekt is FightableObject)
        {
            target = träffatObjekt as FightableObject;
            if (target != null)
            {
                if (target.objectIdentifier == objectIdentifier)
                {
                    //ok båtig
                    target.TakeDamage(damage, target);
                    if (!piercing)
                    {
                        hurtbox.DeleteHitbox();
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

    public override void AddToGameList()
    {
        GibbManager.currentRun.AddToGameList(this);
    }

    protected Projectile()
    {
        objectIdentifier = "projectile";
    }
}