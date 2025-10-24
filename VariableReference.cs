class VariableReference<variable>
{
    public variable variabel;
    
    public VariableReference(variable value)
    {
        variabel = value;
        Console.WriteLine(value);
        Console.WriteLine(variabel);
    }
                
}