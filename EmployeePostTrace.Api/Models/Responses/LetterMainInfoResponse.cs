namespace EmployeePostTrace.Api.Models.Responses
{
    public class LetterMainInfoResponse
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Sender { get; set; }
        public DateTime SendingDate { get; set; }
    }
}
