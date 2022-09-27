using System.Reflection;
using AutoMapper;
using Serilog ; 
namespace Ordering.Application.Common.Mappings
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);
            
            const string mappingMethodName = nameof(IMapFrom<object>.Mapping); // Mapping

            bool HasInterface(Type t) => t.IsGenericType 
                                        && t.GetGenericTypeDefinition() == mapFromType;
            
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces()
                    .Any(HasInterface)).ToList();
            // lấy ra các type kế thừa từ IMapFrom 
            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                
                var methodInfo = type.GetMethod(mappingMethodName);
                Console.WriteLine(type.Name);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces()
                        .Where(HasInterface).ToList();
                    if (interfaces.Count <= 0) continue;
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName,argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        
        }
    }
}
