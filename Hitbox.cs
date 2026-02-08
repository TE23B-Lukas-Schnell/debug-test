class Hitbox
{
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
            for (int i = 0; i < GibbManager.currentRun.hitboxes.Count; i++)
            {
              Raylib.DrawRectangle((int)MathF.Round(GibbManager.currentRun.hitboxes[i].hitbox.X), (int)MathF.Round(GibbManager.currentRun.hitboxes[i].hitbox.Y), (int)MathF.Round(GibbManager.currentRun.hitboxes[i].hitbox.Width), (int)MathF.Round(GibbManager.currentRun.hitboxes[i].hitbox.Height), new Color((byte)GibbManager.currentRun.hitboxes[i].color.R, (byte)GibbManager.currentRun.hitboxes[i].color.G, (byte)GibbManager.currentRun.hitboxes[i].color.B, (byte)180));
            }
        }
    }

    public bool remove = false;
    public Rectangle hitbox;
    public MoveableObject owner;
    Color color;

    public void DeleteHitbox()
    {
        // System.Console.WriteLine("hejdå köttig hitbox:" + this);
        remove = true;
    }

    public void Print()
    {
        System.Console.WriteLine(hitbox.X + hitbox.Y + hitbox.Width + hitbox.Height);
    }

    public Hitbox(Rectangle hitbox, MoveableObject owner)
    {
        this.hitbox = hitbox;
        this.owner = owner;
        color = Color.Red;
    }

    public Hitbox(Rectangle hitbox, MoveableObject owner, Color color)
    {
        this.hitbox = hitbox;
        this.owner = owner;
        this.color = color;
    }
}