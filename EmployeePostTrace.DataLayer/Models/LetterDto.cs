﻿
namespace EmployeePostTrace.DataLayer.Models;

public class LetterDto
{
    public int Id { get; set; }
    public string Header { get; set; }
    public string Sender { get; set; }
    public string Recipient { get; set; }
    public string Content { get; set; }
    public DateTime SendingDate { get; set; }
    public bool IsIncoming { get; set; }
    public int EmployeeId { get; set; }
    public bool IsDeleted { get; set; }
}
