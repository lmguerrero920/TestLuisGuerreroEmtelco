namespace Client.Data.Models
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

        public int DocumentClient { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public int Phone { get; set; }

        public int? Genre { get; set; }

        public virtual Genre Genre1 { get; set; }
    }
}
