﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekt_Teknologji_dotNet.Models
{
    [Table("Car")]
    public class Makinat
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Modeli")]
        public string Modeli { get; set; }
        [Display(Name = "Pershkrimi")]
        public string Pershkrimi { get; set; }
        [Required]
        [Display(Name = "V.Prodhimit")]
        public int Vit_Prodhimi { get; set; }
        [Required]
        [Display(Name = "E disponueshme")]
        public bool ERezervuar { get; set; }
        [Required]
        [Display(Name = "Pagesa/D")]
        [DataType(DataType.Currency)]
        public decimal Kosto1Dite { get; set; }
        
        [Display(Name = "Imazhi ")]
        public string IMG { get; set; }

        public int? TipiID { get; set; }
        public virtual Tipi Tipi { get; set; }
    }
}