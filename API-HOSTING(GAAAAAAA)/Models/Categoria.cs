using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_HOSTING_GAAAAAAA_.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
