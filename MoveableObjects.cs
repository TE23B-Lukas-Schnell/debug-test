abstract class MoveableObject()
{
    //lista för alla objekt som ska hanteras, det är lista för att den kan öka och minska under runtime
    public static List<MoveableObject> gameList = new List<MoveableObject>();

    // Pending adds to avoid modifying gameList while it's iterated from other threads/tasks
    private static readonly List<MoveableObject> pendingAdds = new List<MoveableObject>();


    private static readonly object gameListLock = new object();

    // kan användas säkert i alla threads
    public static void AddToGameList(MoveableObject obj)
    {
        lock (gameListLock)
        {
            pendingAdds.Add(obj);
        }
    }

    //lägger till alla objekt som väntar
    public static void AddPendingObjects()
    {
        lock (gameListLock)
        {
            if (pendingAdds.Count > 0)
            {
                gameList.AddRange(pendingAdds);
                pendingAdds.Clear();
            }
        }
    }

    public static float globalGravityMultiplier = 1;

    public string objectIdentifier = "";
    public bool remove = false;
    public float damageMultiplier = 1;
    public float healMultiplier = 1;
    protected float x, y;
    protected float xSpeed, ySpeed;
    protected float width, height;
    protected bool canGoOffscreen = false;
    protected bool ignoreGround = false;

    protected bool Grounded() => y >= Raylib.GetScreenHeight() - height;

    protected Rectangle GetHitbox() => new Rectangle(x, y, width, height);
    protected bool ShowHitboxesSwitch() => Raylib.IsKeyDown(KeyboardKey.E);

    protected void ShowHitboxes()//enklare att testa programmet och kan hjälpa senare när hitboxes inte matchar spriten
    {
        if (ShowHitboxesSwitch())
        {
            Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, Color.Red);
        }
    }

    //returnar objektet som kollideras med 
    protected MoveableObject? CheckCollisions()
    {
        foreach (MoveableObject obj in gameList)
        {
            if (obj != this)
            {
                if (Raylib.CheckCollisionRecs(GetHitbox(), obj.GetHitbox()))
                {
                    return obj;
                    // Console.WriteLine("${obj} asg nazg durbatuluk asg nazg gimbatul asg nazg thrakatuluk av jack");
                }
            }
        }
        return null;
    }

    Queue<(float x, float y)> lastPositions = new Queue<(float x, float y)>();

    protected int maxTrailSize = (int)MathF.Round(Raylib.GetFPS() * 0.16666666667f);

    protected void ClearLastPositions()
    {
        lastPositions.Clear();
    }

    //denna funktion gjordes av chatgpt
    protected void AddTrailEffects(int maxTrailSize, Color trailColorSet, float rMultiplier, float gMultiplier, float bMultiplier, float aMultiplier)
    {
        // System.Console.WriteLine("mattigt bärre:" + (int)(GibbManager.targetFrameRate * 0.1666666666667f));
        // System.Console.WriteLine("köttig lastpositions array length:" + lastPositions.Count);

        lastPositions.Enqueue((x, y));
        // kortar ner antalet postioner till en sjättedel av fpsen
        while (lastPositions.Count > maxTrailSize) lastPositions.Dequeue();

        int count = lastPositions.Count;
        int i = 0;

        foreach (var pos in lastPositions)
        {
            float trailTime = (float)(count - i) / count;
            Color trailColor = new Color(trailColorSet.R + (int)(rMultiplier * trailTime), trailColorSet.G + (int)(gMultiplier * trailTime), trailColorSet.B + (int)(bMultiplier * trailTime), trailColorSet.A + (int)(aMultiplier * trailTime));
            //clamps color values
            {
                trailColorSet.R = Math.Clamp(trailColorSet.R, (byte)0, (byte)255);
                trailColorSet.G = Math.Clamp(trailColorSet.G, (byte)0, (byte)255);
                trailColorSet.B = Math.Clamp(trailColorSet.B, (byte)0, (byte)255);
                trailColorSet.A = Math.Clamp(trailColorSet.A, (byte)0, (byte)255);
            }
            Raylib.DrawRectangle((int)pos.x, (int)pos.y, (int)width, (int)width, trailColor);
            i++;
        }
    }

    //funktioner relaterade till positions värden

    // förklarar sig själv tycker jag, positionen kan aldrig gå offscreen
    protected void LimitMovement()
    {
        x = Math.Clamp(x, 0, Raylib.GetScreenWidth() - width);
        y = Math.Clamp(y, 0, Raylib.GetScreenHeight() - height);
    }

    //tar bort objekt om de är offscreen
    protected void HandleOffscreen()
    {
        if (canGoOffscreen)
        {
            bool isOffscreen = x + width < 0 || x > Raylib.GetScreenWidth() || y + height < 0 || y > Raylib.GetScreenHeight();

            if (isOffscreen)
            {
                remove = true;
            }
        }
        else
        {
            LimitMovement();
        }
    }

    //gör så att objektet blir påverkat av gravitation, specifiera i parametern
    protected void ApplyGravity(float gravity)
    {
        if (!Grounded())
        {
            ySpeed -= gravity * globalGravityMultiplier * Raylib.GetFrameTime();
        }
        else
        {
            if (!ignoreGround) ySpeed = 0;
        }
    }

    //flyttar objekt, denna funktion behövs för alla objekt som rör på sig
    protected void MoveObject(float gravity)
    {
        x += xSpeed * Raylib.GetFrameTime();
        y -= ySpeed * Raylib.GetFrameTime();

        ApplyGravity(gravity);
        HandleOffscreen();
    }

    //körs varje frame
    abstract public void Update();
    //körs varje frame, används för att rita saker till skärmen
    abstract public void Draw();
    //körs när objektet tas bort
    abstract public void Despawn();
    //körs innan spelet börjar
    abstract public void BeginDraw();
}