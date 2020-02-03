using System;
using System.Collections.Generic;
using System.Text;

namespace MedianaTestTask.Services
{
    public class ResponseNormalizer : IResponseNormalizer
    {
        public int Normalize(string message)
        {
            var filteredMessage = message.Replace(" ", "").Replace(".", "");
            if (String.IsNullOrEmpty(filteredMessage))
                throw new Exception($"Result is incorrect: {message}");
            return int.Parse(filteredMessage);
        }
    }
}
