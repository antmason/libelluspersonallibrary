using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Libellus.Domain;
using Libellus.Utilities;

namespace Libellus.DataAccess
{
    class BookDAO : BaseDAO
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        public BookDAO(string db): base(db)
        {

        }
        #endregion

        #region Inserts
        /// <summary>
        /// Utility function for inserting book records
        /// </summary>
        /// <param name="book"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private bool insertBook(Book book, char mode)
        {
            //attempt to insert subject data
            foreach (Subject s in book.Subjects)
            {
                object[] subjectData = { s.Id, s.Name };
                this.ExecuteNonQuery(SQL.Subject.INSERT_NONCUSTOM, subjectData);

                object[] subjectRelation = { book.Id, s.Id };
                this.ExecuteNonQuery(SQL.Subject.INSERT_RELATION, subjectRelation);
            }
            //attempt to insert publisher data
            if (!book.PublisherId.Equals(""))
            {
                object[] publisherData = { book.PublisherId, book.PublisherInfo };
                this.ExecuteNonQuery(SQL.Publisher.INSERT, publisherData);
            }

            //attempt to insert author data
            foreach (Person p in book.Authors)
            {
                object[] authorData = { p.Id, p.FirstName, p.LastName };
                bool success = this.ExecuteNonQuery(SQL.Person.INSERT_PERSON, authorData);

                object[] authorRelation = { book.Id, p.Id };
                this.ExecuteNonQuery(SQL.Person.INSERT_AUTHOR_RELATION, authorRelation);
            }

            //attempt to insert editor data
            foreach (Person p in book.Editors)
            {
                object[] authorData = { p.Id, p.FirstName, p.LastName };
                bool success = this.ExecuteNonQuery(SQL.Person.INSERT_PERSON, authorData);

                object[] editorRelation = { book.Id, p.Id };
                this.ExecuteNonQuery(SQL.Person.INSERT_EDITOR_RELATION, editorRelation);
            }

            //attempt to insert book data


            object[] data = { book.Id, book.ISBN, book.ShortTitle, book.LongTitle, book.PhysicalDescription, book.Edition, book.Dewey, book.DeweyNormalized, book.LibraryOfCongress, book.PublisherId, book.Summary, book.Notes, book.Awards, book.NewPrice, book.UsedPrice, book.PricePaid, DateTime.Now.ToShortDateString(), book.Language, book.Urls };

            if (mode == 'O')
                return this.ExecuteNonQuery(SQL.Book.INSERT_LIBRARY, data);
            else if (mode == 'W')
                return this.ExecuteNonQuery(SQL.Book.INSERT_WISHLIST, data);
            else
                return false;
        }

        /// <summary>
        /// Inserts a book with the type field set to "w" for wish list
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool InsertIntoWishList(Book book)
        {
            return this.insertBook(book, 'W');
        }

        /// <summary>
        /// Inserts a book record to O as type (meaning own)
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool InsertIntoLibrary(Book book)
        {
            return this.insertBook(book, 'O');
        }

        /// <summary>
        /// Inserts a loan record for the book
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="loanee"></param>
        /// <returns></returns>
        public bool LoanBook(string bookId, string loanee)
        {
            object[] p = { bookId, loanee, DateTime.Now.ToString(Constants.DATETIMEFORMAT)};
            return this.ExecuteNonQuery(SQL.Book.INSERT_LOAN_RECORD, p);
        }
        #endregion

        #region Deletes
        /// <summary>
        /// Delete a book, currently this does nothing about the foreign key problems
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteBook(string id)
        {
            object[] data = { id };
            this.ExecuteNonQuery(SQL.Person.DELETE_AUTHOR_RELATION_BY_BOOK_ID, data);
            this.ExecuteNonQuery(SQL.Person.DELETE_EDITOR_RELATION_BY_BOOK_ID, data);
            return this.ExecuteNonQuery(SQL.Book.DELETE_BY_ID, data);
        }
        #endregion

