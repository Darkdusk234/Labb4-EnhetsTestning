using Labb4_EnhetsTestning;
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

    //Make a method to test if ISBN number has letters in it
    [TestMethod]
    public void AddBook_AddBookWithLettersInISBNNumber_False()
    {
        var library = new LibrarySystem();
        var book = new Book("Wolfbrothers", "Maria Flint", "9780451dad524935", 2001);

        var actual = library.AddBook(book);

        Assert.IsFalse(actual);
    }
}
