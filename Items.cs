class Items
{
    public string name;
    public string description;
    Player playerReference;

    public void OnPickepUp()
    {

    }
    
    public void Update()
    {
        
    }

    Items(ref float playerStatToChange)
    {
        playerReference.Inventory.Add(this);
    }
}