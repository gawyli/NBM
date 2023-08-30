using NapierMessage.SharedKernel.Entities;
using NapierMessage.SharedKernel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace NapierMessage.Core.Models;

public class MessageBody
{
    public string Sender { get; set; } = null!;
    public string? Subject { get; set; }
    public string MessageText { get; set; } = string.Empty;
    

}
public class Message : BaseMessage
{
    public string Type { get; set; } = null!;
    public MessageBody Body { get; set; } = new ();
  
}


