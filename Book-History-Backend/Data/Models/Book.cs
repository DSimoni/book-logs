﻿namespace Book_History_Backend.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishDate { get; set; }

        public ICollection<Author> Authors { get; set; }


    }
}