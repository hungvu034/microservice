using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json ; 
namespace Infrastructure.Common
{
    public class SerializeService : ISerializeService
    {
        public T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public string Serialize<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        public string Serialize<T>(T entity, Type type)
        {
            return JsonConvert.SerializeObject(entity , type , new JsonSerializerSettings());
        }
    }
}