using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedianaTestTask.Jobs
{
    public class Job<T> : IJob<T>
    {
        public event EventHandler<T> Done;
        private IJobTask<T> _task;
        public Job(IJobTask<T> task)
        {
            _task = task;
        }
        public async Task Perform()
        {
            try
            {
                var result = await _task.Perform();
                Done?.Invoke(this, result);
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
