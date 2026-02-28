using System.Numerics;

class SpriteDrawer
{
    float spriteWidth;
    float spriteHeight;
    float rotation = 0;

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

    public float Rotation
    {
        get => rotation;
        set
        {
            //fixa så att det bara kan bli 360 grader någon gång
            rotation = value;
        }
    }

    Texture2D ChangeSpriteSize()
    {
        Image image = Raylib.LoadImageFromTexture(sprite);
        Raylib.ImageResize(ref image, (int)MathF.Round(spriteWidth), (int)MathF.Round(spriteHeight));

        return Raylib.LoadTextureFromImage(image);
    }
    public void DrawTexture(Color color, float x, float y)
    {

        Raylib.DrawTexturePro(sprite,
        new Rectangle(0, 0, spriteWidth, spriteHeight),      // amount of the source image 
        new Rectangle(x + spriteWidth / 2, y + spriteHeight / 2, spriteWidth, spriteHeight),      // position and size of the rectangle
        new Vector2(spriteWidth / 2, spriteHeight / 2),      // origin
        rotation,                                            // rotaion
        color);
    }

    public void DrawTexture(Color color, float x, float y, float originOffsetX, float originOffsetY)
    {
        Raylib.DrawTexturePro(sprite,
        new Rectangle(0, 0, spriteWidth, spriteHeight),      // amount of the source image 
        new Rectangle(x + spriteWidth / 2, y + spriteHeight / 2, spriteWidth, spriteHeight),      // position and size of the rectangle
        new Vector2(originOffsetX, originOffsetY),           // origin
        rotation,                                            // rotaion
        color);
    }

    public float RotateAccordingToMovement(Vector2 vel)
    {
        return MathF.Atan2(-vel.Y, vel.X) * (180f / MathF.PI);
    }

    public void LoadSprite(Texture2D sprite, float width, float height)
    {
        this.sprite = sprite;
        spriteWidth = width;
        spriteHeight = height;

        this.sprite = ChangeSpriteSize();
    }
}