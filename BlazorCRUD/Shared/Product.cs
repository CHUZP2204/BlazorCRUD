﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCRUD.Shared
{
    public partial class Product
    {
        public int IdProducto { get; set; }

        public string NombreProd { get; set; } = null!;

        public double PrecioProd { get; set; }

        public int CantidadProd { get; set; }

        public double PrecioXprod { get; set; }
    }
}
