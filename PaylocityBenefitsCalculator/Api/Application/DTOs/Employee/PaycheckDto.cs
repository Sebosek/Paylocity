namespace Api.Application.DTOs.Employee;

public class PaycheckDto
{
    public decimal TotalAmount { get; set; }

    public decimal Deductions { get; set; }
    
    public decimal Payed { get; set; }
}