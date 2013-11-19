/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 16/11/13
 * Time: 17:57
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL
{
    static class World
    {
        private static int _Width     = 960;
        private static int _Height    = 530;

        private static int _globalX, _globalY;

        public static int Width { get { return _Width; } }
        public static int Height { get { return _Height; } }

        public static int XPos { get { return _globalX; } set { _globalX = value; } }
        public static int YPos { get { return _globalY; } set { _globalY = value; } }
    }
}
