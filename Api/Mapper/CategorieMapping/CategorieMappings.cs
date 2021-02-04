using AutoMapper;
using Domain.Models;
using Dto.Categorie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Mapper.CategorieMapping
{
    public class CategorieMappings:Profile
    {
        public CategorieMappings()
        {
            CreateMap<Categorie, CategorieGetDTO>().ReverseMap();
            CreateMap<Categorie, CategorieInsertDTO>().ReverseMap();
            CreateMap<Categorie, CategorieUpdateDTO>().ReverseMap();
        }
    }
}
