using MKTFY.App.Repositories;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Seeds
{
    public static class CategorySeeder
    {
        public static async Task AddDefaultCategories(ICategoryRepository repo)
        {
            var categoryCarsExists = await repo.FindCategoryByName("Cars & Vehicles");
            if (categoryCarsExists == null)
            {
                await repo.AddCategory("Cars & Vehicles");
            }

            var categoryFurnitureExists = await repo.FindCategoryByName("Furniture");
            if (categoryFurnitureExists == null)
            {
                await repo.AddCategory("Furniture");
            }

            var categoryElectronicsExists = await repo.FindCategoryByName("Electronics");
            if (categoryElectronicsExists == null)
            {
                await repo.AddCategory("Electronics");
            }

            var categoryREExists = await repo.FindCategoryByName("Real Estate");
            if (categoryREExists == null)
            {
                await repo.AddCategory("Real Estate");
            }
        }
    }
}
