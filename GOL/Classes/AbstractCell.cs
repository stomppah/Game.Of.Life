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

namespace GOL.Classes
{
    abstract class AbstractCell
    {
        private static const int maxAge = 255;
        private int age;

        public AbstractCell()
        {
        }
        public int Age
        {

            get
            {
                return age < maxAge
                    ? age++
                    : maxAge; // stays same colour
            }

            set { age = maxAge; }
        }

        public bool IsAlive
        {
            get { return age > 0 ? true : false; }
            set { if (age != 0) age++; }
        }
    }
}