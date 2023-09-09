using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PdfConverter.Component;
using PdfConverter.Helpers;
using PdfConverter.Models;
using PdfConverter.Services;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;

namespace PdfConverter.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<FileItemModel> _files = new();

    [ObservableProperty]
    private FileItemModel? _file;

    [ObservableProperty]
    private IExporterService[] _services = {
        new ChromiumExporterService()
    };

    [ObservableProperty]
    private int _serviceIndex = 0;

    [ObservableProperty]
    private ImageFormat[] _imageFormats = {
        ImageFormat.Gif,
        ImageFormat.Jpeg,
        ImageFormat.Png,
        ImageFormat.Tiff
    };

    [ObservableProperty]
    private ImageFormat _imageFormat = ImageFormat.Jpeg;

    [ObservableProperty]
    private int? _rescale = 4;

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
        bool improviseOutput = !Directory.Exists(Output);

        foreach (var file in Files.Select(x => x.FilePath)) {
            string output = improviseOutput ? Path.GetDirectoryName(file) ?? string.Empty : Output;
            Directory.CreateDirectory(output);
            Services[ServiceIndex].Export(file, output, ImageFormat, Rescale ?? 4);
        }

        return Task.CompletedTask;
    }
}