        #region Updates
        /// <summary>
        /// Changes the type field from W to O
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool TransferFromWishListToLibrary(string bookId)
        {
            object[] data = { bookId };
            return this.ExecuteNonQuery(SQL.Book.UPDATE_TRANSFER_TO_LIBRARY, data);
        }

        /// <summary>
        /// Changes the type fields from O to W
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool TransferFromLibraryToWishList(string bookId)
        {
            object[] data = { bookId };
            return this.ExecuteNonQuery(SQL.Book.UPDATE_TRANSFER_TO_WISHLIST, data);
        }

        /// <summary>
        /// Updates the last loan record to reflect today's date as checkin date
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public bool ReturnBook(string bookId)
        {
            object[] p = { bookId, DateTime.Now.ToString(Constants.DATETIMEFORMAT) };
            return this.ExecuteNonQuery(SQL.Book.UPDATE_LOAN_CHECKIN, p);
        }
        
        public bool Updatebook(Book book)
        {
        	//attempt to insert subject data
        	this.ExecuteNonQuery(SQL.Subject.DELETE_BY_ID, new object[] {book.Id});
            foreach (Subject s in book.Subjects)
            {
                object[] subjectData = { s.Id, s.Name };
                this.ExecuteNonQuery(SQL.Subject.INSERT_NONCUSTOM, subjectData);

                object[] subjectRelation = { book.Id, s.Id };
                this.ExecuteNonQuery(SQL.Subject.INSERT_RELATION, subjectRelation);
            }
            
            //attempt to insert publisher data
            if (!book.PublisherId.Equals(""))
            {
                object[] publisherData = { book.PublisherId, book.PublisherInfo };
                this.ExecuteNonQuery(SQL.Publisher.INSERT, publisherData);
            }

            //attempt to insert author data
            this.ExecuteNonQuery(SQL.Person.DELETE_AUTHOR_RELATION_BY_BOOK_ID,new object[] {book.Id});
            foreach (Person p in book.Authors)
            {
                object[] authorData = { p.Id, p.FirstName, p.LastName };
                bool success = this.ExecuteNonQuery(SQL.Person.INSERT_PERSON, authorData);

                object[] authorRelation = { book.Id, p.Id };
                this.ExecuteNonQuery(SQL.Person.INSERT_AUTHOR_RELATION, authorRelation);
            }

            //attempt to insert editor data
            this.ExecuteNonQuery(SQL.Person.DELETE_EDITOR_RELATION_BY_BOOK_ID,new object[] {book.Id});
            foreach (Person p in book.Editors)
            {
                object[] authorData = { p.Id, p.FirstName, p.LastName };
                bool success = this.ExecuteNonQuery(SQL.Person.INSERT_PERSON, authorData);

                object[] editorRelation = { book.Id, p.Id };
                this.ExecuteNonQuery(SQL.Person.INSERT_EDITOR_RELATION, editorRelation);
            }        	
        	
            object[] param = { book.Id, book.ISBN, book.ShortTitle, book.LongTitle, book.PhysicalDescription, book.Edition, book.Dewey, book.DeweyNormalized, book.LibraryOfCongress, book.PublisherId, book.Summary, book.Notes, book.Awards, book.NewPrice, book.UsedPrice, book.PricePaid, book.Language, book.Urls };
        	return this.ExecuteNonQuery(SQL.Book.UPDATE_BOOK, param);
        }
        #endregion

