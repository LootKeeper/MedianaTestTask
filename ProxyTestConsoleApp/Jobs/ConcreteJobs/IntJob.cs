using System;
using System.Collections.Generic;
using System.Text;

namespace MedianaTestTask.Jobs.ConcreteJobs
{
    public class IntJob : Job<int>
    {
        public IntJob(IJobTask<int> task) : base(task)
        {
        }
    }
}
