using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Library
{
    public class LifeGrid : ICell
    {
        int gridHeight;
        int gridWidth;
        private readonly ICell prototype;
        public CellState[,] CurrentState;
        private CellState[,] nextState;

        public ICell[,] CurrentGrid;
        private ICell[,] nextGrid;

        public LifeGrid(int gridHeight, int gridWidth) : this(gridHeight, gridWidth, new Resident()) { }

        public LifeGrid(int gridHeight, int gridWidth, ICell prototype)
        {
            this.gridHeight = gridHeight;
            this.gridWidth = gridWidth;

            CurrentState = new CellState[gridHeight, gridWidth];
            nextState = new CellState[gridHeight, gridWidth];

            this.prototype = prototype;

            this.CurrentGrid = new ICell[gridHeight, gridWidth];
            this.nextGrid = new ICell[gridHeight, gridWidth];

            Parallel.For(0, gridHeight, row =>
            {
                for (int col = 0; col < gridWidth; col++)
                {
                    CurrentState[row, col] = CellState.Dead;
                    CurrentGrid[row, col] = prototype.Create();
                    nextGrid[row, col] = prototype.Create();
                }
            });
        }

        public ICell Create()
        {
            return new LifeGrid(gridHeight, gridWidth, CurrentGrid[0, 0]);
        }

        #region Legacy Update Methods

        public void UpdateState()
        {
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    var liveNeighbours = GetLiveNeighbours(i, j);
                    nextState[i, j] = LifeRules.GetNewState(CurrentState[i, j], liveNeighbours);
                }
            }

            CurrentState = nextState;
            nextState = new CellState[gridHeight, gridWidth];
        }

        public void UpdateState2()
        {
            Parallel.For(0, gridHeight, i =>
            {
                Parallel.For(0, gridWidth, j => {
                    var liveNeighbours = GetLiveNeighbours(i, j);
                    nextState[i, j] = LifeRules.GetNewState(CurrentState[i, j], liveNeighbours);
                });
            });

            CurrentState = nextState;
            nextState = new CellState[gridHeight, gridWidth];
        }

        public void UpdateState3()
        {
            Parallel.For(0, gridHeight, i =>
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    var liveNeighbours = GetLiveNeighbours(i, j);
                    nextState[i, j] = LifeRules.GetNewState(CurrentState[i, j], liveNeighbours);
                }
            });

            CurrentState = nextState;
            nextState = new CellState[gridHeight, gridWidth];
        }

        public void UpdateState4()
        {
            for (int row = 0; row < gridHeight; row++)
            {
                for (int col = 0; col < gridWidth; col++)
                {
                    var resident = CurrentGrid[row, col] as Resident;
                    if (resident.LiveNeighbors <= 0 && resident.State == CellState.Dead)
                    {
                        nextGrid[row, col] = resident;
                    }
                    else
                    {
                        (nextGrid[row, col] as Resident).State = LifeRules.GetNewState(resident.State, resident.LiveNeighbors);
                    }
                }
            }

            for (int row = 0; row < gridHeight; row++)
            {
                for (int col = 0; col < gridWidth; col++)
                {
                    if ((nextGrid[row, col] as Resident).State == CellState.Alive)
                    {
                        UpdateNeighbours(row, col);
                    }
                }
            }

            CurrentGrid = nextGrid;
            nextGrid = new ICell[gridHeight, gridWidth];
            for (int row = 0; row < gridHeight; row++)
            {
                for (int col = 0; col < gridWidth; col++)
                {
                    nextGrid[row, col] = prototype.Create();
                }
            }
        }
        public void UpdateState5()
        {
            Parallel.For(0, gridHeight, row =>
            {
                Parallel.For(0, gridWidth, col => {
                    var resident = CurrentGrid[row, col] as Resident;
                    if (resident.LiveNeighbors <= 0 && resident.State == CellState.Dead)
                    {
                        nextGrid[row, col] = resident;
                    }
                    else
                    {
                        (nextGrid[row, col] as Resident).State = LifeRules.GetNewState(resident.State, resident.LiveNeighbors);
                    }
                });
            });

            Parallel.For(0, gridHeight, row =>
            {
                Parallel.For(0, gridWidth, col =>
                {
                    if ((nextGrid[row, col] as Resident).State == CellState.Alive)
                    {
                        UpdateNeighbours(row, col);
                    }
                });
            });

            CurrentGrid = nextGrid;
            nextGrid = new ICell[gridHeight, gridWidth];
            Parallel.For(0, gridHeight, row =>
            {
                Parallel.For(0, gridWidth, col =>
                {
                    nextGrid[row, col] = prototype.Create();
                });
            });
        }

        #endregion

        public void UpdateState6()
        {
            Parallel.For(0, gridHeight, row =>
            {
                for (int col = 0; col < gridWidth; col++)
                {
                    var resident = CurrentGrid[row, col] as Resident;
                    if (resident.LiveNeighbors <= 0 && resident.State == CellState.Dead)
                    {
                        nextGrid[row, col] = resident;
                    }
                    else
                    {
                        (nextGrid[row, col] as Resident).State = LifeRules.GetNewState(resident.State, resident.LiveNeighbors);
                    }                    
                }
            });

            Parallel.For(0, gridHeight, row =>
            {
                for (int col = 0; col < gridWidth; col++)
                {
                    if ((nextGrid[row, col] as Resident).State == CellState.Alive)
                    {
                        UpdateNeighbours(row, col);
                    }
                }
            });

            CurrentGrid = nextGrid;
            nextGrid = new ICell[gridHeight, gridWidth];
            Parallel.For(0, gridHeight, row =>
            {
                for (int col = 0; col < gridWidth; col++)
                {
                    nextGrid[row, col] = prototype.Create();
                }
            });
        }

        private void UpdateNeighbours(int positionX, int positionY)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int neighbourX = positionX + i;
                    int neighbourY = positionY + j;

                    if (neighbourX >= 0 && neighbourX < gridHeight && neighbourY >= 0 && neighbourY < gridWidth)
                    {
                        (nextGrid[neighbourX, neighbourY] as Resident).LiveNeighbors = GetNextLiveNeighbours(neighbourX, neighbourY);
                    }
                }
            }
        }

        private int GetNextLiveNeighbours(int positionX, int positionY)
        {
            int liveNeighbours = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int neighbourX = positionX + i;
                    int neighbourY = positionY + j;

                    if (neighbourX >= 0 && neighbourX < gridHeight && neighbourY >= 0 && neighbourY < gridWidth)
                    {
                        if ((nextGrid[neighbourX, neighbourY] as Resident).State == CellState.Alive)
                        {
                            liveNeighbours++;
                        }
                    }
                }
            }

            return liveNeighbours;
        }

        private int GetLiveNeighbours(int positionX, int positionY)
        {
            int liveNeighbours = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int neighbourX = positionX + i;
                    int neighbourY = positionY + j;

                    if (neighbourX >= 0 && neighbourX < gridHeight && neighbourY >= 0 && neighbourY < gridWidth)
                    {
                        if ((CurrentGrid[neighbourX, neighbourY] as Resident).State == CellState.Alive)
                        {
                            liveNeighbours++;
                        }
                    }
                }
            }

            return liveNeighbours;
        }


        public void Randomise()
        {
            Random random = new Random();

            Parallel.For(0, gridHeight, row =>
            {
                for (int col = 0; col < gridWidth; col++)
                {
                    var next = random.Next(6);
                    var newState = next < 5 ? CellState.Dead : CellState.Alive;
                    CurrentState[row, col] = newState;
                    (CurrentGrid[row, col] as Resident).State = newState;
                }
            });

            Parallel.For(0, gridHeight, row =>
            {
                for (int col = 0; col < gridWidth; col++)
                {
                    (CurrentGrid[row, col] as Resident).LiveNeighbors = GetLiveNeighbours(row, col);
                }
            });
        }

    }
}