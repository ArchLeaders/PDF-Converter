using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PdfConverter.Helpers;
using PdfConverter.Models;
using System.Collections.ObjectModel;

namespace PdfConverter.ViewModels;

public enum ServiceType
{
    Chromium,
    Syncfusion
}

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<FileItemModel> _files = new();
    
    [ObservableProperty]
    private FileItemModel? _file;

    [ObservableProperty]
    private ServiceType[] _services = {
        ServiceType.Chromium,
        ServiceType.Syncfusion
    };

    [ObservableProperty]
    private ServiceType _service = ServiceType.Chromium;

    [ObservableProperty]
    private string _output = string.Empty;

    [RelayCommand]
    private async Task Browse(string title)
    {
        BrowserDialog dialog = new(BrowserMode.OpenFolder, title, instanceBrowserKey: title.ToLower().Replace(' ', '-'));
        if (await dialog.ShowDialog() is string path) {
            Output = path;
        }
    }
}