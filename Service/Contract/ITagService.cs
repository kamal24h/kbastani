using DataAccess.Dtos;
using DataAccess.Vms;
using Microsoft.AspNetCore.Http;

namespace Service.Contract;

public interface ITagService
{
    Task<List<TagVm>> GetAll();
    Task<List<TagVm>> GetForReport();
    Task<List<TagVm>> GetForSearch();
    Task<TagVm> GetByIdAsync(int ItemId);
    Task<TagDto> GetForUpdate(int ItemId);
    Task<int> AddAsync(TagDto dto);
    Task<int> UpdateAsync(TagDto dto);
    Task<bool> DeleteById(int id);
}
