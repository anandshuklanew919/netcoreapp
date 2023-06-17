using required.Modals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace required.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModal bookModal);
        Task<BookModal> GetBookById(int id);
        Task<List<BookModal>> GetBooks();
        Task<List<BookModal>> GetTopBooksAsync(int count);
        List<BookModal> SearchBook(string title, string author);
        string GetBookAppName();
    }
}