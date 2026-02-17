class DamageNumbers : MoveableObject
{
    float time = 2;
    float damage;
    Color color;
    float textSize = 40;
    Random random;
    float offsetX;
    float offsetY;

    public override void Update()
    {
        time -= Raylib.GetFrameTime();
        if (time <= 0) remove = true;
        ySpeed = 5;
        MoveObject(0);
    }

    public override void Draw()
    {
        color.A -= (byte)Raylib.GetFrameTime();

        Raylib.DrawText(((int)damage).ToString(), R(x + (offsetX - 50)), R(y + (offsetY - 25)), R(textSize), color);
    }

    public override void Despawn()
    {

    }

    public override void BeginDraw()
    {

    }

    public override void AddToGameList()
    {
        GibbManager.currentRun.AddToGameList(this);
    }

    public DamageNumbers(float x, float y, float damage)
    {
        color = Color.Black;
        this.x = x;
        this.y = y;
        this.damage = damage;

        random = new Random();
        offsetX = random.Next(100);
        offsetY = random.Next(50);
    }
}