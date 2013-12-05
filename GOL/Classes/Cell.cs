/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 29/11/13
 * Description: The cell class provides storage to determine
 * the cells state and age (age not implemented yet).
 * URL: https://github.com/stomppah/Conways-Game-in-.NET
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GOL.Classes
{
    [Serializable()]
    class Cell : ISerializable
    {
        private int m_Age = 0;
        private bool m_Alive = false;

        public bool IsAlive { get { return m_Alive; }
            set
            {
                m_Alive = value;
            }
        }

        private void GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            info.AddValue("m_Age", m_Age);
            info.AddValue("m_Alive", m_Alive);
        }

    } // class

} //namespace