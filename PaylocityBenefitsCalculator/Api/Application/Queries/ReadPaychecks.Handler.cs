using System.Globalization;

using Api.Application.DTOs.Employee;
using Api.Application.Services.Interfaces;

using LanguageExt.Common;

using MediatR;

using Microsoft.Extensions.Internal;

namespace Api.Application.Queries;

internal class ReadPaychecksHandler : IRequestHandler<ReadPaychecks, Result<PaycheckDto>>
{
    private readonly ISender _mediator;
    
    private readonly ISystemClock _clock;

    private readonly IDeductionComputation _deductions;

    public ReadPaychecksHandler(ISender mediator, ISystemClock clock, IDeductionComputation deductions)
    {
        _mediator = mediator;
        _clock = clock;
        _deductions = deductions;
    }

    public async Task<Result<PaycheckDto>> Handle(
        ReadPaychecks request, 
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new FindEmployee(request.Id), cancellationToken);

        return result.Map(e => ComputePaychecks(request, e));
    }

    private PaycheckDto ComputePaychecks(ReadPaychecks request, GetEmployeeDto employee)
    {
        var monthly = _deductions.Compute(employee);
        var yearly = monthly * 12;
        var year = _clock.UtcNow.Year;
        var days = CultureInfo.InvariantCulture.Calendar.GetDaysInYear(year);
        var perday = days / 14m;
        var daily = employee.Salary / days; // daily salary
        var deduction = yearly / days; // deduction in one day

        return new PaycheckDto
        {
            Deductions = deduction * perday,
            TotalAmount = daily * perday,
            Payed = daily * perday - deduction * perday,
        };
    }
}