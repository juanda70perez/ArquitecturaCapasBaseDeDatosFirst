using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal;

[Table("paises")]
public partial class Pais
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string CodigoPais { get; set; } = null!;

    public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();
}
