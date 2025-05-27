using Microsoft.EntityFrameworkCore;
using Assignment.Interfaces;
using Assignment.Models;
using Assignment.Responses;

namespace Assignment.Repositories
{
    public class ContentDataRepository : IContentDataRepository
    {
        private readonly ApplicationDbContext db;
        public ContentDataRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task<RequestResponses<string>> DeleteContentDataAsync(int id)
        {
            var content = await GetByIdAsync(id);
            if(content.IsSuccess)
            {
                db.tbl_ContentData.Remove(content.Data);
                await db.SaveChangesAsync();
                return RequestResponses<string>.Success("Content Deleted Successfully");
            }
            return RequestResponses<string>.Failure("Content With This Id Not Found");
        }
        public async Task<RequestResponses<IList<ContentData>>> GetAllAsync()
        {
            return RequestResponses<IList<ContentData>>.Success(data: await db.tbl_ContentData.ToListAsync());
        }
        public async Task<RequestResponses<ContentData>> GetByIdAsync(int id)
        {
            var content = await db.tbl_ContentData.FirstOrDefaultAsync(c => c.ContentId == id);
            if(content != null)
            {
                return RequestResponses<ContentData>.Success(data: content);
            }
            return RequestResponses<ContentData>.Failure("Content With This Id Not Found");
        }
        public async Task<RequestResponses<string>> InsertContentData(ContentData contentData)
        {
            await db.tbl_ContentData.AddAsync(contentData);
            await db.SaveChangesAsync();
            return RequestResponses<string>.Success("Content Inserted Successfully");
        }
        public async Task<RequestResponses<string>> UpdateContentDataAsync(ContentData contentData)
        {
            var existingContent = await GetByIdAsync(contentData.ContentId);
            if (!existingContent.IsSuccess)
                return RequestResponses<string>.Failure("Content Not Found");
            db.Entry(existingContent.Data).CurrentValues.SetValues(contentData);
            await db.SaveChangesAsync();
            return RequestResponses<string>.Success("Content Updated Successfully");
        }
    }
}
