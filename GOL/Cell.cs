/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 16/11/13
 * Time: 18:03
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace GOL
{
    class Cell
    {
        private static int _size = 5;
        private static int _colour = 255;

        public static int Size { get { return _size; } }
        public static int Colour { get { return _colour; } set { _colour = value; } }
    }
}