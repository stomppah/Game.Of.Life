/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 29/11/13
 * Description: The cell class provides storage to determine the cells state and age (age not implemented yet).
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GOL.Classes
{
    [Serializable()]
    class Cell
    {
        private int m_Age = 0;
        private bool m_Alive = false;

        public bool IsAlive
        {
            get 
            { 
                return m_Alive; 
            }
            set
            {
                m_Alive = value;
            }
        }


    } // class

} //namespace