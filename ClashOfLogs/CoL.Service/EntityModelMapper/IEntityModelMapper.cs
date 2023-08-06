// ReSharper disable UnusedMember.Global

namespace CoL.Service.EntityModelMapper;


public interface IEntityModelMapper<T1, T2>
{
    T2 Get2From(T1 o1);
    T1 Get1From(T2 o2);
    bool UpdateT1FromT2(T1 o1, T2 o2);
    bool UpdateT2FromT1(T2 o1, T1 o2);
}
