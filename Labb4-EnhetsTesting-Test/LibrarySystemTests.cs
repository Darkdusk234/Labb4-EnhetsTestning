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
}
