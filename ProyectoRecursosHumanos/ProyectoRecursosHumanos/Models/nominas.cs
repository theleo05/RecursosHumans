//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoRecursosHumanos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class nominas
    {
        public int id { get; set; }
        public string año { get; set; }
        public string mes { get; set; }
        public Nullable<int> monto_total { get; set; }
    
        public virtual empleados empleados { get; set; }
    }
}
