abstract class MoveableObject
{
    public static float globalGravityMultiplier = 1;

    public string objectIdentifier = "";
    public bool remove = false;
    public Hitbox hitbox;
    public float x, y;
    public float xSpeed, ySpeed;
    public float width, height;
    protected bool canGoOffscreen = false;
    protected bool ignoreGround = false;

    // detta är min avrundnings funktion
    protected static int R(float input) => (int)MathF.Round(input);

    protected bool Grounded() => y >= Raylib.GetScreenHeight() - height;

    protected Hitbox GetHitbox() => hitbox;

    protected void UpdateHitboxPosition(float x, float y, float w, float h)
    {
        hitbox.hitbox = new Rectangle(R(x), R(y), R(w), R(h));
    }

    //returnar objektet som kollideras med 
    protected MoveableObject? CheckCollisions()
    {
        foreach (Hitbox obj in GibbManager.currentRun.hitboxes)
        {
            if (Raylib.CheckCollisionRecs(GetHitbox().hitbox, obj.hitbox))
            {
                // Console.WriteLine("${obj} asg nazg durbatuluk asg nazg gimbatul asg nazg thrakatuluk av jack");
                return obj.owner;
            }
        }
        return null;
    }

    //returnar objektet som kollideras med den angivna hitboxen
    protected MoveableObject? CheckCollisions(Hitbox hitbox)
    {
        foreach (Hitbox obj in GibbManager.currentRun.hitboxes)
        {
            // skip hitboxes that belong to the same owner (avoid self-collision)
            if (obj.owner == hitbox.owner) continue;

            if (Raylib.CheckCollisionRecs(hitbox.hitbox, obj.hitbox))
            {
                // Console.WriteLine("${obj} asg nazg durbatuluk asg nazg gimbatul asg nazg thrakatuluk av jack");
                return obj.owner;
            }
        }
        return null;
    }

    Queue<(float x, float y)> lastPositions = new Queue<(float x, float y)>();

    protected int maxTrailSize = R(Raylib.GetFPS() * 0.16666666667f);

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
            Raylib.DrawRectangle((int)pos.x, (int)pos.y, (int)width, (int)height, trailColor);
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

    abstract public void AddToGameList();

    protected MoveableObject()
    {
        AddToGameList();
        hitbox = new(new Rectangle(x, y, width, height), this);
    }
}