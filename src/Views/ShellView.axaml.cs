using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using PdfConverter.Helpers;
using PdfConverter.ViewModels;

namespace PdfConverter.Views;

public partial class ShellView : Window
{
    public ShellView()
    {
        InitializeComponent();
        FileList.AddHandler(DragDrop.DropEvent, FileListDropEvent);
        FileList.DoubleTapped += FileList_DoubleTapped;
    }

    private async void FileList_DoubleTapped(object? sender, TappedEventArgs e)
    {
        BrowserDialog dialog = new(BrowserMode.OpenFile, "Select PDF Files", filter: "PDF Files:*.pdf|All Files:*.*", instanceBrowserKey: "select-pdf-files");
        if (await dialog.ShowDialog(allowMultiple: true) is IEnumerable<string> paths) {
            AddFiles(paths);
        }
    }

    public void FileListDropEvent(object? sender, DragEventArgs e)
    {

        if (e.Data.GetFiles() is IEnumerable<IStorageItem> paths) {
            AddFiles(paths.Select(x => x.Path.LocalPath));
        }
    }

    private void AddFiles(IEnumerable<string> paths)
    {
        if (DataContext is not ShellViewModel svm) {
            return;
        }

        foreach (var path in paths.Where(x => x.ToLower().EndsWith(".pdf") && !svm.Files.Select(x => x.FilePath).Contains(x))) {
            svm.Files.Add(new(path));
        }
    }
}