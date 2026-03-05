class Sprite
{
    public string filepath;
    public float width;
    public float height;
    public Texture2D sprite;

    public Sprite(string filepath, float width, float height)
    {
        this.filepath = filepath;
        this.width = width;
        this.height = height;
    }
}