using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqliteApp.Standart.Entites
{
    [System.ComponentModel.DataAnnotations.Schema.Table("MapObject")]
    public class MapObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.MaxLength(1000)]

        public string Description { get; set; }

        
    }
}
