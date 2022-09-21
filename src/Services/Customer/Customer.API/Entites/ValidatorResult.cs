using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.Entites
{
    public class ValidatorResult
    {
        public bool IsValid {get ; set; }
        public Dictionary<string , string[]> Errors { get; set; } = new Dictionary<string, string[]>() ; 
    }
}