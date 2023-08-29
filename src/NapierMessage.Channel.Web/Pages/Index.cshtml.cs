using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NapierMessage.Core.Handlers;
using NapierMessage.Core.Models;
using NapierMessage.SharedKernel.Entities;
using NapierMessage.SharedKernel.Utilities;
using System.Text.Json;

namespace NapierMessage.Web.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IMediator _mediator;

    public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [BindProperty]
    public Message Message { get; set; }

    [BindProperty]
    public MessageBody MessageBody { get; set; }
    

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        var message = new Message{
            Id = Message.Id,
            Type= Message.Type,
            Body = MessageBody
        };

        var messageJson = await _mediator.Send(new FilterMessageHandler.Command(message), cancellationToken);

        return new JsonResult(messageJson, JsonUtils.JsonOptions);
    }
}
