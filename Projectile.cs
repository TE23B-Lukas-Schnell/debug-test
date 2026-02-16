abstract class Projectile : MoveableObject
{
    protected float damage;
    protected bool piercing = false;
    protected float gravity;
    List<MoveableObject> objectsAlreadyHit = [];

    // when you collide with an enemy, check whether 
    public void OnHit(float damage, string objectIdentifier)
    {
        FightableObject? target;

        MoveableObject? träffatObjekt = CheckCollisions(hitbox);
        if (träffatObjekt is FightableObject)
        {
            target = träffatObjekt as FightableObject;
            if (target != null && !objectsAlreadyHit.Contains(target))
            {
                if (target.objectIdentifier == objectIdentifier)
                {
                    //ok båtig
                    if (!piercing)
                    {
                        hitbox.DeleteHitbox();
                        remove = true;
                        target.TakeDamage(damage, target);
                    }
                    else
                    {
                        objectsAlreadyHit.Add(target);
                        target.TakeDamage(damage, target);
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