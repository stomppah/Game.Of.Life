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
    struct Cell : ISerializable
    {
        public int m_Age;
        public bool m_Alive;

        public bool IsAlive { get { return m_Alive; }
            set
            {
                if (value == false)
                { m_Age = 0; }
                else { m_Age++; }
                m_Alive = value;
            }
        }

        public Cell(SerializationInfo info, StreamingContext ctxt)
        {
            m_Age = (int)info.GetValue("m_Age", typeof(int));
            m_Alive = (bool)info.GetValue("m_Alive", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            info.AddValue("m_Age", m_Age);
            info.AddValue("m_Alive", m_Alive);
        }

    } // class

} //namespace