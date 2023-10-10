using Api.Application.Interfaces;
using Api.Application.Services;
using Api.Application.Services.DeductionSteps;
using Api.Application.Services.Interfaces;
using Api.Infrastructure.Host.ExceptionWriters;
using Api.Infrastructure.Persistence;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Internal;

namespace Api;

public static class CompositionRoot
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddTransient<IExceptionWriter, BadRequestExceptionWriter>();
        services.AddTransient<IExceptionWriter, ConflictExceptionWriter>();
        services.AddTransient<IExceptionWriter, NotFoundExceptionWriter>();
        services.AddTransient<IExceptionWriter, ExceptionWriter>();
        services.AddSingleton<IEmployeesService, EmployeesService>();
        services.AddSingleton<IDependentsService, DependentsService>();
        services.AddSingleton<IDeductionStep, BaseDeductionStep>();
        services.AddSingleton<IDeductionStep, DependentsDeductionStep>();
        services.AddSingleton<IDeductionStep, RichDeductionStep>();
        services.AddSingleton<IDeductionStep, SeniorDeductionStep>();
        services.AddSingleton<IDeductionComputation, DeductionComputation>();
        services.TryAddSingleton<ISystemClock, SystemClock>();

        return services;
    }
}