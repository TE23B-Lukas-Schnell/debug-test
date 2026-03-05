class PointingArrow
{
    SpriteDrawer arrowDrawer = new();
    SpriteDrawer lightUpArrowDrawer = new();
    Color color;
    string hollowArrowSpriteFilePath = "./Sprites/assets/hollowArrow.png";
    string lightUpArrowSpriteFilePath = "./Sprites/assets/arrowFill.png";

    float width;
    float height;

    public void DrawArrow(float x, float y, float offsetX, float offsetY, float rotaionInDegrees)
    {
        arrowDrawer.Rotation = rotaionInDegrees;
        arrowDrawer.DrawTexture(color, x, y, offsetX, offsetY);
    }

    public void LightUpArrow(float x, float y, float offsetX, float offsetY, float rotaionInDegrees)
    {
        lightUpArrowDrawer.Rotation = rotaionInDegrees;
        lightUpArrowDrawer.DrawTexture(color, x, y, offsetX, offsetY);
    }

    public void LoadArrowSprite()
    {
        arrowDrawer.InitializeSprite(hollowArrowSpriteFilePath, width, height);
        lightUpArrowDrawer.InitializeSprite(lightUpArrowSpriteFilePath, width, height);
        arrowDrawer.CurrentSpriteWidth = width;
        arrowDrawer.CurrentSpriteHeight = height;
        lightUpArrowDrawer.CurrentSpriteWidth = width;
        lightUpArrowDrawer.CurrentSpriteHeight = height;
    }

    public PointingArrow(Color color, float width, float height, bool loadNow)
    {
        this.color = color;
        this.width = width;
        this.height = height;
        if(loadNow == true) LoadArrowSprite();
    }
}