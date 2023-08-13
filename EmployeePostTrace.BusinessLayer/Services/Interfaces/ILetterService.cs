
using EmployeePostTrace.DataLayer.Models;

namespace EmployeePostTrace.BusinessLayer.Services.Interfaces;

public interface ILetterService
{
    Task<int> Add(LetterDto letter);
    Task Delete(int id, bool isDeleted);
    Task<List<LetterDto>> GetAllByRecipientId(int recipientId);
    Task<List<LetterDto>> GetAllBySenderId(int senderId);
    Task<LetterDto> GetById(int id);
    Task Update(LetterDto letter, int id);
}
