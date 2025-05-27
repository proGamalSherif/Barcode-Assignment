using Assignment.Models;

namespace Assignment.Interfaces
{
    public interface IPdfGenerator
    {
        byte[] GeneratedPDF(ContentData data);
        byte[] GeneratedPDF(IList<ContentData> data);
    }
}
