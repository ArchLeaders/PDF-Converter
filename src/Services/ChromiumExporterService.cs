using PdfConverter.Component;
using PdfiumViewer;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PdfConverter.Services;

public class ChromiumExporterService : IExporterService
{
    private static readonly string _dll = Path.Combine(Path.GetTempPath(), "pdfium.dll");

    static ChromiumExporterService()
    {
        if (!File.Exists(_dll)) {
            if (typeof(Program).Assembly.GetManifestResourceStream("PdfConverter.Resources.pdfium.dll") is Stream stream) {
                using (FileStream fs = File.Create(_dll)) {
                    stream.CopyTo(fs);
                }

                stream.Dispose();
            }
            else {
                throw new FileNotFoundException("Could not find embedded pdfium!");
            }
        }

        NativeLibrary.Load(_dll);
    }

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
