using System.Drawing.Imaging;

namespace PdfConverter.Component;

public interface IExporterService
{
    public void Export(string path, string output, ImageFormat imageFormat, int rescale);
}
