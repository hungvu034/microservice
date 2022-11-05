using AutoMapper;

namespace Ordering.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
         void Mapping(Profile profile) {
            Console.WriteLine("Map: " + typeof(T).Name + ": " + GetType().Name );
            profile.CreateMap(typeof(T) , GetType());
        }

    }
}