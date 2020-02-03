using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedianaTestTask.Jobs
{
    public interface IJob<T>
    {
        event EventHandler<T> Done;
        Task Perform();
    }
}
