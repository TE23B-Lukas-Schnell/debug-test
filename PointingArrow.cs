class PointningArrow : Object
{
    public float Rotation
    {
        get;
        set;
    }

    public bool Fill
    {
        set;
        get;
    }

    SpriteDrawer spriteDrawer = new();
    Object objectTarget;

    string hollowSpriteName = "hollow";
    string fillSpriteName = "fill";
    string hollowArrowSpritePath = "./Sprites/assets/hollowArrow.png";
    string fillArrowSPritePath = "./Sprites/assets/arrowFill.png";


    public override void Update()
    {
        x = objectTarget.x;
        y = objectTarget.y;
    }

    public override void Draw()
    {
        spriteDrawer.DrawTexture(Color.White, x, y);
        if (Fill) spriteDrawer.DrawTexture(Color.White, x, y);

    }

    public override void Despawn()
    {

    }

    public override void BeginDraw()
    {
        spriteDrawer.currentSprite = hollowSpriteName;
        spriteDrawer.InitializeSprite();
    }

    public override void AddToGameList()
    {
        GibbManager.currentRun.AddToGameList(this);
    }

    public PointningArrow(float x, float y, Object obj)
    {
        width = 32 * 2;
        height = 17 * 2;
        objectTarget = obj;
        this.x = x;
        this.y = y;

        spriteDrawer.DefineSprite(hollowArrowSpritePath, width, height, hollowSpriteName);
        spriteDrawer.DefineSprite(fillArrowSPritePath, width, height, fillSpriteName);
    }
}