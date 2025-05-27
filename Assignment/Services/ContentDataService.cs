using AutoMapper;
using Assignment.Interfaces;
using Assignment.Models;
using Assignment.Responses;
using Assignment.DTOs;
namespace Assignment.Services
{
    public class ContentDataService : IContentDataService
    {
        private readonly IContentDataRepository contentDataRepo;
        private readonly IMapper mapper;
        private readonly IPdfGenerator pdfGenerator;
        public ContentDataService(IContentDataRepository _contentDataRepo,IMapper _mapper,IPdfGenerator _pdfGenerator)
        {
            contentDataRepo= _contentDataRepo;  
            mapper= _mapper;
            pdfGenerator= _pdfGenerator;
        }
        public async Task<RequestResponses<string>> DeleteContentDataAsync(int id)
        {
            return await contentDataRepo.DeleteContentDataAsync(id);
        }
        public async Task<RequestResponses<IList<ReadContentDataDTO>>> GetAllAsync()
        {
            var result = await contentDataRepo.GetAllAsync();
            if (result.IsSuccess)
            {
                var mappedResult = mapper.Map<IList<ReadContentDataDTO>>(result.Data);
                return RequestResponses<IList<ReadContentDataDTO>>.Success(data: mappedResult);
            }
            return RequestResponses<IList<ReadContentDataDTO>>.Failure(message:result.Message);
        }
        public async Task<RequestResponses<ReadContentDataDTO>> GetByIdAsync(int id)
        {
            var result = await contentDataRepo.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                var mappedResult = mapper.Map<ReadContentDataDTO>(result.Data);
                return RequestResponses<ReadContentDataDTO>.Success(data: mappedResult);
            }
            return RequestResponses<ReadContentDataDTO>.Failure(message: result.Message);
        }
        public async Task<RequestResponses<string>> InsertContentData(ModifyContentDataDTO contentData)
        {
            if (contentData == null)
                return RequestResponses<string>.Failure("Content data cannot be null");
            var mappedDTO = mapper.Map<ContentData>(contentData);
            mappedDTO.ImagePath= await SaveImages(contentData.ImagePath);
            return await contentDataRepo.InsertContentData(mappedDTO);
        }
        public async Task<RequestResponses<string>> UpdateContentDataAsync(int id, ModifyContentDataDTO contentData)
        {
            if (contentData == null)
                return RequestResponses<string>.Failure("Content data cannot be null");
            var mappedDTO = mapper.Map<ContentData>(contentData);
            mappedDTO.ContentId = id;
            return await contentDataRepo.UpdateContentDataAsync(mappedDTO);
        }
        public async Task<string> SaveImages(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);
            var imagePath = Path.Combine(imagesFolder, imageName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"/images/{imageName}";
        }
        public async Task<RequestResponses<byte[]>> GenerateReportById(int id,string hostName)
        {
            var currentData = await contentDataRepo.GetByIdAsync(id);
            currentData.Data.ImagePath = $"{hostName}{currentData.Data.ImagePath}";
            var pdf = pdfGenerator.GeneratedPDF(currentData.Data);
            return RequestResponses<byte[]>.Success(data:pdf);
        }
        public async Task<RequestResponses<byte[]>> GenerateReportAll(string hostName)
        {
            var allData = await contentDataRepo.GetAllAsync();
            foreach (var item in allData.Data)
            {
                item.ImagePath = $"{hostName}{item.ImagePath}";
            }
            var pdf = pdfGenerator.GeneratedPDF(allData.Data);
            return RequestResponses<byte[]>.Success(data: pdf);
        }
    }
}
