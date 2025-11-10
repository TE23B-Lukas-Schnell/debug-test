abstract class Boss : FightableObject
{
    public int screenSizeX;
    public int screenSizeY;

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