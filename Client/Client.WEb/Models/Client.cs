namespace Client.WEb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [Key]
        public int IdClient { get; set; }

        [StringLength(11)]
        [Display(Name = "Numero Documento")]
        public string DocumentClient { get; set; }

        [Required]
        [StringLength(100)] 
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "no se permiten numeros")]
        [Display(Name = "Nombres Completos")]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Email Address")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Telefono ")]
        [StringLength(10)]
        public string Phone { get; set; }

        [Display(Name = "Genero")]
        public int? Genre { get; set; }

        public virtual Genre Genre1 { get; set; }
    }
}
