interface ISprite
{
    // protected float spriteWidth;
    // protected float spriteHeight;
    // protected Texture2D sprite;

    public Texture2D ChangeSpriteSize(Texture2D texture, int width, int height)
    {
        Image image = Raylib.LoadImageFromTexture(texture);
        Raylib.ImageResize(ref image, width, height);
        return Raylib.LoadTextureFromImage(image);
    }
    public void DrawTexture(Texture2D texture, Color color, float x, float y)
    {
        Raylib.DrawTexture(texture, (int)MathF.Round(x), (int)MathF.Round(y), color);
    }
}