
using AutoMapper;
using EmployeePostTrace.Api.Controllers;
using EmployeePostTrace.Api.Infrastructure;
using EmployeePostTrace.Api.Models.Requests;
using EmployeePostTrace.Api.Models.Responses;
using EmployeePostTrace.BusinessLayer.Services.Interfaces;
using EmployeePostTrace.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data;
using System.Security.Claims;

namespace EmployeePostTrace.Api.Tests;

public class EmployeeControllerTests
{
    private Mock<IEmployeeService> _employeeServiceMock;
    private EmployeeController _sut;
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        _employeeServiceMock = new Mock<IEmployeeService>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>()));
        _sut = new EmployeeController(_employeeServiceMock.Object, _mapper);
    }

    [Test]
    public async Task Register()
    {
        var expectedId = 1;

        var lead = new RegistrationEmployeeRequest
        {
            FirstName = "Nathalie",
            LastName = "Bahadur",
            Patronymic = "Efrat",
            Email = "Nied1961@gmail.com",
            Password = "a$i9QSVc6B"
        };

        _employeeServiceMock.Setup(l => l.Add(It.IsAny<EmployeeDto>()))
            .ReturnsAsync(1);

        var actual = await _sut.Register(lead);

        var actualResult = actual.Result as CreatedResult;
        var actualLead = actualResult.Value as EmployeeDto;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.True((int)actualResult.Value == expectedId);

        _employeeServiceMock.Verify(l => l.Add(It.Is<EmployeeDto>(l =>
            l.FirstName == lead.FirstName &&
            l.LastName == lead.LastName &&
            l.Patronymic == lead.Patronymic &&
            l.Email == lead.Email &&
            l.Password == lead.Password &&
            !l.IsDeleted
            )), Times.Once);
    }

    [Test]
    public async Task Delete()
    {
        var lead = new EmployeeDto
        {
            Id = 6,
            FirstName = "Asmaa",
            LastName = "Pavlina",
            Patronymic = "Marlena",
            Email = "Insch9@gmail.com",
            Password = "na9}UeC5nY",
            IsDeleted = false
        };

        _employeeServiceMock.Setup(l => l.GetById(lead.Id))
            .ReturnsAsync(lead);

        var actual = await _sut.Remove(lead.Id);

        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _employeeServiceMock.Verify(l => l.Delete(lead.Id, true), Times.Once);
    }


    [Test]
    public async Task GetAll()
    {
        var leads = new List<EmployeeDto>()
        {
                new EmployeeDto()
                {
                    Id = 1,
                    FirstName = "Shyam",
                    LastName = "Olander",
                    Patronymic = "Beverley",
                    Email = "Reate1940@gmail.com",
                    Password = "BWL|xfzZT}",
                    IsDeleted = false
                },

                new EmployeeDto()
                {
                    Id = 2,
                    FirstName = "Nikostratos",
                    LastName = "Erikas",
                    Patronymic = "Bernadette",
                    Email = "Staing85@gmail.com",
                    Password = "*tLsR@ZE%s",
                    IsDeleted = false
                },

                new EmployeeDto()
                {
                    Id = 3,
                    FirstName = "Koch",
                    LastName = "Harisha",
                    Patronymic = "Vera",
                    Email = "Kied1958@gmail.com",
                    Password = "*tLsR@ZE%s",
                    IsDeleted = true
                }
        };

        _employeeServiceMock.Setup(l => l.GetAll())
            .ReturnsAsync(leads);

        var actual = await _sut.GetAll();

        var actualResult = actual.Result as OkObjectResult;
        var actualLeads = actualResult?.Value as List<EmployeeMainInfoResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(leads.Count, actualLeads.Count);
        Assert.AreEqual(leads[0].Id, actualLeads[0].Id);
        Assert.AreEqual(leads[0].FirstName, actualLeads[0].FirstName);
        Assert.AreEqual(leads[0].LastName, actualLeads[0].LastName);
        Assert.AreEqual(leads[0].Patronymic, actualLeads[0].Patronymic);

        _employeeServiceMock.Verify(l => l.GetAll(), Times.Once);
    }

    [Test]
    public async Task GetById()
    {
        var lead = new EmployeeDto()
        {
            Id = 4,
            FirstName = "Delphine",
            LastName = "Skyler",
            Patronymic = "Richard",
            Email = "Lecought92@gmail.com",
            Password = "BWL|xfzZT}",
            IsDeleted = false
        };

        _employeeServiceMock.Setup(l => l.GetById(lead.Id))
            .ReturnsAsync(lead);

        var actual = await _sut.GetById(lead.Id);

        var actualResult = actual.Result as OkObjectResult;
        var actualLead = actualResult.Value as EmployeeAllInfoResponse;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(lead.Id, actualLead.Id);
        Assert.AreEqual(lead.FirstName, actualLead.FirstName);
        Assert.AreEqual(lead.LastName, actualLead.LastName);
        Assert.AreEqual(lead.Patronymic, actualLead.Patronymic);

        _employeeServiceMock.Verify(l => l.GetById(lead.Id), Times.Once);
    }

    [Test]
    public async Task UpdateTest_ValidRequestPassed_NoContentResultReceived()
    {
        var lead = new EmployeeDto
        {
            Id = 5,
            FirstName = "Jesusa",
            LastName = "Ziyad",
            Patronymic = "Inge",
            Email = "Icund19@gmail.com",
            Password = "4@yn|$Cd|O",
            IsDeleted = false
        };

        var newLead = new UpdateEmployeeRequest
        {
            FirstName = "Danilo",
            LastName = "Oberst",
            Patronymic = "Rebekka"
        };

        _employeeServiceMock.Setup(l => l.Update(lead, lead.Id));

        var actual = await _sut.Update(newLead, lead.Id);

        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _employeeServiceMock.Verify(l => l.Update(It.Is<EmployeeDto>(l =>
        l.FirstName == newLead.FirstName &&
        l.LastName == newLead.LastName &&
        l.Patronymic == newLead.Patronymic 
        ), lead.Id), Times.Once);
    }
}
