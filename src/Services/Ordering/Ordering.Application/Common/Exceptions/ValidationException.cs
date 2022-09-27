using FluentValidation.Results;

namespace Ordering.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
         IDictionary<string,string[]> Errors { get; set; } = new Dictionary<string,string[]>();

         public ValidationException() : base("One or more validation failure have occurred"){
         }

         public ValidationException(IEnumerable<ValidationFailure> failures){
         //   failures.GroupBy(e => e.PropertyName)
         //       .ToDictionary(group => group.Key , group => group.Select(x => x.ErrorMessage));
            Errors = failures.GroupBy(e => e.PropertyName , e => e.PropertyName)
                    .ToDictionary(group => group.Key , group => group.ToArray());
         }
    }
}