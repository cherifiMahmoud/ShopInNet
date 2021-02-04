using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IRepository
{
    public interface ICategorieRepository
    {
        IEnumerable<Categorie> GetCategories();

        Categorie GetCategorie(int Id);
        Categorie GetCategorie(string categorieName);


        bool CheckCategorieExist(int Id);
        bool CheckCategorieExist(String Name);

        bool CreateCategorie(Categorie categorie);
        bool UpdateCategorie(Categorie categorie);
        bool DeleteCategorie(Categorie categorie);

        bool Save();
    }
}
