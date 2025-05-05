using Labb4_EnhetsTestning;
using Newtonsoft.Json;
namespace Labb4_EnhetsTesting_Test;

[TestClass]
public class LibrarySystemTests
{
    [TestMethod]
    public void AddBook_AddBook_True()
    {
        var library = new LibrarySystem();
        var book = new Book("Wolfbrothers", "Maria Flint", "978045152534565", 2001);

        var actual = library.AddBook(book);

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void AddBook_AddBookWithoutISBNNumber_False()
    {
        var library = new LibrarySystem();
        var book = new Book("Wolfbrothers", "Maria Flint", null, 2001);

        var actual = library.AddBook(book);

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void AddBook_AddBookWithISBNAlreadyInSystem_False()
    {
        var library = new LibrarySystem();
        var book = new Book("Wolfbrothers", "Maria Flint", "9780451524935", 2001);

        var actual = library.AddBook(book);

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void AddBook_AddBookWithLettersInISBNNumber_False()
    {
        var library = new LibrarySystem();
        var book = new Book("Wolfbrothers", "Maria Flint", "9780451dad524935", 2001);

        var actual = library.AddBook(book);

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void RemoveBook_RemoveBookFromSystem_True()
    {
        var library = new LibrarySystem();

        var actual = library.RemoveBook("9780451524935");

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void RemoveBook_RemoveBookWithNonExistingISBN_False()
    {
        var library = new LibrarySystem();

        var actual = library.RemoveBook("dajjgajfahgkj");

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void RemoveBook_RemoveBorrowedBook_False()
    {
        var library = new LibrarySystem();
        library.BorrowBook("9780451524935");

        var actual = library.RemoveBook("9780451524935");

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void SearchByISBN_SearchForAnExistingISBNNumber_BookWithThatISBNNumber()
    {
        var library = new LibrarySystem();
        var expectedBook = new Book("1984", "George Orwell", "9780451524935", 1949);

        var actual = library.SearchByISBN("9780451524935");

        Assert.IsTrue(JsonConvert.SerializeObject(expectedBook) == JsonConvert.SerializeObject(actual));
    }
}
