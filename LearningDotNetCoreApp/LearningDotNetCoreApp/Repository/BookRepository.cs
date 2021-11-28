using LearningDotNetCoreApp.Data;
using LearningDotNetCoreApp.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDotNetCoreApp.Repository
{
    public class BookRepository
    {

        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext bookStoreContext)
        {
            _context = bookStoreContext;
        }
        public async Task<List<BookModal>> GetBooks()
        {
            var books = new List<BookModal>();
            var allbooks = await _context.books.ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModal()
                    {
                        Id = book.Id,
                        Author = book.Author,
                        Title = book.Title,
                        Description = book.Description,
                        TotalPages = book.TotalPages,
                        Category = book.Category,
                        Language = book.Language

                    });
                }
            }
            return books;
        }

        public async Task<BookModal> GetBookById(int id)
        {
            var data = await _context.books.FindAsync(id);
            var book = new BookModal()
            {
                Id = data.Id,
                Author = data.Author,
                Title = data.Title,
                Description = data.Description,
                TotalPages = data.TotalPages,
                Category = data.Category,
                Language = data.Language
            };
            return book;
        }


        public List<BookModal> SearchBook(string title, string author)
        {
            return BookRepository.GetBookDataSource().Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase) && book.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private static List<BookModal> GetBookDataSource()
        {
            return new List<BookModal>()
            {
                new BookModal(){Id=1, Title ="MVC" , Author ="Anand Shukla" ,  Description = "This book is for MVC",Category="Programming", Language="English" , TotalPages=1002},
                new BookModal(){ Id=2 , Title = "Dot Net Core" , Author="Ankit Shukla",  Description = "This book is for Dot Net Core",Category="Programming", Language="English" , TotalPages=500},
                new BookModal(){ Id=3 , Title = "Azure Fundamental" , Author="Satya",Description = "This book is for Azure Fundamental",Category="Cloud Tech", Language="English" , TotalPages=600},
                new BookModal(){ Id=4 , Title = "Angular" , Author="Mansi",Description = "This book is for Angular",Category="Programming", Language="English" , TotalPages=650}
            };
        }

        public async Task<int> AddNewBook(BookModal bookModal)
        {
            var book = new Books()
            {
                Author = bookModal.Author,
                CreatedOn = DateTime.UtcNow,
                Description = bookModal.Description,
                Title = bookModal.Title,
                TotalPages = bookModal.TotalPages.HasValue  ? bookModal.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow
            };
            await _context.books.AddAsync(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }
    }
}
