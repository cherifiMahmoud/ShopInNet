﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Categorie
    {
        [Key]
        public int CategorieId { get; set; }
        public string CategorieName { get; set; }
    }
}
