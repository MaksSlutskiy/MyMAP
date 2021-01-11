using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms;

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
        [System.ComponentModel.DataAnnotations.MaxLength(128)]
        public string Label { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.MaxLength(1000)]

        public string Description { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.MaxLength(128)]
        public string Icon { get; set; }
        [Required]
        public double latitude { get; set; }
        [Required]
        public double longitude { get; set; }
        [Required]
        public bool IsVisible { get; set; }
        //[Required]
        //public IEnumerable<Image> Images { get; set; }
        //[Required]
        //public Category Category { get; set; }
        //[Required]
        //public bool IsVisible { get; set; }
    }

    public enum Category
    {
        BeautifulViews,
        Restaurants,
        Bars,
        Cinemas,
        FitnessClubs
    };
}
