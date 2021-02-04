using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dto.Categorie
{
    public class CategorieInsertDTO
    {
        [Required]
        public string CategorieName { get; set; }
    }
}
