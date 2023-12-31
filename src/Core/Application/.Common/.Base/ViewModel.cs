using Application.Email.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace Application.Common.Base;

public abstract partial class ViewModel : ObservableObject
{
    //ViewModel lifecylce
    public virtual void OnViewModelStarting(CancellationToken cancellationToken = default)
    {
        Debug.WriteLine($"{nameof(EmailViewModel)} is starting");
    }
    public virtual void OnViewModelClosing(CancellationToken cancellationToken = default)
    {
        Debug.WriteLine($"{nameof(EmailViewModel)} is closing");
    }
}
