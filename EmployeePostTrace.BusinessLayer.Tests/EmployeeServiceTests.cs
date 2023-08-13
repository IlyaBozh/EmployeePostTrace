
using EmployeePostTrace.BusinessLayer.Models;
using EmployeePostTrace.BusinessLayer.Services;
using EmployeePostTrace.BusinessLayer.Services.Interfaces;
using EmployeePostTrace.BusinessLayer.Tests.TestModels;
using EmployeePostTrace.DataLayer.Models;
using EmployeePostTrace.DataLayer.Repositories.Interfaces;
using Moq;

namespace EmployeePostTrace.BusinessLayer.Tests;

public class EmployeeServiceTests
{
    private IEmployeeService _sut;
    private Mock<IEmployeeRepository> _employeeRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _sut = new EmployeeService(_employeeRepositoryMock.Object);
    }

    [Test]
    public async Task Add()
    {
        var employee = EmployeeDataForTest.GetEmployee();

        _employeeRepositoryMock.Setup(e => e.Add(It.Is<EmployeeDto>(p => p.Id == employee.Id))).ReturnsAsync(employee.Id);

        var expectedId = employee.Id;

        var actualId = await _sut.Add(employee);

        Assert.That(expectedId, Is.EqualTo(actualId));

        _employeeRepositoryMock.Verify(c => c.Add(It.IsAny<EmployeeDto>()), Times.Once);
    }

    [Test]

    public async Task GetAll()
    {
        var employee = EmployeeDataForTest.GetEmployee();

        _employeeRepositoryMock.Setup(e => e.GetAll()).ReturnsAsync(new List<EmployeeDto>() { employee });

        var expectedEmployees = new List<EmployeeDto>() { employee };

        var actualEmployees = await _sut.GetAll();

        Assert.That(actualEmployees, Is.EquivalentTo(expectedEmployees));
        Assert.AreEqual(expectedEmployees.Count, actualEmployees.Count);

        _employeeRepositoryMock.Verify(c => c.GetAll(), Times.Once);
    }

    [Test]
    public async Task GetByEmail()
    {
        var employee = EmployeeDataForTest.GetEmployee();

        _employeeRepositoryMock.Setup(l => l.GetByEmail(employee.Email)).ReturnsAsync(employee);
        
        var expected = employee;

        var actual = await _sut.GetByEmail(employee.Email);

        Assert.AreEqual(expected, actual);
        _employeeRepositoryMock.Verify(l => l.GetByEmail(actual.Email), Times.Once);
    }

    [Test]
    public async Task GetById()
    {
        var employee = EmployeeDataForTest.GetEmployee();

        _employeeRepositoryMock.Setup(l => l.GetById(employee.Id)).ReturnsAsync(employee);
        var expected = employee;
        //when

        var actual = await _sut.GetById(employee.Id);

        //then
        Assert.AreEqual(expected, actual);
        _employeeRepositoryMock.Verify(l => l.GetById(employee.Id), Times.Once);
    }

    [Test]
    public async Task Update()
    {
        var employee = EmployeeDataForTest.GetEmployee();

        _employeeRepositoryMock.Setup(l => l.GetById(employee.Id)).ReturnsAsync(employee);
        _employeeRepositoryMock.Setup(l => l.Update(employee));

        await _sut.Update(employee, 0);

        _employeeRepositoryMock.Verify(l => l.GetById(It.Is<int>(i => i == 0)), Times.Once);
    }
}
