/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 29/11/13
 * Time: 18:03
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

        public bool IsAlive
        {
            get 
            { 
                return m_Age > 0; 
            }
            set
            {
                if (value)
                    m_Age++;
                else
                    m_Age = 0;
            }
        }


    } // class

} //namespace