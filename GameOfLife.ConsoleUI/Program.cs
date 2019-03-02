using GameOfLife.Library;
using System;
using System.Text;

namespace GameOfLife.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new LifeGrid(25, 65);
            grid.Randomise();

            ShowGrid(grid.CurrentState);

            while (Console.ReadLine() != "q")
            {
                grid.UpdateState();
                ShowGrid(grid.CurrentState);
            }
        }

        private static void ShowGrid(CellState[,] currentState)
        {
            Console.Clear();
            int x = 0;
            int rowLength = currentState.GetUpperBound(1) + 1;

            var output = new StringBuilder();

            foreach (var state in currentState)
            {
                output.Append(state == CellState.Alive ? "0" : ".");
                x++;
                if (x >= rowLength)
                {
                    x = 0;
                    output.AppendLine();
                }
            }
            Console.Write(output.ToString());

        }
    }
}
