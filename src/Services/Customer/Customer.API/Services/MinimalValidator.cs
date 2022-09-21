using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.API.Entites;
using Customer.API.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Customer.API.Services
{
    public class MinimalValidator : IMinimalValidator
    {
        public ValidatorResult Validate<T>(T entity)
        {
            ValidatorResult result = new ValidatorResult();
            result.IsValid = true ; 
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach(PropertyInfo property in propertyInfos){
               var attributes = property.GetCustomAttributes(typeof(ValidationAttribute) , true);
                foreach(var attribute in attributes){
                    ValidationAttribute validation = attribute as ValidationAttribute ; 
                    if(validation != null){
                       bool isValid = validation.IsValid(property.GetValue(entity));
                       if(!isValid){
                        result.IsValid = false ; 
                        string PropertyName = property.Name ;
                        if(result.Errors.ContainsKey(PropertyName)){
                            string[] Errors = result.Errors.GetValueOrDefault(PropertyName);
                            List<string> errors = Errors.ToList();
                            errors.Add(validation.FormatErrorMessage(PropertyName));
                            Errors = errors.ToArray();
                            result.Errors[PropertyName] = Errors ; 
                        }
                        else{
                            result.Errors.Add(PropertyName , new string[]{ validation.FormatErrorMessage(PropertyName)});
                        }
                       }
                    }
                }
            } 
            return result; 
        }
    }
}