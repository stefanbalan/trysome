// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using Lazy.Util.EntityModelMapper;

var sw = new Stopwatch();




var src = new Source( );
var dst = new Destination();





var m = new EntityModelMapperBase<Source, Destination>
    .Mapping<Source, Destination, int>(s => s.TestProperty1, d => d.TestProperty1);


sw.Start();
for(int i = 1; i <= 100000; i+=1)
{
    src.TestProperty1 = i;
    m.Apply(src, dst);
    if (dst.TestProperty1 != i) throw new Exception("Failed apply!!!");
}
sw.Stop();
Console.WriteLine($"Property, apply method 1 {sw.Elapsed}");

sw.Reset();
sw.Start();
for(int i = 1; i <= 100000; i+=1)
{
    src.TestProperty1 = i;
    m.Apply2(src, dst);
    if (dst.TestProperty1 != i) throw new Exception("Failed apply!!!");
}
sw.Stop();
Console.WriteLine($"Property, apply method 2 {sw.Elapsed}");



class Source
{
    public int TestProperty1 { get; set; }
    public int TestProperty2 { get; private set; }
    public int TestProperty3 { get; } = 1;
    public int TestField;

    public Source()
    {
    }

    public Source(int testProperty2)
    {
        TestProperty2 = testProperty2;
    }
}

class Destination
{
    public int TestProperty1 { get; set; }
    public int TestProperty2 { get; private set; }
    public int TestProperty3 { get; }
    public int TestField;
}
