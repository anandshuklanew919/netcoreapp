using LearningDotNetCoreApp.Data;
using LearningDotNetCoreApp.Modals;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDotNetCoreApp.Repository
{
    public class LanguageRepository
    {
        private readonly BookStoreContext _context;


        public LanguageRepository(BookStoreContext bookStoreContext)
        {
            _context = bookStoreContext;
        }

        public async Task<List<LanguageModal>> GetLanguage()
        {
            var language = await _context.Languages.Select(x => new LanguageModal() { 
              Id= x.Id,
              Description= x.Description,
              Name= x.Name
            }).ToListAsync();

            return language;
        }
    }
}
