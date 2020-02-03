using System;
using System.Collections.Generic;
using System.Text;

namespace MedianaTestTask.Services
{
    public interface IResponseNormalizer
    {
        int Normalize(string message);
    }
}
