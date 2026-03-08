class Sprite
{
    public string filePath;
    public float width;
    public float height;
    public Texture2D sprite;

    public Sprite(string filePath, float width, float height)
    {
        this.filePath = filePath;
        this.width = width;
        this.height = height;
    }
}