using System.Numerics;

class SpriteDrawer
{
    float rotation = 0;

    public string currentSprite;

    public float CurrentSpriteWidth
    {
        get => sprites[currentSprite].width;

        set
        {
            sprites[currentSprite].width = value;
        }
    }

    public float CurrentSpriteHeight
    {
        get => sprites[currentSprite].height;

        set
        {
            sprites[currentSprite].height = value;
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

    Dictionary<string, Sprite> sprites = new();

    void AddSprite(string name, string filePath, float w, float h)
    {
        sprites.Add(name, new Sprite(filePath, w, h));
    }

    Texture2D LoadSprite(string name)
    {
        Sprite kött = sprites[name];
        Texture2D båt = Raylib.LoadTexture(kött.filepath);
        båt = ChangeSpriteSize(båt, kött.width, kött.height);
        return båt;
    }

    Texture2D ChangeSpriteSize(Texture2D sprite, float width, float height)
    {
        Image image = Raylib.LoadImageFromTexture(sprite);
        Raylib.ImageResize(ref image, (int)MathF.Round(width), (int)MathF.Round(height));

        return Raylib.LoadTextureFromImage(image);
    }

    public void DrawTexture(Color color, float x, float y)
    {
        Raylib.DrawTexturePro(sprites[currentSprite].sprite,
        new Rectangle(0, 0, CurrentSpriteWidth, CurrentSpriteHeight), // amount of the source image 
        new Rectangle(x + CurrentSpriteWidth / 2, y + CurrentSpriteHeight / 2, CurrentSpriteWidth, CurrentSpriteHeight),// position and size of the rectangle
        new Vector2(CurrentSpriteWidth / 2, CurrentSpriteHeight / 2),// origin
        rotation,// rotaion
        color);
    }

    public void DrawTexture(Color color, float x, float y, float originOffsetX, float originOffsetY)
    {
        Raylib.DrawTexturePro(sprites[currentSprite].sprite,
        new Rectangle(0, 0, CurrentSpriteWidth, CurrentSpriteHeight), // amount of the source image 
        new Rectangle(x + CurrentSpriteWidth / 2, y + CurrentSpriteHeight / 2, CurrentSpriteWidth, CurrentSpriteHeight),// position and size of the rectangle
        new Vector2(originOffsetX, originOffsetY), // origin
        rotation,// rotaion
        color);
    }

    public float RotateAccordingToMovement(Vector2 vel)
    {
        return MathF.Atan2(-vel.Y, vel.X) * (180f / MathF.PI);
    }

    public void InitializeSprite(string filePath, float width, float height)
    {
        PrintDic(1);
        sprites[currentSprite].sprite = LoadSprite("sprite");
    }

    // this is for objects that only have 1 sprite, which is most things in this game
    public void DefineSprites(string filePath, float width, float height)
    {
        AddSprite("sprite", filePath, width, height);

        currentSprite = "sprite";
    }

    void PrintDic(int k)
    {
        k++;
        k--;
        k++;
        string bat = "";

        string[] keys = sprites.Keys.ToArray();

        for (int i = 0; i < keys.Length; i++)
        {
            System.Console.WriteLine($"sprite name: {keys[i]}");
            printSPriteInfo(sprites[keys[i]]);
        }
        System.Console.WriteLine();

    }

    void printSPriteInfo(Sprite info)
    {
        System.Console.WriteLine($"fil: {info.filepath}, w:{info.width}, h:{info.height}");
    }
}