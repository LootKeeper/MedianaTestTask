using MedianaTestTask.Jobs;
using MedianaTestTask.Jobs.ConcreteJobs;
using MedianaTestTask.Network;
using MedianaTestTask.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedianaTestTask
{
    class TaskResolver
    {
        List<int> _results;
        JobPool<int> _pool;
        IPayloadValidator _validator;
        IResponseNormalizer _normalizer;
        public TaskResolver()
        {
            _results = new List<int>();
            _validator = new PayloadValidator();
            _normalizer = new ResponseNormalizer();
        }
        public void Resolve()
        {
            _pool = new JobPool<int>(300);
            _pool.NotifyOnJobDone += HandleResult;
            for (int i = 1; i <= 2018; i++)
            {
                var job = new IntJob(new Receiver(new ReceiverSettings(i), _validator, _normalizer));
                _pool.AddJob(job);
            }
        }

        private void HandleResult(object sender, int result)
        {
            _results.Add(result);
            Console.WriteLine($"Total results: {_results.Count}");
            if (_pool.IsPoolEmpty())
            {
                CalcMediana();
            }
        }
        private void CalcMediana()
        {
            _results.Sort();
            bool isEven = _results.Count % 2 == 0;
            if (isEven)
            {
                var middleLeftIndex = _results.Count / 2;
                var middleRightIndex = middleLeftIndex + 1;
                var result = (_results[middleLeftIndex] + _results[middleRightIndex]) / 2;
                PrintMediana(result); // 4897494;
            }
            else
            {
                var middleElement = (_results.Count / 2) + 1;
                var result = (_results[middleElement]);
                PrintMediana(result);
            }
        }

        private void PrintMediana(int mediana)
        {
            Console.WriteLine($"Mediana is: {mediana}");
        }
    }
}
