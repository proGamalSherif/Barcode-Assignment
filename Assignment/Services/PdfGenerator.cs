using Assignment.Interfaces;
using Assignment.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace Assignment.Services
{
    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GeneratedPDF(ContentData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "ContentData is null in GeneratedPDF");
            if (data.ImagePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                var imageName = Path.GetFileName(data.ImagePath);
                var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                data.ImagePath = Path.Combine(wwwrootPath, imageName);
            }
            var document = new PdfDocument();
            var createdPage = document.AddPage();
            var gfx = XGraphics.FromPdfPage(createdPage);
            double margin = 40;
            double y = margin;
            var titleFont = new XFont("Verdana", 15, XFontStyle.Bold);
            var labelFont = new XFont("Arial", 12, XFontStyle.Bold);
            var contentFont = new XFont("Arial", 12);
            double lineHeight = contentFont.GetHeight() * 1.2;
            string title = data.ContentTitle;
            string description = data.ContentDescription;
            string imagePath = data.ImagePath;
            gfx.DrawString($"Entry Information {data.ContentId}", titleFont, XBrushes.Black,
                          new XRect(margin, y, createdPage.Width - 2 * margin, lineHeight),
                          XStringFormats.TopLeft);
            y += lineHeight + 10;
            gfx.DrawLine(new XPen(XColors.Black, 0.5), margin, y, createdPage.Width - margin, y);
            y += 20;
            double imageBoxWidth = 150;
            double imageBoxHeight = 150;
            double spacing = 20;
            double imageX = createdPage.Width - margin - imageBoxWidth;
            double textStartX = margin;
            double contentWidth = imageX - margin - spacing;
            double imgWidth = 0, imgHeight = 0;
            bool imageDrawn = false;
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                try
                {
                    XImage image = XImage.FromFile(imagePath);
                    double imgRatio = Math.Min(imageBoxWidth / image.PixelWidth, imageBoxHeight / image.PixelHeight);
                    imgWidth = image.PixelWidth * imgRatio;
                    imgHeight = image.PixelHeight * imgRatio;
                    double imgY = y + (imageBoxHeight - imgHeight) / 2;
                    gfx.DrawRectangle(XPens.Black, imageX, y, imageBoxWidth, imageBoxHeight);
                    gfx.DrawImage(image, imageX + (imageBoxWidth - imgWidth) / 2, imgY, imgWidth, imgHeight);
                    imageDrawn = true;
                }
                catch (Exception ex)
                {
                    gfx.DrawString("Error loading image: " + ex.Message, contentFont, XBrushes.Red,
                                  new XRect(imageX, y, imageBoxWidth, lineHeight),
                                  XStringFormats.TopLeft);
                }
            }
            double textY = y;
            gfx.DrawString("Title:", labelFont, XBrushes.Black,
                          new XRect(textStartX, textY, contentWidth, lineHeight),
                          XStringFormats.TopLeft);
            textY += lineHeight;

            gfx.DrawString(title, contentFont, XBrushes.Black,
                          new XRect(textStartX, textY, contentWidth, lineHeight),
                          XStringFormats.TopLeft);
            textY += lineHeight + 10;

            gfx.DrawString("Description:", labelFont, XBrushes.Black,
                          new XRect(textStartX, textY, contentWidth, lineHeight),
                          XStringFormats.TopLeft);
            textY += lineHeight;

            gfx.DrawString(description, contentFont, XBrushes.Black,
                          new XRect(textStartX, textY, contentWidth, lineHeight * 3),
                          XStringFormats.TopLeft);
            textY += lineHeight * 3;
            y += Math.Max(imageBoxHeight, textY - y) + 30;
            using var stream = new MemoryStream();
            document.Save(stream, false);
            return stream.ToArray();
        }
        public byte[] GeneratedPDF(IList<ContentData> dataList)
        {
            if (dataList == null || dataList.Count == 0)
                throw new ArgumentNullException(nameof(dataList), "ContentData list is null or empty");
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            double margin = 40;
            double y = margin;
            var titleFont = new XFont("Verdana", 15, XFontStyle.Bold);
            var labelFont = new XFont("Arial", 12, XFontStyle.Bold);
            var contentFont = new XFont("Arial", 12);
            double lineHeight = contentFont.GetHeight() * 1.2;
            double imageBoxWidth = 150;
            double imageBoxHeight = 150;
            double spacing = 20;
            double imageX = page.Width - margin - imageBoxWidth;
            double textStartX = margin;
            double contentWidth = imageX - margin - spacing;
            foreach (var data in dataList)
            {
                if (y + imageBoxHeight + 30 > page.Height)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = margin;
                }
                string imagePath = data.ImagePath;
                if (!string.IsNullOrEmpty(imagePath) && imagePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    var imageName = Path.GetFileName(imagePath);
                    var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    imagePath = Path.Combine(wwwrootPath, imageName);
                }
                gfx.DrawString($"Entry {data.ContentId}", titleFont, XBrushes.Black,
                    new XRect(margin, y, page.Width - 2 * margin, lineHeight),
                    XStringFormats.TopLeft);
                y += lineHeight + 10;
                gfx.DrawLine(new XPen(XColors.Black, 0.5), margin, y, page.Width - margin, y);
                y += 10;
                double imgWidth = 0, imgHeight = 0;
                bool imageDrawn = false;
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    try
                    {
                        XImage image = XImage.FromFile(imagePath);
                        double imgRatio = Math.Min(imageBoxWidth / image.PixelWidth, imageBoxHeight / image.PixelHeight);
                        imgWidth = image.PixelWidth * imgRatio;
                        imgHeight = image.PixelHeight * imgRatio;

                        double imgY = y + (imageBoxHeight - imgHeight) / 2;
                        gfx.DrawRectangle(XPens.Black, imageX, y, imageBoxWidth, imageBoxHeight);
                        gfx.DrawImage(image, imageX + (imageBoxWidth - imgWidth) / 2, imgY, imgWidth, imgHeight);
                        imageDrawn = true;
                    }
                    catch (Exception ex)
                    {
                        gfx.DrawString("Error loading image: " + ex.Message, contentFont, XBrushes.Red,
                            new XRect(imageX, y, imageBoxWidth, lineHeight),
                            XStringFormats.TopLeft);
                    }
                }
                double textY = y;
                gfx.DrawString("Title:", labelFont, XBrushes.Black,
                    new XRect(textStartX, textY, contentWidth, lineHeight),
                    XStringFormats.TopLeft);
                textY += lineHeight;

                gfx.DrawString(data.ContentTitle, contentFont, XBrushes.Black,
                    new XRect(textStartX, textY, contentWidth, lineHeight),
                    XStringFormats.TopLeft);
                textY += lineHeight + 5;

                gfx.DrawString("Description:", labelFont, XBrushes.Black,
                    new XRect(textStartX, textY, contentWidth, lineHeight),
                    XStringFormats.TopLeft);
                textY += lineHeight;

                gfx.DrawString(data.ContentDescription, contentFont, XBrushes.Black,
                    new XRect(textStartX, textY, contentWidth, lineHeight * 3),
                    XStringFormats.TopLeft);
                textY += lineHeight * 3;

                y += Math.Max(imageBoxHeight, textY - y) + 30;
            }
            using var stream = new MemoryStream();
            document.Save(stream, false);
            return stream.ToArray();
        }
    }
}