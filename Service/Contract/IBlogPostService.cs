using DataAccess.Dtos;
using DataAccess.Vms;
using Microsoft.AspNetCore.Http;

namespace Service.Contract;

public interface IBlogPostService
{
    Task<List<BlogPostVm>> Get();
    Task<List<BlogPostVm>> GetForReport();
    Task<List<BlogPostVm>> GetForSearch();
    Task<BlogPostVm> GetByIdAsync(long ItemId);
    Task<BlogPostDto> GetForUpdate(long ItemId);
    Task<long> AddAsync(BlogPostDto dto);
    Task<long> UpdateAsync(BlogPostDto dto);
    Task<bool> DeleteById(long id);
    Task<bool> InsertGalleryImage(List<IFormFile> imageGalleries, long itemId);
}
