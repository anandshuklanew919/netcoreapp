﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace required.Data
{
    public class Books
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int LanguageId { get; set; }
        public int TotalPages { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public string BookPdfUrl { get; set; }
        public string CoverImageUrl { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public ICollection<BookGallery> bookGallery { get; set; }
    }
}
