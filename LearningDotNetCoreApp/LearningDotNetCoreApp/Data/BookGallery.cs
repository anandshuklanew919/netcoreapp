﻿namespace required.Data
{
    public class BookGallery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int BookId { get; set; }

        public Books Book { get; set; }
    }
}
