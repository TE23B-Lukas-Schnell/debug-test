abstract class Object
{
    // detta är min avrundnings funktion
    protected static int R(float input) => (int)MathF.Round(input);

    public string objectIdentifier = "";
    public bool remove = false;
    public float x, y;
    public float width, height;

    //körs varje frame
    abstract public void Update();
    //körs varje frame, används för att rita saker till skärmen
    abstract public void Draw();
    //körs när objektet tas bort
    abstract public void Despawn();
    //körs innan spelet börjar
    abstract public void BeginDraw();
    //gör så att alla objekt hamnar i listan
    abstract public void AddToGameList();

    protected Object()
    {
        AddToGameList();
    }
}