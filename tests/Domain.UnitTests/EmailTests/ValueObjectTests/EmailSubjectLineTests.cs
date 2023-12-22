namespace Domain.UnitTests.EmailTests.ValueObjectTests;

[TestFixture]
public class EmailSubjectLineTests
{
    [Test]
    public void Create_Valid_SubjectLine_ShouldSucceed()
    {
        // Arrange
        string validSubject = "Important Update";

        // Act
        var subjectLine = EmailSubjectLine.Create(validSubject);

        // Assert
        Assert.That(subjectLine, Is.Not.Null);
        Assert.That(subjectLine.ToString(), Is.EqualTo(validSubject));
    }

    [TestCase(null)]
    [TestCase("")]
    public void Create_EmptyOrNullOrWhiteSpace_SubjectLine_ShouldThrow_EmptyValueException(string emptySubject)
    {
        // Act & Assert
        Assert.Throws<EmptyValueException>(() => EmailSubjectLine.Create(emptySubject));
    }

    [Test]
    public void Create_SubjectLine_ExceedsMaxLength_ShouldThrow_SubjectLineTooLongException()
    {
        // Arrange
        var longSubject = new string('X', 101);

        // Act & Assert
        Assert.Throws<SubjectLineTooLongException>(() => EmailSubjectLine.Create(longSubject));
    }

    [Test]
    public void Create_SubjectLine_WithNewlinesOrCarriageReturns_ShouldThrow_InvalidSubjectException()
    {
        // Arrange
        string subjectWithNewlines = "Subject with\nnewline";
        string subjectWithCarriageReturn = "Subject with\rreturn";
        string subjectWithBoth = "Subject with\nnewline\rreturn";

        // Act & Assert
        Assert.Throws<InvalidSubjectException>(() => EmailSubjectLine.Create(subjectWithNewlines));
        Assert.Throws<InvalidSubjectException>(() => EmailSubjectLine.Create(subjectWithCarriageReturn));
        Assert.Throws<InvalidSubjectException>(() => EmailSubjectLine.Create(subjectWithBoth));
    }
}