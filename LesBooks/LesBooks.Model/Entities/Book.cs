using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Book
    {
        public Book()
        {

        }

        public Book(int id, Author author, List<Category> categories, Publisher publisher, Stock stock, 
            Dimension dimension, Pricing pricing, ActivationStatusReason activationStatusReason,
            String title, DateTime publicationYear, String edition, String ISBN, int pageCount,
            String synopsis, Boolean active, String barcode, Double value)
        {
            this.id = id;
            this.author = author;
            this.categories = categories;
            this.publisher = publisher;
            this.stock = stock;
            this.dimension = dimension;
            this.pricing = pricing;
            this.activationStatusReason = activationStatusReason;
            this.title = title;
            this.publicationYear = publicationYear;
            this.edition = edition;
            this.ISBN = ISBN;
            this.pageCount = pageCount;
            this.synopsis = synopsis;
            this.active = active;
            this.barcode = barcode;
            this.value = value;
        }


        public int id { get; set; }
        public Author author {  get; set; }   

        public List<Category> categories { get; set; }

        public Publisher publisher { get; set; }

        public Stock stock { get; set; }

        public Dimension dimension { get; set; }

        public Pricing pricing { get; set; }

        public ActivationStatusReason activationStatusReason { get; set; }

        public String title { get; set; }

        public DateTime publicationYear { get; set; }

        public String edition { get; set; }

        public String ISBN { get; set; }

        public int pageCount { get; set; }

        public String synopsis { get; set; }

        public Boolean active { get; set; }

        public String barcode { get; set; }

        public Double value { get; set; }
    }
}
