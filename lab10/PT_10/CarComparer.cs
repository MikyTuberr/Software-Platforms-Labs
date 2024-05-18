﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_10
{
    internal class CarComparer : IComparer<Car>
    {
        public int Compare(Car x, Car y)
        {
            if (x == null || y == null)
                return 0;

            return x.Motor.HorsePower.CompareTo(y.Motor.HorsePower);
        }
    }
}
