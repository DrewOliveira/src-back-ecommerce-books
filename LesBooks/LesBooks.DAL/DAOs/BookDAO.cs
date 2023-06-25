using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace LesBooks.DAL
{
    public class BookDAO : Connection, IBookDAO
    {
        IAuthorDAO _authorDAO;
        IPublisherDAO _publisherDAO;
        IPricingDAO _pricingDAO;
        IStockDAO _stockDAO;
        IActivationStatusReasonDAO _activationStatusReasonDAO;
        IBookCategoryDAO _bookCategoryDAO;

        public BookDAO(IAuthorDAO authorDAO, IPublisherDAO publisherDAO, IPricingDAO pricingDAO, IActivationStatusReasonDAO activationStatusReasonDAO, IStockDAO stockDAO, IBookCategoryDAO bookCategoryDAO)
        {
            _authorDAO = authorDAO;
            _publisherDAO = publisherDAO;
            _pricingDAO = pricingDAO;
            _stockDAO = stockDAO;
            _activationStatusReasonDAO = activationStatusReasonDAO;
            _bookCategoryDAO = bookCategoryDAO;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            try
            {
                string sql = "SELECT * FROM book";

                OpenConnection();
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dimension dimensionData = new Dimension
                    {
                        height = Convert.ToDouble(reader["height"]),
                        width = Convert.ToDouble(reader["width"]),
                        weight = Convert.ToDouble(reader["weight"]),
                        depth = Convert.ToDouble(reader["depth"])
                    };

                    Book book = new Book();

                    book.id = (int)reader["id"];
                    book.title = (string)reader["title"];
                    book.publicationYear = Convert.ToDateTime(reader["publicationYear"]);
                    book.edition = (string)reader["edition"];
                    book.ISBN = (string)reader["ISBN"];
                    book.pageCount = (int)reader["pageCount"];
                    book.synopsis = (string)reader["synopsis"];
                    book.active = Convert.ToBoolean(reader["active"]);
                    book.barcode = (string)reader["barcode"];
                    book.dimension = dimensionData;
                    book.value = Convert.ToDouble(reader["value"]);
                    book.categories = _bookCategoryDAO.GetAllCategoryByBookId(Convert.ToInt32((int)reader["id"]));
                    book.author = _authorDAO.GetAuthorById(Convert.ToInt32(reader["author_id"]));
                    book.pricing = _pricingDAO.GetPricingById(Convert.ToInt32(reader["pricing_id"]));
                    book.publisher = _publisherDAO.GetPublisherById(Convert.ToInt32(reader["publisher_id"]));
                    book.stock = _stockDAO.GetStockByBookId(Convert.ToInt32((int)reader["id"]));
                    book.activationStatusReason = _activationStatusReasonDAO.GetActivationStatusReasonById(Convert.ToInt32(reader["activation_status_reason_id"]));

                    books.Add(book);
                }

                reader.Close();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return books;
        }

        public Book GetBookById(int id)
        {
            Book book = null;

            try
            {
                string sql = "SELECT * FROM book where id = @id_book";

                OpenConnection();

                cmd.Parameters.AddWithValue("@id_book", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Dimension dimensionData = new Dimension
                    {
                        height = Convert.ToDouble(reader["height"]),
                        width = Convert.ToDouble(reader["width"]),
                        weight = Convert.ToDouble(reader["weight"]),
                        depth = Convert.ToDouble(reader["depth"])
                    };

                    book = new Book();

                    book.id = (int)reader["id"];
                    book.title = (string)reader["title"];
                    book.publicationYear = Convert.ToDateTime(reader["publicationYear"]);
                    book.edition = (string)reader["edition"];
                    book.ISBN = (string)reader["ISBN"];
                    book.pageCount = (int)reader["pageCount"];
                    book.synopsis = (string)reader["synopsis"];
                    book.active = Convert.ToBoolean(reader["active"]);
                    book.barcode = (string)reader["barcode"];
                    book.dimension = dimensionData;
                    book.value = Convert.ToDouble(reader["value"]);
                    book.categories = _bookCategoryDAO.GetAllCategoryByBookId(Convert.ToInt32((int)reader["id"]));
                    book.author = _authorDAO.GetAuthorById(Convert.ToInt32(reader["author_id"]));
                    book.pricing = _pricingDAO.GetPricingById(Convert.ToInt32(reader["pricing_id"]));
                    book.publisher = _publisherDAO.GetPublisherById(Convert.ToInt32(reader["publisher_id"]));
                    book.stock = _stockDAO.GetStockByBookId(Convert.ToInt32((int)reader["id"]));
                    book.activationStatusReason = _activationStatusReasonDAO.GetActivationStatusReasonById(Convert.ToInt32(reader["activation_status_reason_id"]));
                }

                reader.Close();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return book;
        }

        public Book ChangeBook(Book book)
        {
            try
            {
                string sql = "UPDATE book SET title = @title, publicationYear = @publicationYear, edition = @edition, pageCount = @pageCount, synopsis = @synopsis, active = @active, barcode = @barcode, height = @height, width= @width, weight = @weight, depth = @depth, value = @value, author_id = @author_id, publisher_id = @publisher_id, pricing_id = @pricing_id, activation_status_reason_id = @activation_status_reason_id where id = @book_id;";
                OpenConnection();
                cmd.Parameters.AddWithValue("@book_id", book.id);
                cmd.Parameters.AddWithValue("@title", book.title);
                cmd.Parameters.AddWithValue("@publicationYear", book.publicationYear);
                cmd.Parameters.AddWithValue("@edition", book.edition);
                cmd.Parameters.AddWithValue("@pageCount", book.pageCount);
                cmd.Parameters.AddWithValue("@synopsis", book.synopsis);
                cmd.Parameters.AddWithValue("@active", book.active);
                cmd.Parameters.AddWithValue("@barcode", book.barcode);
                cmd.Parameters.AddWithValue("@height", book.dimension.height);
                cmd.Parameters.AddWithValue("@width", book.dimension.width);
                cmd.Parameters.AddWithValue("@weight", book.dimension.weight);
                cmd.Parameters.AddWithValue("@depth", book.dimension.depth);
                cmd.Parameters.AddWithValue("@value", book.value);
                cmd.Parameters.AddWithValue("@author_id", book.author.id);
                cmd.Parameters.AddWithValue("@publisher_id", book.publisher.id);
                cmd.Parameters.AddWithValue("@pricing_id", book.pricing.id);
                cmd.Parameters.AddWithValue("@activation_status_reason_id", book.activationStatusReason.id);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return book;
        }
    }
}

