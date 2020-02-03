using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedianaTestTask.Jobs
{
    public interface IJobTask<T>
    {
        ValueTask<T> Perform();
    }
}
