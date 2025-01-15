namespace TheClient;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal class MyAttribute : Attribute
{
    public int MinAge { get; set; }
}
