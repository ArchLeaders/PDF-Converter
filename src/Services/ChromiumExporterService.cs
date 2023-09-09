using PdfConverter.Component;
using PdfiumViewer;
using System.Drawing;
using System.Drawing.Imaging;

namespace PdfConverter.Services;

public class ChromiumExporterService : IExporterService
{
    public static float DPI { get; set; } = 72;

    public void Export(string path, string output, ImageFormat imageFormat, int rescale)
    {
        PdfDocument pdf = PdfDocument.Load(path);

        for (int i = 0; i < pdf.PageCount; i++) {
            string outputFile = Path.Combine(output, $"{Path.GetFileNameWithoutExtension(path)}-{i}.{imageFormat.ToString().ToLower()}");
            using FileStream fs = File.Create(outputFile);

            Image bitmap = pdf.Render(i, (int)pdf.PageSizes[i].Width * rescale, (int)pdf.PageSizes[i].Height * rescale, DPI, DPI, forPrinting: false);
            bitmap.Save(fs, imageFormat);
        }
    }

    public override string ToString()
    {
        return "Chromium";
    }
}
