using System;

namespace MedianaTestTask
{
    class Program
    {
        static void Main(string[] args)
        {            
            TaskResolver resolver = new TaskResolver();
            resolver.Resolve();
            Console.ReadLine();            
        }
    }
}
