using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Infrastructure.Mapping
{
    public static class AutoMapperExtension
    {
        public static IMappingExpression<TSource,TDestination> IgnoreAllNonExisting<TSource,TDestination>(this IMappingExpression<TSource,TDestination> expression ){
                var flags = BindingFlags.Public | BindingFlags.Instance ; 
                var SourceType = typeof(TSource);
                var DestinationType = typeof(TDestination);
                foreach (var item in DestinationType.GetProperties())
                {
                    if(SourceType.GetProperty(item.Name , flags) == null){
                        expression.ForMember(item.Name , opt => opt.Ignore());
                    }
                }
                return expression ; 
        }
    }
}