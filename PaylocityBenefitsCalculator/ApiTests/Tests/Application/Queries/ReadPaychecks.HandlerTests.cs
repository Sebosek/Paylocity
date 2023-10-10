using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using Api.Application.DTOs.Employee;
using Api.Application.Queries;
using Api.Application.Services.Interfaces;

using LanguageExt.Common;

using MediatR;

using Microsoft.Extensions.Internal;

using Xunit;

namespace ApiTests.Tests.Application.Queries;

public class ReadPaychecksHandlerTests
{
    private readonly ReadPaychecksHandler _sut = new(new MockSender(), new MockSystemClock(), new MockDeductionComputation());

    [Fact]
    public async Task Handle_ValidData_Success()
    {
        var result = await _sut.Handle(new ReadPaychecks(1), CancellationToken.None);
        
        Assert.True(result.Match(_ => true, _ => false));
    }
    
    private class MockSystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => new(new DateTime(2023, 10, 1, CultureInfo.InvariantCulture.Calendar));
    }
    
    private class MockDeductionComputation : IDeductionComputation
    {
        public decimal Compute(GetEmployeeDto employee)
        {
            return 1_000;
        }
    }
        
    private class MockSender : ISender
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = new())
        {
            var result = new Result<GetEmployeeDto>(new GetEmployeeDto
            {
                Salary = 100_000,
            });
            
            return Task.FromResult<TResponse>((TResponse)(object) result);
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = new()) where TRequest : IRequest
            => throw new NotImplementedException();

        public Task<object?> Send(object request, CancellationToken cancellationToken = new())
            => throw new NotImplementedException();

        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request,
            CancellationToken cancellationToken = new()) => throw new NotImplementedException();

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = new())
            => throw new NotImplementedException();
    }
}