
using EmployeePostTrace.DataLayer.Models;

namespace EmployeePostTrace.DataLayer.Repositories.Interfaces;

public interface ILetterRepository
{
    Task<int> Add(ILetterRepository letter);
    Task Delete(int id, bool IsDeleted);
    Task<List<LetterDto>> GetAllByRecipientId(int recipientId);
    Task<List<LetterDto>> GetAllBySenderId(int senderId);
    Task<LetterDto> GetById(int id);
    Task update(LetterDto letter);
}
