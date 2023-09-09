using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using PdfConverter.Helpers;
using PdfConverter.ViewModels;
using PdfConverter.Views;

namespace PdfConverter;

public partial class App : Application
{
    public static string Title { get; } = $"PDF Converter - {typeof(App).Assembly.GetName().Version?.ToString(3)}";

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new ShellView {
                DataContext = new ShellViewModel(),
            };

            BrowserDialog.StorageProvider = desktop.MainWindow.StorageProvider;
        }

        base.OnFrameworkInitializationCompleted();
    }
}