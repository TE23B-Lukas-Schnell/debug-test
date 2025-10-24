abstract class Items
{
    public string name;
    public string description;
    Player playerReference;
    public List<float> valuesToChange;
    public float valueAmount;
    public bool pickedUp;

    Items(float valueChange, Player playerReference, string name, string description)
    {
        valueAmount = valueChange;
        this.playerReference = playerReference;
        this.playerReference.Inventory.Add(this);


    }
}