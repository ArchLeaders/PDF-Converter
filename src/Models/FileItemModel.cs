using CommunityToolkit.Mvvm.ComponentModel;

namespace PdfConverter.Models;

public partial class FileItemModel : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _filePath = string.Empty;

    public FileItemModel(string path)
    {
        _name = Path.GetFileName(path);
        _filePath = path;
    }
}
