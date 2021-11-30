using LearningDotNetCoreApp.Data;
using LearningDotNetCoreApp.Modals;
using LearningDotNetCoreApp.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDotNetCoreApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(BookRepository bookRepository, LanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this._bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetBooks();
            return View(data);
        }

        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
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


        public async Task<ActionResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.BookId = bookId;

            var language = new SelectList(await _languageRepository.GetLanguage(), "Id", "Name");
            ViewBag.Language = language;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewBook(BookModal bookModal)
        {
            if (ModelState.IsValid)
            {
                if (bookModal.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModal.CoverImageUrl = await UploadImage(folder, bookModal.CoverPhoto);

                }


                if (bookModal.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModal.BookPdfUrl = await UploadImage(folder, bookModal.BookPdf);

                }

                if (bookModal.GalleryFiles != null)
                {
                    bookModal.Gallery = new List<GalleryModel>();
                    string folder = "books/gallery/";
                    foreach (var file in bookModal.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.Name,
                            URL = await UploadImage(folder, file)
                        };

                        bookModal.Gallery.Add(gallery);
                    }
                }


                int id = await _bookRepository.AddNewBook(bookModal);

                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            var language = new SelectList(await _languageRepository.GetLanguage(), "Id", "Name");
            ViewBag.Language = language;
            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }
    }
}
