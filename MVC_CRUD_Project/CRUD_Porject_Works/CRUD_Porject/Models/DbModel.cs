using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUD_Porject.Models
{
	public enum Gender { Male = 1, Female }
	public class Author
    {
		public int AuthorID { get; set; }
        [Required,StringLength(30),Display(Name ="Author Name")]
		public string AuthorName { get; set; }
		[Required, StringLength(50), Display(Name = "Author Address")]
		public string AuthorAddress { get; set; }
		[EnumDataType(typeof(Gender))]
		public Gender Gender { get; set; }
		public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

	}

	public class Genre
	{
		public int GenreID { get; set; }
        [Required,StringLength(30),Display(Name ="Genre Name")]
		public string GenreName { get; set; }
		public virtual ICollection<Book> Books { get; set; } = new List<Book>();
	}

	public class Publisher
    {
		public int PublisherID { get; set; }
        [Required,StringLength(30),Display(Name ="Publisher Name")]
		public string PublisherName { get; set; }
		public virtual ICollection<Book> Books { get; set; } = new List<Book>();
	}

	public class Book
	{
		public int BookID { get; set; }
		[Required, StringLength(30), Display(Name = "Book Name")]
		public string BookName { get; set; }
		[Required, DisplayFormat(DataFormatString = "{0:0.00}")]
		public decimal Price { get; set; }
		[Required, Column(TypeName = "date"),
			Display(Name = "Publish Date"),
			DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
			ApplyFormatInEditMode = true)]
		public DateTime PublishDate { get; set; }
		[Required, StringLength(150)]
		public string Picture { get; set; }
		public bool Available { get; set; }
		[ForeignKey("Genre")]
		public int GenreID { get; set; }
		[ForeignKey("Publisher")]
		public int PublisherID { get; set; }
		public virtual Genre Genre { get; set; }
		public virtual Publisher Publisher { get; set; }
		public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
		public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
	}
	public class BookAuthor
    {
        [Key,Column(Order=0),ForeignKey("Author")]		
		public int AuthorID { get; set; }
		[Key, Column(Order=1), ForeignKey("Book")]
		public int BookID { get; set; }
		public virtual Author Author { get; set; }
		public virtual Book Book { get; set; }
		
    }
	public class Customer
    {
		public int CustomerID { get; set; }
        [Required,StringLength(30),Display(Name ="Customer Name")]
		public string CustomersName { get; set; }
		public virtual ICollection<SaleDetail> Sales { get; set; } = new List<SaleDetail>();
	}
	//public class Sale
 //   {
	//	public int SaleID { get; set; }
	//	[ForeignKey("Customer")]
	//	public int CustomersID { get; set; }
		
	//	public virtual Customer Customer { get; set; }
	//	public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
	//}

	public class SaleDetail
    {
		[Key, Column(Order = 0), ForeignKey("Customer")]
		public int CustomerID { get; set; }
		[Key, Column(Order = 1), ForeignKey("Book")]
		public int BookID { get; set; }
        [Required]
		public int Quantity { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual Book Book { get; set; }

	}
	public class BookSellerDbContext : DbContext
    {
		public BookSellerDbContext()  
		{
			Database.SetInitializer(new BookSellerDbInitializer());
		}
		public DbSet<Author> Authors { get; set; }
		public DbSet<Genre> Genres{ get; set; }
		public DbSet<Publisher> Publishers { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<BookAuthor> BookAuthors { get; set; }
		public DbSet<Customer> Customers { get; set; }
		
		public DbSet<SaleDetail> SaleDetails { get; set; }
	}
	public class BookSellerDbInitializer : DropCreateDatabaseIfModelChanges<BookSellerDbContext>
    {
        protected override void Seed(BookSellerDbContext db)
        {
			Author a = new Author{ AuthorName = "Maruf", AuthorAddress = "Gazipur",Gender=Gender.Male };
			Genre g = new Genre { GenreName = "Genre01", };
			Publisher p = new Publisher { PublisherName = "Publisher01" };
			Book b = new Book { BookName = "Book01", Price = 200.00M, PublishDate = new DateTime(1998, 10, 10), Available = true, Publisher = p , Picture = "1.jpg" };
			g.Books.Add(b);
			Customer c = new Customer { CustomersName = "C1" };
			
			b.SaleDetails.Add(new SaleDetail { Book = b, Customer= c, Quantity = 1 });
			BookAuthor ba = new BookAuthor { Author = a, Book = b };

			db.Authors.Add(a);
			db.Genres.Add(g);
			db.Publishers.Add(p);
			db.Customers.Add(c);
			
			db.BookAuthors.Add(ba);
			db.SaveChanges();
        }
    }

}