/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 16/11/13
 * Time: 22:08
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL.Classes.Base
{
    abstract class CellBase
    {
        private const int m_MaxAge = 255;
        private int m_Age = 0;

        public CellBase()
        {
        }

        public bool IsAlive
        {
            get 
            { return m_Age > 0 ? true : false; 
            }
            set
            {
                if (value) m_Age++; else m_Age = 0;
            }
        }
    }
}