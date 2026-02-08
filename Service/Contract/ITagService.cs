using DataAccess.Dtos;
using DataAccess.Vms;
using Microsoft.AspNetCore.Http;

namespace Service.Contract;

public interface ITagService
{
    Task<List<TagVm>> GetAll();
    Task<List<TagVm>> GetForReport();
    Task<List<TagVm>> GetForSearch();
    Task<TagVm> GetByIdAsync(long ItemId);
    Task<TagDto> GetForUpdate(long ItemId);
    Task<long> AddAsync(TagDto dto);
    Task<long> UpdateAsync(TagDto dto);
    Task<bool> DeleteById(long id);
}
