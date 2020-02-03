using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedianaTestTask.Jobs
{
    public class JobPool<T>
    {
        public event EventHandler<T> NotifyOnJobDone;

        int _maxJobs;
        ConcurrentQueue<IJob<T>> _jobs;
        List<IJob<T>> _jobsInWork;
        bool _isPoolActive;
        Thread _dequeueThread;
        public JobPool(int maxJobs)
        {
            _maxJobs = maxJobs;
            _isPoolActive = true;
            _jobs = new ConcurrentQueue<IJob<T>>();
            _jobsInWork = new List<IJob<T>>();
            _dequeueThread = new Thread(new ThreadStart(TryDequeueJob));
            _dequeueThread.IsBackground = true;
            _dequeueThread.Start();
        }
        public void AddJob(IJob<T> job)
        {
            job.Done += OnJobDone;
            this._jobs.Enqueue(job);
        }
        public bool IsPoolEmpty()
        {
            return _jobs.Count == 0 && _jobsInWork.Count == 0;
        }
        private void OnJobDone(object sender, T result)
        {
            var job = (IJob<T>)sender;
            job.Done -= OnJobDone;
            _jobsInWork.Remove(job);
            this.NotifyOnJobDone?.Invoke(this, result);
        }

        private void TryDequeueJob()
        {
            Task.Run(async () =>
            {
                while (_isPoolActive)
                {
                    if (_jobsInWork.Count < _maxJobs)
                    {
                        while (_jobsInWork.Count < _maxJobs && _jobs.Count > 0)
                        {
                            DequeueAndRun();
                        }
                    }
                    else
                    {
                        await Task.Delay(5000);
                    }
                }
            });
        }
        private void DequeueAndRun()
        {
            IJob<T> job;
            if (_jobs.TryDequeue(out job))
            {
                PerformJob(job);
            }
        }
        private void PerformJob(IJob<T> job)
        {
            _jobsInWork.Add(job);
            Task.Run(() => job.Perform());
        }
    }
}
