using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PdfConverter.Component;
using PdfConverter.Helpers;
using PdfConverter.Models;
using System.Collections.ObjectModel;

namespace PdfConverter.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<FileItemModel> _files = new();
    
    [ObservableProperty]
    private FileItemModel? _file;

    [ObservableProperty]
    private ExporterServiceType[] _services = {
        ExporterServiceType.Chromium,
        ExporterServiceType.Syncfusion
    };

    [ObservableProperty]
    private ExporterServiceType _service = ExporterServiceType.Chromium;

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

    [RelayCommand]
    private Task Export()
    {
        IExporterService exporter = ExporterServiceProvider.GetService(Service);

        bool improviseOutput = !Directory.Exists(Output);

        foreach (var file in Files.Select(x => x.FilePath)) {
            string output = improviseOutput ? Path.GetDirectoryName(file) ?? string.Empty : Output;
            Directory.CreateDirectory(output);
            exporter.Export(file, output);
        }

        return Task.CompletedTask;
    }
}