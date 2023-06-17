using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorCRUD.Server.Modelos;

public partial class Usuario
{
    public int Id { get; set; }
    [Required(ErrorMessage = "* Obligatorio")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "* Obligatorio")]
    public string Apellido { get; set; } = null!;
    [Required(ErrorMessage = "* Obligatorio")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "* Obligatorio")]
    public string Telefono { get; set; } = null!;
    [Required(ErrorMessage = "* Obligatorio")]
    public DateTime FechaAlta { get; set; }

    public DateTime? FechaBaja { get; set; }
}
