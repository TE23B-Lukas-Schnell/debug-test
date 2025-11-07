abstract class Boss : MoveableObject
{
    public int screenSizeX;
    public int screenSizeY;

    public Texture2D sprite;

    public Texture2D ChangeSpriteSize(Texture2D texture, int width, int height)
    {
        Image image = Raylib.LoadImageFromTexture(texture);
        image.Width = width;
        image.Height = height;
        return Raylib.LoadTextureFromImage(image);
    }

    public void DrawTexture(Texture2D texture, Color color)
    {   
        Raylib.DrawTexture(texture, (int)x, (int)y, color);
    }



}