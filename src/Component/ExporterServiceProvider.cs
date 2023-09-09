using PdfConverter.Services;

namespace PdfConverter.Component;

public enum ExporterServiceType
{
    Chromium,
    Syncfusion
}

public class ExporterServiceProvider
{
    public static IExporterService GetService(ExporterServiceType serviceType)
    {
        return serviceType switch {
            ExporterServiceType.Chromium => new ChromiumExporterService(),
            ExporterServiceType.Syncfusion => new SyncfusionExporterService(),
            _ => throw new ArgumentException($"The provided service type '{serviceType}' is not implemented", nameof(serviceType))
        };
    }
}
