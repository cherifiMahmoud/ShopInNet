using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dto.Categorie
{
    public class CategorieUpdateDTO
    {
        [Required]
        public int CategorieId { get; set; }
        [Required]
        public string CategorieName { get; set; }
    }
}
