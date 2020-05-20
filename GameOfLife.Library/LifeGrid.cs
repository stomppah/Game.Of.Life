using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Library
{
    [Serializable()]
    public class LifeGrid : ICell, ISerializable
    {
        int gridHeight;
        int gridWidth;
        private readonly ICell prototype;
        public CellState[,] CurrentState;
        private CellState[,] nextState;

        public ICell[,] CurrentGrid;
        private ICell[,] nextGrid;

        public LifeGrid(int gridHeight, int gridWidth) : this(gridHeight, gridWidth, CellFactory.GetCell()) { }

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

        public void UpdateState()
        {
            UpdateCellState();
            UpdateCellNeighbours();
            UpdateGrids();
        }

        private void UpdateCellState()
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
        }

        private void UpdateCellNeighbours()
        {
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
        }

        private void UpdateGrids()
        {
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

        public void AddAliveCell(int x, int y, int cellPixelSize)
        {
            int row = y / cellPixelSize;
            int col = x / cellPixelSize;

            (CurrentGrid[row, col] as Resident).State = CellState.Alive;
            (CurrentGrid[row, col] as Resident).LiveNeighbors = GetLiveNeighbours(row, col);
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("gridHeight", gridHeight);
            info.AddValue("gridWidth", gridWidth);
            info.AddValue("CurrentGrid", CurrentGrid);
            info.AddValue("nextGrid", nextGrid);
        }

        public LifeGrid(SerializationInfo info, StreamingContext ctxt)
        {
            this.gridHeight = info.GetInt32("gridHeight");
            this.gridWidth = info.GetInt32("gridWidth");

            CurrentState = new CellState[gridHeight, gridWidth];
            nextState = new CellState[gridHeight, gridWidth];

            this.prototype = CellFactory.GetCell();

            this.CurrentGrid = new ICell[gridHeight, gridWidth];
            this.nextGrid = new ICell[gridHeight, gridWidth];

            this.CurrentGrid = (ICell[,])info.GetValue("CurrentGrid", typeof(ICell[,]));
            this.nextGrid = (ICell[,])info.GetValue("nextGrid", typeof(ICell[,]));
        }
    }
}