        #region Searches
        /// <summary>
        /// Dynamically builds the query for a search on the book_data view
        /// </summary>
        /// <param name="libMode"></param>
        /// <param name="searchMode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataSet SearchBooks(Constants.LibraryMode libMode, Constants.SearchMode searchMode, string value)
        {
            string baseQuery = "";
            switch (libMode)
            {
                case Constants.LibraryMode.LIBRARY:
                    baseQuery = SQL.Book.SELECT_LIBRARY;
                    break;

                case Constants.LibraryMode.LOANEDBOOKS:
                    baseQuery = SQL.Book.SELECT_LOANED_BOOKS;
                    break;

                case Constants.LibraryMode.WISHLIST:
                    baseQuery = SQL.Book.SELECT_WISHLIST;
                    break;

                case Constants.LibraryMode.LOANHISTORY:
                    baseQuery = SQL.Book.SELECT_LOAN_RECORDS;
                    break;
            }

            switch (searchMode)
            {
                case Constants.SearchMode.NONE:
                    //DO NOTHING
                    break;

                case Constants.SearchMode.AUTHOR_LAST:
                    baseQuery += this.like("last_name", value);
                    break;

                case Constants.SearchMode.AUTHOR_FIRST:
                    baseQuery += this.like("first_name", value);
                    break;

                case Constants.SearchMode.TITLE:
                    baseQuery += this.like("short_title", value);
                    break;

                case Constants.SearchMode.ISBN:
                    baseQuery += this.like("isbn", value);
                    break;

                case Constants.SearchMode.SUBJECT:
                    baseQuery += this.like("subject", value);
                    break;

                case Constants.SearchMode.PUBLISHER:
                    baseQuery += this.like("publisher_info", value);
                    break;

                case Constants.SearchMode.BOOK_ID:
                    baseQuery += this.equal("id", value);
                    break;
            }

            switch (searchMode)
            {
                case Constants.SearchMode.SUBJECT:
                    //no group by clause
                    break;

                case Constants.SearchMode.BOOK_ID:
                    //book_id is only used when getting loan records
                    baseQuery += " GROUP BY loan_id";
                    break;

                default:
                    baseQuery += " GROUP BY id";
                    break;
            }

            return this.ExecuteQuery(baseQuery, null);
        }

