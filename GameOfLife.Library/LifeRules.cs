using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Library
{
    public class LifeRules
    {
        public static CellState GetNewState(CellState currentState, int liveNeighbours)
        {
            switch (currentState)
            {
                case CellState.Alive:
                    if (liveNeighbours < 2 || liveNeighbours > 3)
                        return CellState.Dead;
                    break;

                case CellState.Dead:
                    if (liveNeighbours == 3)
                        return CellState.Alive;
                    break;

                default:
                    break;
            }

            return currentState;
        }
    }
}
