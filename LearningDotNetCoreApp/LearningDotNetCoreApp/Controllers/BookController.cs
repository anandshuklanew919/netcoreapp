using LearningDotNetCoreApp.Modals;
using LearningDotNetCoreApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDotNetCoreApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository bookRepository;
        public BookController()
        {
            bookRepository = new BookRepository();
        }
        public List<BookModal> Index()
        {
            return bookRepository.GetBooks();
        }


        public BookModal GetBooksByid(int id)
        {
            return bookRepository.GetBookById(id);
        }

        public List<BookModal> SearchBooks(string title, string name)
        {
            return bookRepository.SearchBook(title, name);
        }
    }
}
