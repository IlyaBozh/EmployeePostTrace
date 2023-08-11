
using EmployeePostTrace.BusinessLayer.Services.Interfaces;
using EmployeePostTrace.DataLayer.Models;
using EmployeePostTrace.DataLayer.Repositories.Interfaces;

namespace EmployeePostTrace.BusinessLayer.Services;

public class LetterService : ILetterService 
{
    private readonly ILetterRepository _letterRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public LetterService(ILetterRepository letterRepository, IEmployeeRepository employeeRepository)
    {
        _letterRepository = letterRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<int> Add(LetterDto letter) => await _letterRepository.Add(letter);

    public async Task Delete(int id, bool isDeleted) => await _letterRepository.Delete(id, isDeleted);

    public async Task<List<LetterDto>> GetAllByRecipientId(int recipientId) => await _letterRepository.GetAllByRecipientId(recipientId);

    public async Task<List<LetterDto>> GetAllBySenderId(int senderId) => await _letterRepository.GetAllBySenderId(senderId);

    public async Task<LetterDto> GetById(int id) => await _letterRepository.GetById(id);

    public async Task Update(LetterDto letter) => await _letterRepository.Update(letter);
}
