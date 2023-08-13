using EmployeePostTrace.DataLayer.Models;

namespace EmployeePostTrace.BusinessLayer.Tests.TestModels;

public static class LetterDataForTests
{
    public static LetterDto GetLetter()
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

        return letter;
    }
}
