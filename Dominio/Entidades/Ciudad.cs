using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal;

[Table("ciudades")]
public partial class Ciudad
{
    public int IdPais { get; set; }

    public int IdDepartamento { get; set; }

    public int IdCiudad { get; set; }

    public string NombreCiudad { get; set; } = null!;

    public string CodigoCiudad { get; set; } = null!;

    public virtual Departamento? Departamento { get; set; } = null!;
}
