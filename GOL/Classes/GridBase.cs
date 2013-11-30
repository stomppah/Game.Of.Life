/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 29/11/13
 * Time: 23:38
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL.Classes
{
    abstract class GridBase
    {
        private static CellBase[,] m_Grid;

        private const int m_Width = 960;
        private const int m_Height = 530;

        private int m_Rows  = m_Width / 5;
        private int m_Cols  = m_Height / 5;

        public GridBase()
        {
            m_Grid = new Cell[m_Rows, m_Cols];
        }

        public void setCellAt(int xCoord, int yCoord)
        {
            m_Grid[xCoord, yCoord].IsAlive = true;
        }

        public void getCellAt(int xCoord, int yCoord)
        {
            m_Grid[xCoord, yCoord].IsAlive = false;
        }

    }
}
