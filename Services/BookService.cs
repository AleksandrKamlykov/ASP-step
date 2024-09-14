using System.Text.Json;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class BookService : IBookService
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Services", "books.json");
        public BookService()
        {

            // string _filePath = Path.Combine(Directory.GetCurrentDirectory(), path);

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }
        public void SaveBooks(List<Book> books)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(books, options);
            File.WriteAllText(_filePath, json);
        }
        public Book? CreateBook(Book book)
        {
            try
            {
                File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(book));
                return book;
            }catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while writing the file: {ex.Message}");
                return null;
            }
        }

        public bool DeleteBook(Guid id)
        {
          var book = GetBook(id);
            if(book == null)
            {
                return false;
            }
            var books = GetBooks();
            var booksForSave = books.Where(b => b.Id.ToString() != id.ToString()).ToList();
            SaveBooks(booksForSave);
            return true;
        }

        public async Task<Book> GetBook(Guid id)
        {
            var books = GetBooks();

            return books.FirstOrDefault(b => b.Id == id);
        }

        public List<Book> GetBooks()
        {
            try
            {
                if (!File.Exists(_filePath) || new FileInfo(_filePath).Length == 0)
                {
                    return new List<Book>();
                }

                var json = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Book>();
                }

                return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework or simply write to console)
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                return new List<Book>();
            }

        }

        public Book UpdateBook(Guid id, Book book)
        {
            var books = GetBooks();
            var bookToUpdate = books.FirstOrDefault(b => b.Id == id);
            if(bookToUpdate == null)
            {
                return null;
            }
            bookToUpdate.Title = book.Title;
            bookToUpdate.Author = book.Author;
            bookToUpdate.ISBN = book.ISBN;
            SaveBooks(books);
            return bookToUpdate;
        
        }

        Book? IBookService.GetBook(Guid id)
        {
           var books = GetBooks();
            return books.FirstOrDefault(b => b.Id == id);
        }
    }
}
