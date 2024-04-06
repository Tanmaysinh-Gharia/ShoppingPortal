using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Interfaces
{
    public interface ICategoryRepository
    {
        //Task<Category> GetByIdAsync(Guid id);
        //Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        //Task DeleteAsync(Guid id);
    }
}
