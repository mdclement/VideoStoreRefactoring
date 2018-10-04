using System;
using VideoStore.Core;

namespace VideoStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new RentalInfo().Statement());
            Console.ReadLine();
        }
    }
}
