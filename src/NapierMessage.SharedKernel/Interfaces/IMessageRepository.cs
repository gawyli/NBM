using System.Text.Json;

namespace NapierMessage.SharedKernel.Interfaces;

public interface IMessageRepository
{
    public Task WriteToJsonFile(JsonDocument messageJson, CancellationToken cancellationToken);
}