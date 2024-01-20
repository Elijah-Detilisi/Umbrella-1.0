﻿using Application.Email.Models;
using Application.User.Models;

namespace Application.Email.Services;

public interface IEmailFetcher : IDisposable
{
    //Properties
    bool IsConnected { get; }
    
    //Methods
    Task<List<EmailModel>> LoadEmailsAsync(CancellationToken cancellationToken = default);
    Task ConnectAsync(UserModel userModel, CancellationToken cancellationToken = default);
}
