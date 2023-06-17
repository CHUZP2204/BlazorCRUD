using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorCRUD.Shared;

public partial class TblVehiculo
{
    
    public int IdVehiculo { get; set; }
    [Required(ErrorMessage = "Requerido")]
    public string PlacaVehiculo { get; set; } = null!;
    [Required(ErrorMessage = "Requerido")]
    public string ModeloVehiculo { get; set; } = null!;
}
