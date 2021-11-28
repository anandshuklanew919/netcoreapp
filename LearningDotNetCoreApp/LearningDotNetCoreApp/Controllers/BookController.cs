using LearningDotNetCoreApp.Data;
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
        private readonly BookRepository _bookRepository;
        public BookController(BookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetBooks();
            return View(data);
        }

        public async Task<ViewResult> GetBook(int id)
        {
            var data =  await _bookRepository.GetBookById(id);
            return View(data);
        }


        public async Task<ViewResult> GetBooksByid(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModal> SearchBooks(string title, string name)
        {
            return _bookRepository.SearchBook(title, name);
        }


        public ActionResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewBook(BookModal bookModal)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModal);

                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }
           

            return View();
        }
    }
}
