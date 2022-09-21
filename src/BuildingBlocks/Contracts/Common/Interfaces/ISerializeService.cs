using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Common.Interfaces
{
    public interface ISerializeService
    {
        string Serialize<T>(T entity);
        string Serialize<T> (T entity , Type type);

        T Deserialize<T>(string jsonString); 
    }
}