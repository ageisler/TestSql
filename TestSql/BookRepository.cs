using Microsoft.Data.Sqlite;
using System.IO;

namespace TestSql
{
    public class BookRepository
    {
        private readonly string connectionString = "Data Source=Library.db;";

        public BookRepository()
        {
            if (!File.Exists("Library.db"))
            {
                File.Create("Library.db").Dispose();
            }
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Books (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT,
                Author TEXT,
                ISBN TEXT,
                Genre TEXT,
                PublicationDate TEXT
            )";
                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        public List<Book> GetBooks()
        {
            var books = new List<Book>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Books";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                ISBN = reader["ISBN"].ToString(),
                                Genre = reader["Genre"].ToString(),
                                PublicationDate = DateTime.Parse(reader["PublicationDate"].ToString())
                            });
                        }
                    }
                }
            }
            return books;
        }

        public void AddBook(Book book)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Books (Title, Author, ISBN, Genre, PublicationDate) VALUES (@Title, @Author, @ISBN, @Genre, @PublicationDate)";
                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@Genre", book.Genre);
                    command.Parameters.AddWithValue("@PublicationDate", book.PublicationDate.ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateBook(Book book)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Books SET Title=@Title, Author=@Author, ISBN=@ISBN, Genre=@Genre, PublicationDate=@PublicationDate WHERE Id=@Id";
                using (var command = new SqliteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@Genre", book.Genre);
                    command.Parameters.AddWithValue("@PublicationDate", book.PublicationDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteBook(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Books WHERE Id=@Id";
                using (var command = new SqliteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}