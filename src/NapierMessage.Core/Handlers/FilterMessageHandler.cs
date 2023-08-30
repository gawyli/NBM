using MediatR;
using Microsoft.AspNetCore.Mvc;
using NapierMessage.Core.Models;
using NapierMessage.SharedKernel.Entities;
using NapierMessage.SharedKernel.Handlers.Commands;
using NapierMessage.SharedKernel.Interfaces;
using NapierMessage.SharedKernel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace NapierMessage.Core.Handlers;
public class FilterMessageHandler
{
    public class Command : ICommand<JsonDocument>
    {
        public Message Message { get; set; }
        public Command(Message message)
        {
            Message = message;
        }

        public Command() : this(null)
        { 
        }
    }
    public class Handler : BaseCommandHandler, IRequestHandler<Command, JsonDocument>
    {
        public Handler(IMessageRepository messageRepository) : base(messageRepository)
        {
        }

        public async Task<JsonDocument> Handle(Command request, CancellationToken cancellationToken)
        {
            string messageType = request.Message!.Type;
            JsonDocument messageJson;

            if (messageType == "Sms")
            {
                messageJson = Sms.ProcessSms(request.Message);
            }
            else if (messageType == "Email")
            {
                messageJson = Email.ProcessEmail(request.Message);
            }
            else // Assume messageType is "Tweet" or something else
            {
                messageJson = Tweet.ProcessTweet(request.Message);
            }

            await WriteToJson(messageJson, cancellationToken);

            return messageJson;
        }   
    }
}


