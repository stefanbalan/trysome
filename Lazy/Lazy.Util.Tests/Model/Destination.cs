namespace Lazy.Util.Tests.Model;

public class Destination
{
    public int TestProperty1 { get; set; }
    public int TestProperty2 { get; private set; }
    public int TestProperty3 { get; }
    public int TestField;

    public int IntProperty { get; set; }
    public string TestReferenceType { get; set; } = null!;
}