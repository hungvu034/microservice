using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Anotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MongoDbCollectionAtributte : Attribute
    {
        public MongoDbCollectionAtributte(string collectionName)
        {
            CollectionName = collectionName;
        }

        public string CollectionName { get; set; }
    }
}