using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using PdfConverter.ViewModels;

namespace PdfConverter.Views;

public partial class ShellView : Window
{
    public ShellView()
    {
        InitializeComponent();
        FileList.AddHandler(DragDrop.DropEvent, FileListDropEvent);
    }

    public void FileListDropEvent(object? sender, DragEventArgs e)
    {
        if (DataContext is not ShellViewModel svm) {
            return;
        }

        if (e.Data.GetFiles() is IEnumerable<IStorageItem> paths) {
            foreach (var path in paths.Select(x => x.Path.LocalPath).Where(x => x.ToLower().EndsWith(".pdf") && !svm.Files.Select(x => x.FilePath).Contains(x))) {
                svm.Files.Add(new(path));
            }
        }
    }
}