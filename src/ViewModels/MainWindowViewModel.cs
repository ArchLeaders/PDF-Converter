using CommunityToolkit.Mvvm.ComponentModel;

namespace PdfConverter.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
}