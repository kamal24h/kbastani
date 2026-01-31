using DataAccess.Dtos;
using DataAccess.Vms;
using Microsoft.AspNetCore.Http;

namespace Service.Contract;

public interface IProjectService
{
    Task<List<ProjectVm>> Get();
    Task<List<ProjectVm>> GetForReport();
    Task<List<ProjectVm>> GetForSearch();
    Task<ProjectVm> GetByIdAsync(long ItemId);
    Task<ProjectDto> GetForUpdate(long ItemId);
    Task<long> AddAsync(ProjectDto dto);
    Task<long> UpdateAsync(ProjectDto dto);
    Task<bool> DeleteById(long id);


}
