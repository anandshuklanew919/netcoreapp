using required.Modals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace required.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModal>> GetLanguage();
    }
}