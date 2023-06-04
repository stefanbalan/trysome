namespace Lazy.Util.Tests.Model;

public class Source
{
    public int TestProperty1 { get; set; }
    public int TestProperty2 { get; private set; }
    public int TestProperty3 { get; } = 1;
    public int TestField;

    public int IntField;
    public int IntProperty { get; set; }
    public bool BoolProperty { get; set; }

    public string TestReferenceType { get; set; } = null!;

    public int TestMethod(int num, int x) => num + x;

    public Source()
    {
    }

    public Source(int testProperty2)
    {
        TestProperty2 = testProperty2;
    }
}