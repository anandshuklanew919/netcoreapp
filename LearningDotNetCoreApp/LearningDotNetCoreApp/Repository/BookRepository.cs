using LearningDotNetCoreApp.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDotNetCoreApp.Repository
{
    public class BookRepository
    {
        public List<BookModal> GetBooks()
        {
            return BookRepository.GetBookDataSource();
        }

        public BookModal GetBookById(int id)
        {
            return BookRepository.GetBookDataSource().Where(book => book.Id == id).FirstOrDefault();
        }


        public List<BookModal> SearchBook(string title, string author)
        {
            return BookRepository.GetBookDataSource().Where(book => book.Title.Contains(title,StringComparison.OrdinalIgnoreCase) && book.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
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
    }
}
