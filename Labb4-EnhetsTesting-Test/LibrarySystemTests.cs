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

    [TestMethod]
    public void SearchByISBN_SearchWithAnNonExistantISBNNumber_Null()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByISBN("941758197598175");

        Assert.IsNull(actual);
    }

    [TestMethod]
    public void SearchByISBN_SearchWithEmptyString_Null()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByISBN("");

        Assert.IsNull(actual);
    }

    [TestMethod]
    public void SearchByISBN_SearchWithNonNumbersInISBNNumber_Null()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByISBN("90481095718DSAdd");

        Assert.IsNull(actual);
    }

    [TestMethod]
    public void SearchByISBN_SearchWithOnlyPartOfAnISBN_FirstBookInSystemWithThatPartOFISBN()
    {
        var library = new LibrarySystem();
        var expectedBook = new Book("1984", "George Orwell", "9780451524935", 1949);

        var actual = library.SearchByISBN("97804");

        Assert.IsTrue(JsonConvert.SerializeObject(expectedBook) == JsonConvert.SerializeObject(actual));
    }

    [TestMethod]
    public void SearchByAuthor_SearchWithExistingAuthor_ListOfBooksFromAuthor()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByAuthor("George Orwell");

        Assert.IsTrue(actual.Count != 0);
    }

    [TestMethod]
    public void SearchByAuthor_SearchWithNonExistingAuthor_EmptyList()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByAuthor("Maria Carry");

        Assert.IsTrue(actual.Count == 0);
    }

    [TestMethod]
    public void SearchByAuthor_SearchWithEmptyString_Null()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByAuthor("");

        Assert.IsNull(actual);
    }

    [TestMethod]
    public void SearchByAuthor_SearchWithExistingAuthorWithBigLetters_ListOfBooksFromAuthor()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByAuthor("GEORGE ORWELL");

        Assert.IsTrue(actual.Count != 0);
    }

    [TestMethod]
    public void SearchByAuthor_SearchWithOnlyFirstNameOfAuthor_ListOfBooksFromAuthorsWithThatName()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByAuthor("George");

        Assert.IsTrue(actual.Count != 0);
    }

    [TestMethod]
    public void SearchByTitle_SearchWithExistingTitle_ListOfBooksWithThatTitle()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByTitle("1984");

        Assert.IsTrue(actual.Count != 0);
    }

    [TestMethod]
    public void SearchByTitle_SearchWithNonExistingTitle_EmptyList()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByTitle("dagajogj");

        Assert.IsTrue(actual.Count == 0);
    }

    [TestMethod]
    public void SearchByTitle_SearchWithEmptyString_Null()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByTitle("");

        Assert.IsNull(actual);
    }

    [TestMethod]
    public void SearchByTitle_SearchWithExistingTitleWithOnlyCapitalLetters_ListOfBooksWithThatTitle()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByTitle("TO KILL A MOCKINGBIRD");

        Assert.IsTrue(actual.Count != 0);
    }

    [TestMethod]
    public void SearchByTitle_SearchWithPartOfAnExistingTitle_ListOfBooksWhoseTitlesContainThatPart()
    {
        var library = new LibrarySystem();

        var actual = library.SearchByTitle("The");

        Assert.IsTrue(actual.Count != 0);
    }

    [TestMethod]
    public void BorrowBook_ExistingISBNNumberInputted_True()
    {
        var library = new LibrarySystem();
        var isbn = "9780451524935";

        var actual = library.BorrowBook(isbn);

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void BorrowBook_NonExistingISBNNumberInputted_False()
    {
        var library = new LibrarySystem();

        var actual = library.BorrowBook("7418659813675987");

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void BorrowBook_ExistingISBNNumberInputtedCheckIfBookIsMarkedAsBorrowed_True()
    {
        var library = new LibrarySystem();

        library.BorrowBook("9780451524935");
        var actual = library.SearchByISBN("9780451524935");

        Assert.IsTrue(actual.IsBorrowed);
    }

    [TestMethod]
    public void BorrowBook_BorrowingAAlreadyBorrowedBook_False()
    {
        var library = new LibrarySystem();

        library.BorrowBook("9780451524935");
        var actual = library.BorrowBook("9780451524935");

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void BorrowBook_ExistingISBNNumberInputtedCheckIfCorrectBorrowDateIsNoted_CorrectBorrowDate()
    {
        var library = new LibrarySystem();
        var time = DateTime.Now;

        library.BorrowBook("9780451524935");
        var actual = library.SearchByISBN("9780451524935");

        Assert.AreEqual(time.Date, actual.BorrowDate.Value.Date);
    }

    [TestMethod]
    public void ReturnBook_ExistingISBNNumberOfABorrowedBookInputted_True()
    {
        var library = new LibrarySystem();

        library.BorrowBook("9780451524935");
        var actual = library.ReturnBook("9780451524935");

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void ReturnBook_NonExistingISBnNumberInputted_False()
    {
        var library = new LibrarySystem();

        library.BorrowBook("978051573567164657318");
        var actual = library.ReturnBook("978051573567164657318");

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void ReturnBook_ExistingISBNNumberOfABorrowedBookInputtedCheckIfReturnedBooksBorrowDateIsReset_Null()
    {
        var library = new LibrarySystem();
        string isbn = "9780451524935";

        var actualBorrow = library.BorrowBook(isbn);
        library.ReturnBook(isbn);
        var actual = library.SearchByISBN(isbn);

        Assert.IsTrue(actualBorrow, "Book was not borrowed, check if valid ISBN number was used in test.");
        Assert.IsNull(actual.BorrowDate, "Book's borrow date was not reset.");
    }

    [TestMethod]
    public void ReturnBook_ExistingISBNNumberOfANonBorrowedBookInputted_False()
    {
        var library = new LibrarySystem();

        var actual = library.ReturnBook("9780451524935");

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void IsBookOverdue_InputtingALoanedBookThatIsOverdue_True()
    {
        var library = new LibrarySystem();
        library.BorrowBook("9780451524935");

        var actual = library.IsBookOverdue("9780451524935", -1);

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void IsBookOverdue_InputtingALoanedBookThatIsNotOverdue_False()
    {
        var library = new LibrarySystem();
        library.BorrowBook("9780451524935");

        var actual = library.IsBookOverdue("9780451524935", 10);

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void IsBookOverdue_InputtingANotLoanedBook_False()
    {
        var library = new LibrarySystem();
        library.BorrowBook("9780451524935");

        var actual = library.IsBookOverdue("9780451524935", -1);

        Assert.IsFalse(actual);
    }
}
