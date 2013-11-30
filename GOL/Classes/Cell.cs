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
        private const int m_MaxAge = 255;
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
                m_Alive = value; ;
            }
        }

        public int Age { get { return m_Age; } set { value = m_Age; } }
    }
}