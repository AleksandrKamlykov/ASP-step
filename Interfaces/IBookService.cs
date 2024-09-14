using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IBookService
    {
        List<Book> GetBooks();
        Book GetBook(Guid id);
        Book?CreateBook(Book book);
        Book UpdateBook(Guid id, Book book);
        bool DeleteBook(Guid id);
    }
}
