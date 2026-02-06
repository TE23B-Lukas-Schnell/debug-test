class SpriteDrawer
{
    float spriteWidth;
    float spriteHeight;

    public Texture2D sprite;

    public float SpriteHeight
    {
        get => spriteHeight;

        set
        {
            spriteHeight = value;
            sprite = ChangeSpriteSize();
        }
    }
    public float SpriteWidth
    {
        get => spriteWidth;

        set
        {
            spriteWidth = value;
            sprite = ChangeSpriteSize();
        }
    }

    Texture2D ChangeSpriteSize()
    {
        Image image = Raylib.LoadImageFromTexture(sprite);
        Raylib.ImageResize(ref image, (int)MathF.Round(spriteWidth), (int)MathF.Round(spriteHeight));
        
        return Raylib.LoadTextureFromImage(image);
    }
    public void DrawTexture( Color color, float x, float y)
    {
        Raylib.DrawTexture(sprite, (int)MathF.Round(x), (int)MathF.Round(y), color);
    }

    public void LoadSprite(Texture2D sprite, float width, float height)
    {
        this.sprite = sprite;
        spriteWidth = width;
        spriteHeight = height;

         this.sprite = ChangeSpriteSize();
    }
}