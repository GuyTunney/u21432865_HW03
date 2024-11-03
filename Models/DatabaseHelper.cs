using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;
using u21432865_HW03.Models;
using u21432865_HW03.Data;

namespace u21432865_HW03.Helpers
{
    public class DatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public async Task<List<Student>> GetPaginatedStudentsAsync(int pageNumber, int pageSize)
        {
            var students = new List<Student>();

            using (var conn = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(@"
                    SELECT studentId, name, surname, birthdate, gender, class 
                    FROM students 
                    ORDER BY studentId 
                    OFFSET @Offset ROWS 
                    FETCH NEXT @PageSize ROWS ONLY;", conn);
                command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                await conn.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var student = new Student
                        {
                            StudentId = (int)reader["studentId"],
                            Name = reader["name"].ToString(),
                            Surname = reader["surname"].ToString(),
                            Birthdate = reader["birthdate"] != DBNull.Value ? DateTime.Parse(reader["birthdate"].ToString()) : (DateTime?)null,
                            Gender = reader["gender"].ToString(),
                            Class = reader["class"].ToString()
                        };
                        students.Add(student);
                    }
                }
            }

            return students;
        }

        public async Task<int> GetTotalStudentCountAsync()
        {
            int count = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT COUNT(*) FROM students", conn);
                await conn.OpenAsync();
                count = (int)await command.ExecuteScalarAsync();
            }
            return count;
        }

        public async Task<List<Book>> GetPaginatedBooksAsync(int pageNumber, int pageSize)
        {
            var books = new List<Book>();

            using (var conn = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(@"
                    SELECT b.bookId, b.name AS book_name, b.pagecount, 
                           a.authorId, a.name AS author_name, a.surname AS author_surname, 
                           t.typeId, t.name AS type_name
                    FROM books b
                    JOIN authors a ON b.authorId = a.authorId
                    JOIN types t ON b.typeId = t.typeId
                    ORDER BY b.bookId
                    OFFSET @Offset ROWS 
                    FETCH NEXT @PageSize ROWS ONLY;", conn);

                command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                await conn.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var book = new Book
                        {
                            BookId = (int)reader["bookId"],
                            BookName = reader["book_name"].ToString(),
                            PageCount = (int)reader["pagecount"],
                            Author = new Author
                            {
                                AuthorId = (int)reader["authorId"],
                                Name = reader["author_name"].ToString(),
                                Surname = reader["author_surname"].ToString()
                            },
                            Type = new BookType
                            {
                                TypeId = (int)reader["typeId"],
                                Name = reader["type_name"].ToString()
                            }
                        };
                        books.Add(book);
                    }
                }
            }

            return books;
        }

        public async Task<int> GetTotalBookCountAsync()
        {
            int count = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT COUNT(*) FROM books", conn);
                await conn.OpenAsync();
                count = (int)await command.ExecuteScalarAsync();
            }
            return count;
        }
        public async Task AddStudentAsync(Student student)
        {
            using (var dbContext = new LibraryDbContext())
            {
                dbContext.Students.Add(student);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddBookAsync(Book book)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(@"
                    INSERT INTO books (name, pagecount, authorId, typeId) 
                    VALUES (@Name, @PageCount, @AuthorId, @TypeId);", conn);
                command.Parameters.AddWithValue("@Name", book.BookName);
                command.Parameters.AddWithValue("@PageCount", book.PageCount);
                command.Parameters.AddWithValue("@AuthorId", book.Author.AuthorId);
                command.Parameters.AddWithValue("@TypeId", book.Type.TypeId);

                await conn.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
