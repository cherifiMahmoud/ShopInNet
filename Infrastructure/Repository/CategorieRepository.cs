using System;
using Domain.IRepository;
using System.Collections.Generic;
using System.Text;
using Domain.Models;
using ApplicationLayer.DataBase;
using System.Linq;

namespace Infrastructure.Repository
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly ApplicationDbContext db;

        public CategorieRepository(ApplicationDbContext db)
        {
            this.db = db;
        }



        public bool CheckCategorieExist(int Id)
        {
            return db.categories.Any(C => C.CategorieId == Id);
        }

        public bool CheckCategorieExist(string Name)
        {
            return db.categories.Any(C => C.CategorieName.ToLower() == Name.ToLower());
        }

        public bool CreateCategorie(Categorie categorie)
        {
            db.categories.Add(categorie);
            return Save();
        }

        public bool DeleteCategorie(Categorie categorie)
        {
            db.categories.Remove(categorie);
            return Save();
        }

        public Categorie GetCategorie(int Id)
        {
            return db.categories.Find(Id);
        }

        public Categorie GetCategorie(string categorieName)
        {
            return db.categories.Find(categorieName);
        }

        public IEnumerable<Categorie> GetCategories()
        {
            return db.categories.ToList();
        }

        public bool Save()
        {
            return db.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateCategorie(Categorie categorie)
        {
            db.categories.Update(categorie);
            return Save();
        }
    }
}
