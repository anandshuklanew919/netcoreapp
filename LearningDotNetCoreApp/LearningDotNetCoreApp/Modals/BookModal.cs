using LearningDotNetCoreApp.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDotNetCoreApp.Modals
{
    public class BookModal
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Please enter valid title")]
        [Required(ErrorMessage = "Please enter title of the book")]
        //[MyCustomeValidation("Azure")]
        public string Title {  get; set; }
        [Required(ErrorMessage = "Please enter author of the book")] 
        public string Author { get; set; }

        [StringLength(500, MinimumLength = 3, ErrorMessage = "Please enter valid description")]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage ="Please select language of the book")]
        public int LanguageId { get; set; }

        [Required(ErrorMessage = "Please enter total no. of pages of the book")]
        public int? TotalPages { get; set; }

        public string Language { get; set; }

        [Display(Name ="Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }

        public string CoverImageUrl { get; set; }

        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; } 

        [Required]
        [Display(Name = "Upload your book in pdf format")]
        public IFormFile BookPdf { get; set; }

        public string BookPdfUrl { get; set; }
    }
}
