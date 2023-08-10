
using Dapper;
using EmployeePostTrace.DataLayer.Models;
using EmployeePostTrace.DataLayer.Repositories.Interfaces;
using System.Data;

namespace EmployeePostTrace.DataLayer.Repositories;

public class LetterRepository : BaseRepository, ILetterRepository 
{
    public LetterRepository(IDbConnection connection) : base(connection) { }

    public async Task<int> Add(LetterDto letter)
    {
        var id = await _connectionString.QuerySingleAsync<int>(
            StoredProcedures.Letter_Add,
            param: new
            {
                letter.Header,
                letter.Sender,
                letter.Recipient,
                letter.Content,
                letter.SenderId,
                letter.RecipientId
            },
            commandType: CommandType.StoredProcedure);

        return id;
    }

    public async Task Delete(int id, bool isDeleted)
    {
        await _connectionString.ExecuteAsync(
            StoredProcedures.Letter_Delete,
            param: new { id, isDeleted },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<List<LetterDto>> GetAllByRecipientId(int recipientId)
    {
        var letters = (await _connectionString.QueryAsync<LetterDto>(
            StoredProcedures.Letter_GetAllByRecipientId,
            param: new { recipientId },
            commandType: CommandType.StoredProcedure)).ToList();

        return letters;
    }

    public async Task<List<LetterDto>> GetAllBySenderId(int senderId)
    {
        var letters = (await _connectionString.QueryAsync<LetterDto>(
        StoredProcedures.Letter_GetAllBySenderId,
        param: new { senderId },
        commandType: CommandType.StoredProcedure)).ToList();

        return letters;
    }

    public async Task<LetterDto> GetById(int id)
    {
        var letter = (await _connectionString.QueryAsync<LetterDto>(
        StoredProcedures.Letter_GetById,
        param: new { id },
        commandType: CommandType.StoredProcedure)).FirstOrDefault();

        return letter;
    }

    public async Task Update(LetterDto letter)
    {
        await _connectionString.ExecuteAsync(
           StoredProcedures.Letter_Update,
           param: new { letter.Header, letter.Content },
           commandType: CommandType.StoredProcedure);
    }
}
