using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Library
{
    public static class CellFactory
    {
        private static HashSet<ICell> hashSet = new HashSet<ICell>();

        public static ICell GetCell()
        {
            ICell c = null;

            if(hashSet.Count != 0)
            {
                ICell[] arr = new ICell[1];
                hashSet.CopyTo(arr);
                return arr[0];
            }
            var newres = new Resident();
            hashSet.Add(newres);
            return newres;
        }
    }
}
