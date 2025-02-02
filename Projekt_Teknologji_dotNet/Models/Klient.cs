﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekt_Teknologji_dotNet.Models
{
    [Table("Client")]
    public class Klient
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Klienti")]
        public string Username { get; set; }

        [Display(Name = "Numer telefoni")]
        public string PhoneNumber { get; set; }
    }
}