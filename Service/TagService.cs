using AutoMapper;
using DataAccess.Contract;
using DataAccess.Dtos;
using DataAccess.Vms;
using Domain;
using Microsoft.AspNetCore.Http;
using Service.Contract;

namespace Service;

public class TagService(IMapper mapper, IUnitOfWork unitOfWork) : ITagService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private Guid _currentUser = Guid.Parse("4b859c11-79f9-4104-9fe9-276aeaf5f115");

    public async Task<List<TagVm>> GetAll()
    {
        var model = await unitOfWork.TagRepository.GetAll();
        var modelVm = mapper.Map<List<TagVm>>(model).ToList();
        return modelVm;
    }

    public async Task<TagVm> GetByIdAsync(int id)
    {
        var res = await unitOfWork.TagRepository.GetById(id);
        var resV = mapper.Map<TagVm>(res);
        return resV;
    }
    
    public async Task<int> AddAsync(TagDto dto)
    {
        if (!dto.IsValid())
            return 0;        
        dto.PrepareDto(_currentUser);
        dto.Slug = dto.NameEn.ToLower().Replace(" ", "-");
        var entity = mapper.Map<Tag>(dto);
        await unitOfWork.TagRepository.AddAsync(entity);
        var result = unitOfWork.SaveChanges();
        return result;
    }

    public async Task<int> UpdateAsync(TagDto dto)
    {
        if(!dto.IsValid())
            return 0;
        #region get current user
        //try
        //{
        //    var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    _currentUser = Guid.Parse(userId);
        //}
        //catch (Exception ex)
        //{
        //    _currentUser = Guid.Parse("4B859C11-79F9-4104-9FE9-276AEAF5F115");
        //    Console.WriteLine(ex.Message);
        //}
        #endregion        
        dto.PrepareDto(_currentUser);
        var entity = mapper.Map<Tag>(dto);
        unitOfWork.TagRepository.Update(entity);
        var res = await unitOfWork.SaveChangesAsync();
        return res;
    }

    public async Task<bool> DeleteById(int id)
    {
        var result = await unitOfWork.TagRepository.DeleteAsync(id);

        if (result == false)
            return false;

        unitOfWork.SaveChanges();
        return true;
    }

    public async Task<TagDto> GetForUpdate(int id)
    {
        var res = await unitOfWork.TagRepository.GetById(id);
        var resV = mapper.Map<TagDto>(res);
        return resV;
    }

    public Task<List<TagVm>> GetForReport()
    {
        throw new NotImplementedException();
    }

    public Task<List<TagVm>> GetForSearch()
    {
        throw new NotImplementedException();
    }
}
