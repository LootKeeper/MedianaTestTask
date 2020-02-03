using System;
using System.Collections.Generic;
using System.Text;

namespace MedianaTestTask.Services
{
    public interface IPayloadValidator
    {
        bool IsValid(string message);
    }
}