        /// <summary>
        /// Returns true if there is a book already matching this id
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool ExistsInLibrary(Book book)
        {
            object[] data = { book.Id };
            DataSet ds = this.ExecuteQuery(SQL.Book.SELECT_SIMPLE_LIBRARY_BY_ID, data);
#if DEBUG
            Console.Out.WriteLine(this.getDataSetInfo(ds));
#endif
            if (ds == null || ds.Tables[0].Rows.Count < 1)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Checks to see if the book is already in the wish list
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool ExistsInWishList(Book book)
        {
            object[] data = { book.Id };
            DataSet ds = this.ExecuteQuery(SQL.Book.SELECT_SIMPLE_WISHLIST_BY_ID, data);
#if DEBUG
            Console.Out.WriteLine(this.getDataSetInfo(ds));
#endif
            if (ds == null || ds.Tables[0].Rows.Count < 1)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Returns true if there is a loan record with an empty checkin date for the book
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public bool IsBookLoaned(string bookId)
        {
            object[] p = { bookId };
            DataSet ds = this.ExecuteQuery(SQL.Book.SELECT_CURRENT_LOAN_BY_ID, p);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Selects
        /// <summary>
        /// Retrieves all books
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllBooks()
        {
            return this.ExecuteQuery(SQL.Book.SELECT_ALL, null);
        }

        /// <summary>
        /// Retrieves only the books that have been loaned
        /// </summary>
        /// <returns></returns>
        public DataSet GetLoanedBooks()
        {
            return this.ExecuteQuery(SQL.Book.SELECT_LOANED_BOOKS, null);
        }

        /// <summary>
        /// Retrieves only the books marked as "W"
        /// </summary>
        /// <returns></returns>
        public DataSet GetWishListBooks()
        {
            return this.ExecuteQuery(SQL.Book.SELECT_WISHLIST, null);
        }

        /// <summary>
        /// Retrieves only the books marked as "O"
        /// </summary>
        /// <returns></returns>
        public DataSet GetLibraryBooks()
        {
            return this.ExecuteQuery(SQL.Book.SELECT_LIBRARY, null);
        }

        /// <summary>
        /// Retrieves a single book record by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book GetBookById(string id)
        {
            object[] data = { id };
            DataSet ds = this.ExecuteQuery(SQL.Book.SELECT_BY_ID_LIBRARY, data);
            Book b = new Book();
            if (this.FillBookFromDataSet(ds, b))
                return b;
            else
                return null;

        }

        #endregion

        #region Private Utility Methods

        /// <summary>
        /// Receives a DataSet, and fills the domain objects Properties from it
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool FillBookFromDataSet(DataSet ds, Book book)
        {
            if (ds == null || ds.Tables[0].Rows.Count < 1)
                return false;
            DataRow dr = ds.Tables[0].Rows[0];

            book.Id = dr["id"].ToString();
            book.ISBN = dr["isbn"].ToString();
            book.Language = dr["language"].ToString();
            book.LibraryOfCongress = dr["lcc_number"].ToString();
            book.LongTitle = dr["long_title"].ToString();
            book.Notes = dr["notes"].ToString();
            book.PhysicalDescription = dr["physical_desc"].ToString();
            book.PublisherId = dr["publisher_id"].ToString();
            book.ShortTitle = dr["short_title"].ToString();
            book.Summary = dr["summary"].ToString();
            book.Urls = dr["urls"].ToString();
            book.NewPrice = dr["new_price"].ToString();
            book.UsedPrice = dr["used_price"].ToString();
            book.PricePaid = dr["price_paid"].ToString();

            //get book_subject data
            DataSet subjectDS = this.ExecuteQuery(SQL.Subject.SELECT_BY_BOOK_ID, new object[] { book.Id });
            if (subjectDS != null)
                foreach (DataRow dr1 in subjectDS.Tables[0].Rows)
                {
                    Subject s = new Subject();
                    s.Id = dr1["id"].ToString();
                    s.Name = dr1["subject"].ToString();
                    book.Subjects.Add(s);
                }

            //get publisher data
            DataSet publisherDS = this.ExecuteQuery(SQL.Publisher.SELECT_PUBLISHER_BY_ID, new object[] { book.PublisherId });
            if (publisherDS != null)
                foreach (DataRow dr2 in publisherDS.Tables[0].Rows)
                {
                    book.PublisherInfo = dr2["publisher_info"].ToString();
                }

            //get author data
            DataSet authorDS = this.ExecuteQuery(SQL.Person.SELECT_AUTHORS_BY_BOOK_ID, new object[] { book.Id });
            if (authorDS != null)
                foreach (DataRow dr3 in authorDS.Tables[0].Rows)
                {
                    Person p = new Person();
                    p.Id = dr3["id"].ToString();
                    p.FirstName = dr3["first_name"].ToString();
                    p.LastName = dr3["last_name"].ToString();
                    book.Authors.Add(p);
                }

            //get editor data
            DataSet editorDS = this.ExecuteQuery(SQL.Person.SELECT_EDITORS_BY_BOOK_ID, new object[] { book.Id });
            if (editorDS != null)
                foreach (DataRow dr4 in editorDS.Tables[0].Rows)
                {
                    Person p = new Person();
                    p.Id = dr4["id"].ToString();
                    p.FirstName = dr4["first_name"].ToString();
                    p.LastName = dr4["last_name"].ToString();
                    book.Editors.Add(p);
                }
            return true;
        }

        /// <summary>
        /// Just simply adds the formatted like clause to the end of a query
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="like"></param>
        /// <returns></returns>
        private string like(string fieldName, string like)
        {
            return " AND " + fieldName + " LIKE '%" + like + "%'";
        }

        /// <summary>
        /// Used to add a formatted = clause to the end of a query
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="eq"></param>
        /// <returns></returns>
        private string equal(string fieldName, string eq)
        {
            return " AND " + fieldName + "='" + eq + "'";
        }
        #endregion

    }
}
