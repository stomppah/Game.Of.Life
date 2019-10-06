using GameOfLife.Library;
using System;
using System.Diagnostics;

namespace GameOfLife.PerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new GameOfLife.Library.LifeGrid(200, 200);

            var iterations = 10000;

            Console.WriteLine($"Number of iterations: {iterations}");

            grid.Randomise();
            var stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                grid.UpdateState4();
            Console.WriteLine($"Nested for loop (Optimised): {stopWatch.ElapsedMilliseconds}");

            grid.Randomise();
            stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                grid.UpdateState5();
            Console.WriteLine($"Nested Parallel For loop (Optimised): {stopWatch.ElapsedMilliseconds}");

            grid.Randomise();
            stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                grid.UpdateState();
            Console.WriteLine($"Single Level Parallel For loop (Optimised): {stopWatch.ElapsedMilliseconds}");

            grid.Randomise();
            stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                grid.UpdateState();
            Console.WriteLine($"Nested for loop: {stopWatch.ElapsedMilliseconds}");

            grid.Randomise();
            stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                grid.UpdateState2();
            Console.WriteLine($"Nested Parallel For loop: {stopWatch.ElapsedMilliseconds}");

            grid.Randomise();
            stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                grid.UpdateState3();
            Console.WriteLine($"Single Level Parallel For loop: {stopWatch.ElapsedMilliseconds}");

        }
    }
}
