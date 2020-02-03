using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MedianaTestTask.Services
{
    public class PayloadValidator : IPayloadValidator
    {
        Regex _payloadRool;
        public PayloadValidator()
        {
            _payloadRool = new Regex(@"([0-9]+\s?\.+)");
        }
        public bool IsValid(string message)
        {
            return _payloadRool.IsMatch(message) || message.EndsWith('\r');
        }
    }
}
