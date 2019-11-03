using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slap
{
    class LaneComparer : IComparer<Parcel>
    {
        public int Compare(Parcel x, Parcel y)
        {
            if (x.Lanes == null || y.Lanes == null)
            {
                return 0;
            }

            return x.Lanes.CompareTo(y.Lanes);
        }
    }
}
