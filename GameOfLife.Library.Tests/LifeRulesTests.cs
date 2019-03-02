using System;
using Xunit;

namespace GameOfLife.Library.Tests
{
    public class LifeRulesTests
    {
        //Any live cell with fewer than two live neighbours dies, as if caused by under-population.
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void LiveCell_LessThanTwoLiveNeighbours_Dies(int liveNeighbours)
        {
            CellState currentState = CellState.Alive;

            CellState result = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.Equal(CellState.Dead, result);
        }

        //Any live cell with two or three live neighbours lives on to the next generation.
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void LiveCell_TwoOrThreeLiveNeighbours_Lives(int liveNeighbours)
        {
            CellState currentState = CellState.Alive;

            CellState result = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.Equal(CellState.Alive, result);
        }

        //Any live cell with more than three live neighbours dies, as if by overcrowding.
        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void LiveCell_MoreThanThreeLiveNeighbours_Dies(int liveNeighbours)
        {
            CellState currentState = CellState.Alive;

            CellState result = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.Equal(CellState.Dead, result);
        }

        //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        [Fact]
        public void DeadCell_ExactlyThreeLiveNeighbours_Lives()
        {
            CellState currentState = CellState.Dead;
            int liveNeighbours = 3;

            CellState result = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.Equal(CellState.Alive, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void DeadCell_LessThanThreeLiveNeighbours_Dies(int liveNeighbours)
        {
            CellState currentState = CellState.Dead;

            CellState result = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.Equal(CellState.Dead, result);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void DeadCell_MoreThanThreeLiveNeighbours_Dies(int liveNeighbours)
        {
            CellState currentState = CellState.Dead;

            CellState result = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.Equal(CellState.Dead, result);
        }
    }
}
