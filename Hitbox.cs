class Hitbox
{
    public static List<Hitbox> hitboxes = [];

    static readonly List<Hitbox> pendingAdds = [];
    static readonly List<Hitbox> pendingRemoves = [];
    static readonly object listLock = new object();

    //lägger till alla objekt som väntar
    public static void AddPendingHitboxes()
    {
        lock (listLock)
        {
            if (pendingAdds.Count > 0)
            {
                hitboxes.AddRange(pendingAdds);
                pendingAdds.Clear();
            }
        }
    }

    // kan användas säkert i alla threads
    public static void AddToHitboxList(Hitbox obj)
    {
        lock (listLock)
        {
            pendingAdds.Add(obj);
        }
    }

    static bool ShowHitboxesSwitch = false;
    //enklare att testa programmet och kan hjälpa senare när hitboxes inte matchar spriten
    public static void ShowHitboxes()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.E))
        {
            ShowHitboxesSwitch = !ShowHitboxesSwitch;
        }

        if (ShowHitboxesSwitch)
        {
            for (int i = 0; i < hitboxes.Count; i++)
            {
                Raylib.DrawRectangle((int)MathF.Round(hitboxes[i].hitbox.X), (int)MathF.Round(hitboxes[i].hitbox.Y), (int)MathF.Round(hitboxes[i].hitbox.Width), (int)MathF.Round(hitboxes[i].hitbox.Height), Color.Red);
            }
        }
    }

    public bool remove = false;
    public Rectangle hitbox;
    public MoveableObject owner;

    public void DeleteHitbox()
    {
        // System.Console.WriteLine("hejdå köttig hitbox:" + this);
        remove = true;
    }

    public Hitbox(Rectangle hitbox, MoveableObject owner)
    {
        this.hitbox = hitbox;
        this.owner = owner;
        AddToHitboxList(this);
    }
}