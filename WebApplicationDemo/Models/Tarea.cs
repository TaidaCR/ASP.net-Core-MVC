using System.ComponentModel.DataAnnotations;

namespace WebApplicationDemo.Models
{
    public class Tarea
    {

        public int Id { get; set; }

        /*REQUIRED: indica que el campo es obligatorio, afecta a lo de abajo*/
        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name ="Tarea")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 carácteres")]

        public string Nombre { get; set; }

        [StringLength(100, ErrorMessage = "La descripción no puede tener más de 100 carácteres")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es requerida")]
        [Display(Name = "Fecha de vencimiento")]
        [DataType(DataType.Date)]

        public DateTime FechaVencimiento { get; set; }

        [Display(Name = "¿Está completa?")]

        public bool EstaCompleta { get; set; }

        [StringLength(500, ErrorMessage = "La explicacion no puede tener más de 500 carácteres")]
        [Display(Name = "Explicación")]
        public string Explicacion { get; set; }
    }
}
