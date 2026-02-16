abstract class DamageNumbers : MoveableObject
{
    float time = 3;
    float damage;
    Color color;

    public override void Update()
    {
        time -= Raylib.GetFrameTime();
    }

    public override void Draw()
    {
        Raylib.DrawText(damage.ToString(),R(x),R(y),40,color);
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
}