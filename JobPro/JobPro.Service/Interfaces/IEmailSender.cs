﻿// JobPro.Service/Email/IEmailSender.cs
public interface IEmailSender
{
    Task SendEmailAsync(string to, string subject, string body);
}