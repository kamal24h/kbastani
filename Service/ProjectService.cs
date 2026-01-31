using System.Security.Claims;
using AutoMapper;
using DataAccess.Contract;
using DataAccess.Dtos;
using DataAccess.Vms;
using Domain;
using Microsoft.AspNetCore.Http;
using Service.Contract;

namespace Service;

public class ProjectService(IMapper mapper, IUnitOfWork unitOfWork,
    IProjectRepository _projectRepository) : IProjectService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private Guid _currentUser = Guid.Parse("4b859c11-79f9-4104-9fe9-276aeaf5f115");

    public async Task<List<ProjectVm>> Get()
    {
        var model = await _projectRepository.GetListAsync();
        var modelVm = _mapper.Map<List<ProjectVm>>(model).ToList();
        return modelVm;
    }

    public async Task<List<ProjectVm>> GetForSearch() 
    {
        var res = await _projectRepository.GetListAsync();
        var resVm = _mapper.Map<List<ProjectVm>>(res);
        return resVm;
    }


    public async Task<ProjectVm> GetByIdAsync(long id)
    {
        var res = await _projectRepository.GetById(id);
        var resV = _mapper.Map<ProjectVm>(res);
        return resV;
    }
    public async Task<ProjectDto> GetForUpdate(long id)
    {
        var res = await _projectRepository.GetById(id);
        var resV = _mapper.Map<ProjectDto>(res);
        return resV;
    }    


    public async Task<long> AddAsync(ProjectDto dto)
    {
        if (!dto.IsValid())
            return 0;
        try
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _currentUser = Guid.Parse(userId);
        }
        catch (Exception ex)
        {
            _currentUser = Guid.Parse("4B859C11-79F9-4104-9FE9-276AEAF5F115");
            Console.WriteLine(ex.Message);
        }
        dto.PrepareDto(_currentUser);
        var entity = _mapper.Map<Project>(dto);
        await _projectRepository.AddAsync(entity);
        //await _docSerialRepository.IncrementSerieByDocumentId((int)SysDocumentTypeEnum.InventoryItems);
        var result = _unitOfWork.SaveChanges();
        //var vm = _mapper.Map<ProjectVm>(addedItem);
        return result;
    }

    public async Task<long> UpdateAsync(ProjectDto dto)
    {
        if(!dto.IsValid())
            return 0;
        try
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _currentUser = Guid.Parse(userId);
        }
        catch (Exception ex)
        {
            _currentUser = Guid.Parse("4B859C11-79F9-4104-9FE9-276AEAF5F115");
            Console.WriteLine(ex.Message);
        }
        dto.PrepareDto(_currentUser);
        //return new SingleResponse<int>(0, System.Net.HttpStatusCode.BadRequest);
        var entity = _mapper.Map<Project>(dto);
        _projectRepository.Update(entity);
        var res = await _unitOfWork.SaveChangesAsync();
        return res;
    }

    public async Task<bool> DeleteById(long id)
    {
        var result = await _projectRepository.DeleteById(id);

        if (result == false)
            return false;

        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public Task<List<ProjectVm>> GetForReport()
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertGalleryImage(List<IFormFile> imageGalleries, long itemId)
    {
        throw new NotImplementedException();
    }

    //public async Task<List<ItemGalleryShowViewModel>> GetGalleryImages(int itemId)
    //{
    //    var images = await _AttachmentRepository.GetAllByItemId(itemId);
    //    var galleryList = new List<ItemGalleryShowViewModel>();

    //    foreach (var image in images)
    //    {
    //        var gallery = new ItemGalleryShowViewModel()
    //        {
    //            ItemId = itemId,
    //            ImageId = image.ItemAttachmentId,
    //            Picture = image.FileName

    //        };
    //        galleryList.Add(gallery);
    //    };
    //    return galleryList;
    //}

    //public async Task<bool> InsertGalleryImage(List<IFormFile> imageGallery, int itemId)
    //{
    //    if (imageGallery != null)
    //    {
    //        var GalleryImagesFileNewName = "";
    //        List<ProjectAttachment> imageGalleries = [];

    //        for (int i = 0; i < imageGallery.Count; i++)
    //        {
    //            if (imageGallery[i].Length > 0  &&
    //                imageGallery[i].ContentType == "jpg")
    //            {
    //                // Get the filename and extension
    //                var GalleryImagesFileName = Path.GetFileName(imageGallery[i].FileName);
    //                var GalleryImagesFileExt = Path.GetExtension(GalleryImagesFileName);
    //                // Generate a unique filename
    //                GalleryImagesFileNewName = Guid.NewGuid().ToString() + GalleryImagesFileExt;

    //                // Combine the path with the filename
    //                string GalleryImagesFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImageProducts/");

    //                // Save the file to the server
    //                imageGallery[i].AddImageToServer(GalleryImagesFileNewName,
    //                    GalleryImagesFilePath, 50, 100);


    //                imageGalleries.Add(new ProjectAttachment
    //                {
    //                    ItemAttachmentGuid = Guid.NewGuid(),
    //                    ItemId = itemId,
    //                    FileName = GalleryImagesFileNewName,
    //                    FilePath = GalleryImagesFilePath,
    //                    CreatedBy = _currentUser,
    //                    CreateDate = DateTime.Now
    //                });

    //            }
    //        }

    //        // Save ImageName Of Product On the Database
    //        await _AttachmentRepository.AddList(imageGalleries);


    //        await _unitOfWork.SaveChangesAsync();
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

}
