using Assignment.Responses;
using Assignment.DTOs;
namespace Assignment.Interfaces
{
    public interface IContentDataService
    {
        Task<RequestResponses<IList<ReadContentDataDTO>>> GetAllAsync();
        Task<RequestResponses<ReadContentDataDTO>> GetByIdAsync(int id);
        Task<RequestResponses<string>> InsertContentData(ModifyContentDataDTO contentData);
        Task<RequestResponses<string>> UpdateContentDataAsync(int id, ModifyContentDataDTO contentData);
        Task<RequestResponses<string>> DeleteContentDataAsync(int id);
        Task<string> SaveImages(IFormFile file);
        Task<RequestResponses<byte[]>> GenerateReportById(int id, string hostName);
        Task<RequestResponses<byte[]>> GenerateReportAll(string hostName);
    }
}
