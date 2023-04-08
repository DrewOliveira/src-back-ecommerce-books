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
                string sql = "SELECT * FROM book where active = 1";

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
    }
}

