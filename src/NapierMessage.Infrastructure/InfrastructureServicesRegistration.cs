using Microsoft.Extensions.DependencyInjection;
using NapierMessage.Infrastructure.Repositories;
using NapierMessage.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierMessage.Infrastructure;
public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();

        return services;
    }
}
