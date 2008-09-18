using System;
using System.Collections.Generic;
using System.Text;

namespace Libellus.DataAccess
{
    class SQL
    {
        public struct BaseInfo
        {
            public static string INSERT = "INSERT INTO owner_info (owner_name,date_created,last_accessed,db_alias,password,id) values ({0},{1},{2},{3},{4},'1')";
            public static string SELECT = "SELECT * from owner_info";
            public static string UPDATE_DTE = "UPDATE owner_info SET last_accessed = {0} WHERE id = '1'";
        }

        /// <summary>
        /// NOTE TO FREAKIN SELF: 
        /// My base classes take care of adding the ' ' around SQL parameters, don't put it in yourself!  
        /// This makes the whole app go to hell in a hand basket!
        /// </summary>
        public struct Book
        {
            public static string INSERT_LIBRARY = "INSERT INTO book (id,isbn,short_title,long_title,physical_desc,edition_info,dewey,dewey_norm,lcc_number,publisher_id,summary,notes,awards,new_price,used_price,price_paid,date_added,language,urls,type) values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},'O')";
            public static string INSERT_WISHLIST = "INSERT INTO book (id,isbn,short_title,long_title,physical_desc,edition_info,dewey,dewey_norm,lcc_number,publisher_id,summary,notes,awards,new_price,used_price,price_paid,date_added,language,urls,type) values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},'W')";
            public static string SELECT_LOANED_BOOKS = "SELECT * FROM loaned_book_data WHERE type='O'";
            public static string SELECT_LOAN_RECORDS = "SELECT * FROM loaned_book_records WHERE type='O'";
            public static string SELECT_LIBRARY = "SELECT * FROM book_data WHERE type='O'";
            public static string SELECT_WISHLIST = "SELECT * FROM wish_list_data WHERE type='W'";
            public static string SELECT_ALL = "SELECT * FROM book_data";
            public static string SELECT_BY_ID_LIBRARY = "SELECT * FROM book_data WHERE id = {0} AND type = 'O' GROUP BY id";
            public static string SELECT_BY_ID_WISHLIST = "SELECT * FROM book_data WHERE id = {0} AND type = 'W' GROUP BY id";
            public static string SELECT_SIMPLE_LIBRARY_BY_ID = "SELECT id FROM book WHERE id = {0} AND type = 'O' GROUP BY id";
            public static string SELECT_SIMPLE_WISHLIST_BY_ID = "SELECT id FROM book WHERE id = {0} AND type = 'W' GROUP BY id";
            public static string DELETE_BY_ID = "DELETE FROM book WHERE id = {0}";
            public static string INSERT_LOAN_RECORD = "INSERT INTO loan_book (book_id,loan_to,date_loaned,date_checkedin) VALUES ({0},{1},{2},'')";
            public static string DELETE_LOAN_RECORD = "DELETE FROM loan_book WHERE book_id = {0}";
            public static string SELECT_LOAN_BY_BOOK_ID = "SELECT * FROM loan_book WHERE book_id = {0}";
            public static string SELECT_CURRENT_LOAN_BY_ID = "SELECT * FROM loan_book WHERE book_id = {0} AND date_checkedin = ''";
            public static string UPDATE_LOAN_CHECKIN = "UPDATE loan_book set date_checkedin = {1} WHERE book_id = {0}";
            public static string UPDATE_TRANSFER_TO_WISHLIST = "UPDATE book SET type = 'W' WHERE id = {0}";
            public static string UPDATE_TRANSFER_TO_LIBRARY = "UPDATE book SET type = 'O' WHERE id = {0}";
            public static string UPDATE_BOOK = "UPDATE BOOK SET isbn={1},short_title={2},long_title={3},physical_desc={4},edition_info={5},dewey={6},dewey_norm={7},lcc_number={8},publisher_id={9},summary={10},notes={11},awards={12},new_price={13},used_price={14},price_paid={15},language={16},urls={17} WHERE ID = {0}";
        }

        public struct DBInfo
        {
            public static string SELECT = "SELECT * from db_info WHERE id = '1'";
        }

        public struct Person
        {
            public static string INSERT_PERSON = "INSERT INTO person (id,first_name,last_name) values ({0},{1},{2})";
            public static string INSERT_AUTHOR_RELATION = "INSERT INTO book_author(book_id,person_id) values ({0},{1})";
            public static string INSERT_EDITOR_RELATION = "INSERT INTO book_editor(book_id,person_id) values ({0},{1})";
            
            public static string SELECT_BY_ID = "SELECT * FROM person WHERE id = {0}";
            public static string SELECT_ALL = "SELECT * FROM person";

            public static string SELECT_AUTHORS_BY_BOOK_ID = "SELECT p.id AS id, p.first_name AS first_name, p.last_name AS last_name FROM book_author ba INNER JOIN person p ON ba.person_id = p.id AND ba.book_id = {0}";
            public static string SELECT_EDITORS_BY_BOOK_ID = "SELECT p.id AS id, p.first_name AS first_name, p.last_name AS last_name FROM book_editor be INNER JOIN person p ON be.person_id = p.id AND be.book_id = {0}";

            public static string DELETE_PERON_BY_ID = "DELETE FROM person WHERE id = {0};";
            public static string DELETE_AUTHOR_RELATION_BY_BOOK_ID = "DELETE FROM book_author WHERE book_id = {0}";
            public static string DELETE_EDITOR_RELATION_BY_BOOK_ID = "DELETE FROM book_editor WHERE book_id = {0}";
        }

        public struct Publisher
        {
            public static string INSERT = "INSERT INTO publisher (id,publisher_info) values ({0},{1})";
            public static string SELECT_ALL = "SELECT * FROM publisher";
            public static string SELECT_PUBLISHER_BY_ID = "SELECT * FROM publisher p WHERE p.id = {0}";
            public static string DELETE_BY_ID = "DELETE FROM publisher WHERE id = {0}";
        }

        public struct Subject
        {
            public static string INSERT_CUSTOM = "INSERT INTO subject (id,subject,type) values ({0},{1},'C')";
            public static string INSERT_NONCUSTOM = "INSERT INTO subject (id,subject,type) values ({0},{1},'N')";
            public static string INSERT_RELATION = "INSERT INTO book_subject (book_id,subject_id) values ({0},{1})";
            public static string SELECT_ALL = "SELECT * FROM subject";
            public static string SELECT_BY_ID = "SELECT * FROM subject WHERE id = {0}";
            public static string DELETE_BY_ID = "DELETE FROM subject WHERE id = {0}";
            public static string SELECT_BY_BOOK_ID = "SELECT s.subject as subject, s.id as id FROM book_subject bs INNER JOIN subject s ON bs.subject_id = s.id AND bs.book_id = {0}";
        }
    }
}
