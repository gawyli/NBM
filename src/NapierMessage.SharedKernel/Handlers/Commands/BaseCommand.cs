using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using NapierMessage.SharedKernel.Entities;
using NapierMessage.SharedKernel.Interfaces;
using System.Text.Json;

namespace NapierMessage.SharedKernel.Handlers.Commands;
public interface ICommand
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>, ICommand
{
}
public abstract class BaseCommandHandler
{
    private readonly IMessageRepository _messageRepository;

    protected BaseCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    protected async Task WriteToJson(JsonDocument message, CancellationToken cancellationToken)
    {
        await _messageRepository.WriteToJsonFile(message, cancellationToken);
    }
}