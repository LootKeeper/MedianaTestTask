using MedianaTestTask.Jobs.ConcreteJobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedianaTestTaskTests.Jobs
{
    public class FakeJob : IIntJobTask
    {
        public ValueTask<int> Perform()
        {
            throw new NotImplementedException();
        }
    }
}
