
using EmployeePostTrace.BusinessLayer.Services;
using EmployeePostTrace.BusinessLayer.Services.Interfaces;
using EmployeePostTrace.BusinessLayer.Tests.TestModels;
using EmployeePostTrace.DataLayer.Models;
using EmployeePostTrace.DataLayer.Repositories.Interfaces;
using Moq;

namespace EmployeePostTrace.BusinessLayer.Tests;

public class LetterServiceTests
{
    private ILetterService _sut;
    private Mock<ILetterRepository> _letterRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _letterRepositoryMock = new Mock<ILetterRepository>();
        _sut = new LetterService(_letterRepositoryMock.Object);
    }

    [Test]
    public async Task Add()
    {
        var letter = LetterDataForTests.GetLetter();

        _letterRepositoryMock.Setup(e => e.Add(It.Is<LetterDto>(p => p.Id == letter.Id))).ReturnsAsync(letter.Id);

        var expectedId = letter.Id;

        var actualId = await _sut.Add(letter);

        Assert.That(expectedId, Is.EqualTo(actualId));

        _letterRepositoryMock.Verify(c => c.Add(It.IsAny<LetterDto>()), Times.Once);
    }

    [Test]

    public async Task GetAllBySenderId()
    {
        var letter = LetterDataForTests.GetLetter();

        _letterRepositoryMock.Setup(e => e.GetAllBySenderId(letter.SenderId)).ReturnsAsync(new List<LetterDto> { letter });

        var expectedEmployees = new List<LetterDto>() { letter };

        var actualEmployees = await _sut.GetAllBySenderId(letter.SenderId);

        Assert.That(actualEmployees, Is.EquivalentTo(expectedEmployees));
        Assert.AreEqual(expectedEmployees.Count, actualEmployees.Count);

        _letterRepositoryMock.Verify(c => c.GetAllBySenderId(letter.SenderId), Times.Once);
    }

    [Test]

    public async Task GetAllByRecipientId()
    {
        var letter = LetterDataForTests.GetLetter();

        _letterRepositoryMock.Setup(e => e.GetAllByRecipientId(letter.SenderId)).ReturnsAsync(new List<LetterDto> { letter });

        var expectedEmployees = new List<LetterDto>() { letter };

        var actualEmployees = await _sut.GetAllByRecipientId(letter.SenderId);

        Assert.That(actualEmployees, Is.EquivalentTo(expectedEmployees));
        Assert.AreEqual(expectedEmployees.Count, actualEmployees.Count);

        _letterRepositoryMock.Verify(c => c.GetAllByRecipientId(letter.SenderId), Times.Once);
    }

    [Test]
    public async Task GetById()
    {
        var letter = LetterDataForTests.GetLetter();

        _letterRepositoryMock.Setup(l => l.GetById(letter.Id)).ReturnsAsync(letter);
        var expected = letter;

        var actual = await _sut.GetById(letter.Id);

        Assert.AreEqual(expected, actual);
        _letterRepositoryMock.Verify(l => l.GetById(letter.Id), Times.Once);
    }

    [Test]
    public async Task Update()
    {
        var letter = LetterDataForTests.GetLetter();

        _letterRepositoryMock.Setup(l => l.GetById(letter.Id)).ReturnsAsync(letter);
        _letterRepositoryMock.Setup(l => l.Update(letter));

        await _sut.Update(letter, 0);

        _letterRepositoryMock.Verify(l => l.GetById(It.Is<int>(i => i == 0)), Times.Once);
    }
}
