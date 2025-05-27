using Assignment.Models;
using Assignment.Responses;

namespace Assignment.Interfaces
{
    public interface IContentDataRepository
    {
        Task<RequestResponses<IList<ContentData>>> GetAllAsync();
        Task<RequestResponses<ContentData>> GetByIdAsync(int id);
        Task<RequestResponses<string>> InsertContentData(ContentData contentData);
        Task<RequestResponses<string>> UpdateContentDataAsync(ContentData contentData);
        Task<RequestResponses<string>> DeleteContentDataAsync(int id);
    }
}


