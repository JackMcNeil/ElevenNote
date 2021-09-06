using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category() { UserId = _userId,  Name = model.Name, Descirption = model.Description };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Categories
                    .Where(e => e.UserId == _userId)
                    .Select(e => new CategoryListItem { CategoryId = e.CategoryId, Name = e.Name, Descirption = e.Descirption });
                return query.ToArray();
            }
        }

        public CategoryDetail GetCategoryById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Categories
                    .Single(e => e.CategoryId == id && e.UserId == _userId);
                return new CategoryDetail
                {
                    CategoryId = entity.CategoryId,
                    Name = entity.Name,
                    Description = entity.Descirption,
                };
            }
        }

        public bool UpdateCategory(CategoryEdit edit)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories.Single(e => e.CategoryId == edit.CategoryId && e.UserId == _userId);

                entity.Name = edit.Name;
                entity.Descirption = edit.Description;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories.Single(e => e.CategoryId == categoryId && e.UserId == _userId);

                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
