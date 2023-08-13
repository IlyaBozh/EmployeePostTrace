
using AutoMapper;
using EmployeePostTrace.Api.Controllers;
using EmployeePostTrace.Api.Infrastructure;
using EmployeePostTrace.Api.Models.Requests;
using EmployeePostTrace.Api.Models.Responses;
using EmployeePostTrace.BusinessLayer.Services.Interfaces;
using EmployeePostTrace.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeePostTrace.Api.Tests;

public class LetterControllerTests
{
    private Mock<ILetterService> _letterServiceMock;
    private LetterController _sut;
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        _letterServiceMock = new Mock<ILetterService>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>()));
        _sut = new LetterController(_letterServiceMock.Object, _mapper);
    }

    [Test]
    public async Task AddLetter()
    {
        var expectedId = 1;

        var letter = new AddLetterRequest
        {
            Header = "Увольнение",
            Content = "Вы уволены",
            Recipient = "Рабочий",
            Sender = "Начальник",
            RecipientId = 1,
            SenderId = 2
        };

        _letterServiceMock.Setup(l => l.Add(It.IsAny<LetterDto>()))
            .ReturnsAsync(1);

        var actual = await _sut.Add(letter);

        var actualResult = actual.Result as CreatedResult;
        var actualLetter = actualResult.Value as LetterDto;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.True((int)actualResult.Value == expectedId);

        _letterServiceMock.Verify(l => l.Add(It.Is<LetterDto>(l =>
            l.Header == letter.Header &&
            l.Content == letter.Content &&
            l.Recipient == letter.Recipient &&
            l.Sender == letter.Sender &&
            l.RecipientId == letter.RecipientId &&
            l.SenderId == letter.SenderId
            )), Times.Once);
    }

    [Test]
    public async Task Delete()
    {
        var letter = new LetterDto()
        {
            Header = "Увольнение",
            Content = "Вы уволены",
            Recipient = "Рабочий",
            Sender = "Начальник",
            RecipientId = 1,
            SenderId = 2,
            SendingDate = DateTime.Now,
            IsDeleted = false
        };

        _letterServiceMock.Setup(l => l.GetById(letter.Id))
            .ReturnsAsync(letter);

        var actual = await _sut.Delete(letter.Id);

        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _letterServiceMock.Verify(l => l.Delete(letter.Id, true), Times.Once);
    }


    [Test]
    public async Task GetAllBySenderId()
    {
        var letters = new List<LetterDto>()
        {
                new LetterDto()
                {
                    Id = 1,
                    Header = "Увольнение",
                    Content = "Вы уволены",
                    Recipient = "Рабочий",
                    Sender = "Начальник",
                    RecipientId = 1,
                    SenderId = 2,
                    SendingDate = DateTime.Now,
                    IsDeleted = false
                },

                new LetterDto()
                {
                    Id = 2,
                    Header = "Трудоустройство",
                    Content = "Вы приняты",
                    Recipient = "Безработный",
                    Sender = "Начальник",
                    RecipientId = 3,
                    SenderId = 2,
                    SendingDate = DateTime.Now,
                    IsDeleted = false
                },

                new LetterDto()
                {
                    Id = 3,
                    Header = "Повышение",
                    Content = "Вы повышены",
                    Recipient = "Рабочий",
                    Sender = "Начальник",
                    RecipientId = 4,
                    SenderId = 2,
                    SendingDate = DateTime.Now,
                    IsDeleted = false
                }
        };

        _letterServiceMock.Setup(l => l.GetAllBySenderId(2))
            .ReturnsAsync(letters);

        var actual = await _sut.GetAllBySenderId(2);

        var actualResult = actual.Result as OkObjectResult;
        var actualLeads = actualResult?.Value as List<LetterMainInfoResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(letters.Count, actualLeads.Count);
        Assert.AreEqual(letters[0].Id, actualLeads[0].Id);

        _letterServiceMock.Verify(l => l.GetAllBySenderId(2), Times.Once);
    }

    [Test]
    public async Task GetAllByRecipientId()
    {
        var letters = new List<LetterDto>()
        {
                new LetterDto()
                {
                    Id = 1,
                    Header = "Увольнение",
                    Content = "Вы уволены",
                    Recipient = "Рабочий",
                    Sender = "Начальник",
                    RecipientId = 1,
                    SenderId = 2,
                    SendingDate = DateTime.Now,
                    IsDeleted = false
                },

                new LetterDto()
                {
                    Id = 2,
                    Header = "Трудоустройство",
                    Content = "Вы приняты",
                    Recipient = "Безработный",
                    Sender = "Начальник",
                    RecipientId = 3,
                    SenderId = 2,
                    SendingDate = DateTime.Now,
                    IsDeleted = false
                },

                new LetterDto()
                {
                    Id = 3,
                    Header = "Повышение",
                    Content = "Вы повышены",
                    Recipient = "Рабочий",
                    Sender = "Начальник",
                    RecipientId = 4,
                    SenderId = 2,
                    SendingDate = DateTime.Now,
                    IsDeleted = false
                }
        };

        _letterServiceMock.Setup(l => l.GetAllByRecipientId(1))
            .ReturnsAsync(new List<LetterDto>() { letters[0] });

        var actual = await _sut.GetAllByRecipientId(1);

        var actualResult = actual.Result as OkObjectResult;
        var actualLeads = actualResult?.Value as List<LetterMainInfoResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(1, actualLeads.Count);
        Assert.AreEqual(letters[0].Id, actualLeads[0].Id);

        _letterServiceMock.Verify(l => l.GetAllByRecipientId(1), Times.Once);
    }

    [Test]
    public async Task GetById()
    {
        var letter = new LetterDto()
        {
            Header = "Увольнение",
            Content = "Вы уволены",
            Recipient = "Рабочий",
            Sender = "Начальник",
            RecipientId = 1,
            SenderId = 2,
            SendingDate = DateTime.Now,
            IsDeleted = false
        };

        _letterServiceMock.Setup(l => l.GetById(letter.Id))
            .ReturnsAsync(letter);

        var actual = await _sut.GetById(letter.Id);

        var actualResult = actual.Result as OkObjectResult;
        var actualLetter = actualResult.Value as LetterAllInfoResponse;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(letter.Id, actualLetter.Id);
        Assert.AreEqual(letter.Header, actualLetter.Header);
        Assert.AreEqual(letter.Content, actualLetter.Content);
        Assert.AreEqual(letter.Recipient, actualLetter.Recipient);
        Assert.AreEqual(letter.Sender, actualLetter.Sender);
        Assert.AreEqual(letter.RecipientId, actualLetter.RecipientId);
        Assert.AreEqual(letter.SendingDate, actualLetter.SendingDate);

        _letterServiceMock.Verify(l => l.GetById(letter.Id), Times.Once);
    }

    [Test]
    public async Task UpdateLetter()
    {
        var letter = new LetterDto()
        {
            Header = "Увольнение",
            Content = "Вы уволены",
            Recipient = "Рабочий",
            Sender = "Начальник",
            RecipientId = 1,
            SenderId = 2,
            SendingDate = DateTime.Now,
            IsDeleted = false
        };

        var newLead = new UpdateLetterRequest
        {
            Header = "Не Увольнение",
            Content = "Вы не уволены",
        };

        _letterServiceMock.Setup(l => l.Update(letter, letter.Id));

        var actual = await _sut.UpdateLetter(newLead, letter.Id);

        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _letterServiceMock.Verify(l => l.Update(It.Is<LetterDto>(l =>
        l.Header == newLead.Header &&
        l.Content == newLead.Content
        ), letter.Id), Times.Once);
    }
}
