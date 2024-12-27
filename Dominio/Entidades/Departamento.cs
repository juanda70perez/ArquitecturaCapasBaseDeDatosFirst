using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal;

[Table("departamentos")]
public partial class Departamento
{
    public int IdPais { get; set; }

    public int IdDepartamento { get; set; }

    public string NombreDepartamento { get; set; } = null!;

    public string CodigoDepartamento { get; set; } = null!;

    public virtual ICollection<Ciudad> Ciudades { get; set; } = new List<Ciudad>();

    public virtual Pais? IdPaisNavigation { get; set; }
}
