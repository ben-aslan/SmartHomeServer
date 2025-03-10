﻿using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete;

public class Bot : IEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string UserName { get; set; } = null!;

    [MaxLength(1000)]
    public string Token { get; set; } = null!;

    public long BotChatId { get; set; }

    [MaxLength(2000)]
    public string? WebhookUrl { get; set; }

    public bool Selected { get; set; } = false;
    public bool Active { get; set; } = true;
    public bool Status { get; set; } = true;
}
