using DataAccess.Dtos;
using DataAccess.Vms;
using Microsoft.AspNetCore.Http;

namespace Service.Contract;

public interface IBlogPostService
{
    Task<List<BlogPostVm>> Get();
    Task<List<BlogPostVm>> GetForReport();
    Task<List<BlogPostVm>> GetForSearch();
    Task<BlogPostVm> GetByIdAsync(int ItemId);
    Task<BlogPostDto> GetForUpdate(int ItemId);
    Task<int> AddAsync(BlogPostDto dto);
    Task<int> UpdateAsync(BlogPostDto dto);
    bool DeleteById(int ItemId);
    Task<bool> InsertGalleryImage(List<IFormFile> imageGalleries, int itemId);
}
