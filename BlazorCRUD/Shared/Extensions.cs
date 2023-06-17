using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BlazorCRUD.Shared.Clases;

namespace BlazorCRUD.Shared
{
    public static class Extensions
    {
        public static float ToDpi(this float centimeter)
        {
            
            var inch = centimeter / 2.54;
            return (float)(inch * 72);
            
        }

    }
}
