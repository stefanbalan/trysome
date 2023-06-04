// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using Lazy.Util.EntityModelMapper;

var sw = new Stopwatch();




var src = new Source( );
var dst = new Destination();


var m1 = new EntityModelMapper<Source, Destination>
    .Mapping<Source, Destination, int>(s => s.TestProperty1, d => d.TestProperty1);


sw.Start();
for(int i = 1; i <= 100000; i+=1)
{
    src.TestProperty1 = i;
    m1.Apply(src, dst);
    if (dst.TestProperty1 != i) throw new Exception("Failed apply!!!");
}
sw.Stop();
Console.WriteLine($"Property, apply method 1 {sw.Elapsed}");


sw.Reset();
sw.Start();
for(int i = 1; i <= 100000; i+=1)
{
    src.TestProperty1 = i;
    var changed = m1.ApplyConditional(src, dst);
    if(!changed) throw new Exception("Failed apply!!!");
    if (dst.TestProperty1 != i) throw new Exception("Failed apply!!!");
}
sw.Stop();
Console.WriteLine($"Property, apply method 1 {sw.Elapsed} (with change detection)");
sw.Reset();



// sw.Reset();
// sw.Start();
// for(int i = 1; i <= 100000; i+=1)
// {
//     src.TestProperty1 = i;
//     m1.Apply2(src, dst);
//     if (dst.TestProperty1 != i) throw new Exception("Failed apply!!!");
// }
// sw.Stop();
// Console.WriteLine($"Property, apply method 2 {sw.Elapsed} (with boxing)");
// sw.Reset();






class Source
{
    public int TestProperty1 { get; set; }
    public string TestProperty2 { get; set; }
    public int TestField;
}

class Destination
{
    public int TestProperty1 { get; set; }
    public string TestProperty2 { get; set; }
    public int TestField;
}
