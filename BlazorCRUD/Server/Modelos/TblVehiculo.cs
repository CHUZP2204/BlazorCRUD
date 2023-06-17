using System;
using System.Collections.Generic;

namespace BlazorCRUD.Server.Modelos;

public partial class TblVehiculo
{
    public int IdVehiculo { get; set; }

    public string PlacaVehiculo { get; set; } = null!;

    public string ModeloVehiculo { get; set; } = null!;
}
