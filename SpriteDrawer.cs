using System.Numerics;

class SpriteDrawer
{
    float rotation = 0;

    public string currentSprite;

    public float originX, originY;

    public float CurrentSpriteWidth
    {
        get => sprites[currentSprite].width;

        set
        {
            sprites[currentSprite].width = value;
            sprites[currentSprite].sprite = ChangeSpriteSize(sprites[currentSprite].sprite, sprites[currentSprite].width, sprites[currentSprite].height);
        }
    }

    public float CurrentSpriteHeight
    {
        get => sprites[currentSprite].height;

        set
        {
            sprites[currentSprite].height = value;
            sprites[currentSprite].sprite = ChangeSpriteSize(sprites[currentSprite].sprite, sprites[currentSprite].width, sprites[currentSprite].height);
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
        Sprite spriteInfo = sprites[name];
        Texture2D actualSprite = Raylib.LoadTexture(spriteInfo.filePath);

        Texture2D resizedSprite = ChangeSpriteSize(actualSprite, spriteInfo.width, spriteInfo.height);

        return resizedSprite;
    }


    Texture2D ChangeSpriteSize(Texture2D sprite, float width, float height)
    {
        Image image = Raylib.LoadImageFromTexture(sprite);
        Raylib.ImageResize(ref image, (int)MathF.Round(width), (int)MathF.Round(height));
        return Raylib.LoadTextureFromImage(image);
    }

    //prints the current sprite
    public void DrawTexture(Color color, float x, float y)
    {
        Raylib.DrawTexturePro(sprites[currentSprite].sprite,
        new Rectangle(0, 0, CurrentSpriteWidth, CurrentSpriteHeight), // amount of the source image 
        new Rectangle(x + CurrentSpriteWidth / 2, y + CurrentSpriteHeight / 2, CurrentSpriteWidth, CurrentSpriteHeight),// position and size of the rectangle
        new Vector2(CurrentSpriteWidth / 2, CurrentSpriteHeight / 2),// origin
        rotation,// rotaion
        color);
    }

    // prints a sprite based on the name, crashes if the name is wrong
    public void DrawTexture(Color color, float x, float y, string spriteName)
    {
        Raylib.DrawTexturePro(sprites[spriteName].sprite,
        new Rectangle(0, 0, CurrentSpriteWidth, CurrentSpriteHeight), // amount of the source image 
        new Rectangle(x + CurrentSpriteWidth / 2, y + CurrentSpriteHeight / 2, CurrentSpriteWidth, CurrentSpriteHeight),// position and size of the rectangle
        new Vector2(CurrentSpriteWidth / 2, CurrentSpriteHeight / 2),// origin
        rotation,// rotaion
        color);
    }

    public float RotateAccordingToMovement(float xSpeed, float ySpeed)
    {
        return MathF.Atan2(-ySpeed, xSpeed) * (180f / MathF.PI);
    }

    public void InitializeSprite()
    {
        // if (sprites == null || sprites.Count < 1)
        // {
        string[] keys = sprites.Keys.ToArray();

        for (int i = 0; i < keys.Length; i++)
        {
            sprites[keys[i]].sprite = LoadSprite(keys[i]);   //sprite 
            System.Console.WriteLine($"sprite loaded succesfully: {keys[i]}");
            PrintSpriteInfo(sprites[keys[i]]);
        }

        if (keys.Length == 1)
        {
            currentSprite = keys[0];
        }
        // }
    }

    // this is for objects that only have 1 sprite, which is most things in this game
    public void DefineSprite(string filePath, float width, float height)
    {
        AddSprite("sprite", filePath, width, height);
    }

    public void DefineSprite(string filePath, float width, float height, string spriteName)
    {
        AddSprite(spriteName, filePath, width, height);
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
            PrintSpriteInfo(sprites[keys[i]]);
        }
        System.Console.WriteLine();

    }

    void PrintSpriteInfo(Sprite info)
    {
        System.Console.WriteLine($"fil: {info.filePath}, w:{info.width}, h:{info.height}");
    }
}