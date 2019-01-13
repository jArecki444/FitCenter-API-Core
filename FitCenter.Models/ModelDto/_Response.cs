using System.Collections.Generic;

namespace FitCenter.Models.ModelDto
{
    public class Response<T> where T : class
    {
        public T SuccessResult { get; set; }

        public bool ErrorOccurred => Errors.Count > 0;

        public IDictionary<string, List<string>> Errors;

        public Response()
        {
            Errors = new Dictionary<string, List<string>>();
        }

        public void AddError(string key, string value)
        {
            if (!Errors.Keys.Contains(key))
            {
                Errors.Add(key, new List<string>());
            }
            Errors[key].Add(value);
        }

        public void RemoveError(string key)
        {
            Errors.Remove(key);
        }
    }
}