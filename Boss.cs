abstract class Boss : FightableObject
{
    public int screenSizeX;
    public int screenSizeY;
    protected bool isActiveBoss = false;

    public bool Active
    {
        get => isActiveBoss;

        set => isActiveBoss = value;
    }

    protected float contactDamage;

    protected delegate void BossAttack(float damage, float exitTime);

    

    protected List<BossAttack> bossAttacks = [];



    public Texture2D sprite;

    public Texture2D ChangeSpriteSize(Texture2D texture, int width, int height)
    {
        Image image = Raylib.LoadImageFromTexture(texture);
        Raylib.ImageResize(ref image, width, height);
        return Raylib.LoadTextureFromImage(image);
    }

    public void DrawTexture(Texture2D texture, Color color)
    {
        Raylib.DrawTexture(texture, (int)x, (int)y, color);
    }
}