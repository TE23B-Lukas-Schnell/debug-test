interface ISprite
{
    public Texture2D ChangeSpriteSize(Texture2D texture, int width, int height)
    {
        Image image = Raylib.LoadImageFromTexture(texture);
        Raylib.ImageResize(ref image, width, height);
        return Raylib.LoadTextureFromImage(image);
    }
    public void DrawTexture(Texture2D texture, Color color, float x, float y)
    {
        Raylib.DrawTexture(texture, (int)x, (int)y, color);
    }
}