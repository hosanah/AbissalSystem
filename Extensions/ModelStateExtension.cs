using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AbissalSystem.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErros(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach (var model in modelState.Values)
                result.AddRange(model.Errors.Select(error => error.ErrorMessage));

            return result;
        }
    }
}