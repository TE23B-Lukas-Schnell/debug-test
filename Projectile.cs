abstract class Projectile : MoveableObject
{
    protected float damage;
    protected bool piercing = false;
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

    protected Projectile(float x, float y, float width, float height, float xSpeed, float ySpeed, float gravity, float damage)
    {


        this.x = x;
        this.y = y;
        this.xSpeed = xSpeed;
        this.ySpeed = ySpeed;
        this.width = width;
        this.height = height;
        this.gravity = gravity;
        this.damage = damage;

        objectIdentifier = "projectile";
        hitbox = new(new Rectangle(x, y, width, height), this);

        ignoreGround = true;
        canGoOffscreen = true;

    }
}