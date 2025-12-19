using System.Security.Claims;
using AutoMapper;
using DataAccess.Contract;
using DataAccess.Dtos;
using DataAccess.Vms;
using Domain;
using Microsoft.AspNetCore.Http;
using Service.Contract;

namespace Service;

public class BlogPostService(IMapper mapper, IUnitOfWork unitOfWork,
    IBlogPostRepository _blogPostRepository) : IBlogPostService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private Guid _currentUser = Guid.Parse("4b859c11-79f9-4104-9fe9-276aeaf5f115");

    public async Task<List<BlogPostVm>> Get()
    {
        var model = await _blogPostRepository.GetListAsync();
        var modelVm = _mapper.Map<List<BlogPostVm>>(model).ToList();
        return modelVm;
    }

    //public async Task<List<BlogPostForReportVm>> GetForReport()
    //{
    //    var ItemMembers = await _blogPostRepository.Get();
    //    var res = _mapper.Map<List<BlogPostForReportVm>>(ItemMembers).ToList();
    //    return res;
    //}



    public async Task<List<BlogPostVm>> GetForSearch() 
    {
        var res = await _blogPostRepository.GetListAsync();
        var resVm = _mapper.Map<List<BlogPostVm>>(res);
        return resVm;
    }


    public async Task<BlogPostVm> GetByIdAsync(int id)
    {
        var res = await _blogPostRepository.GetById(id);
        var resV = _mapper.Map<BlogPostVm>(res);
        return resV;
    }
    public async Task<BlogPostDto> GetForUpdate(int id)
    {
        var res = await _blogPostRepository.GetById(id);
        var resV = _mapper.Map<BlogPostDto>(res);
        return resV;
    }

    //public async Task<ItemShowViewModel> GetProductForShowById(int id)
    //{
    //    var model = await GetByIdAsync(id);
    //    var viewModel = new ItemShowViewModel()
    //    {
    //        ItemId = id,
    //        Name = model.Title,
    //        Count = (int)model.Balance,
    //        Price = (int)model.DefaultSellingPrice.GetValueOrDefault(),
    //        Description = "گروه محصولات: " + model.CategoryTitle + " با عنوان تخصصی :" + model.ProfessionalTitle,
    //        MainImage = model.MainImage,
    //        //Images = model.Attachments
    //    };
    //    var properties = await _BlogPostPropertiesRepository.GetByItemId(model.ItemId);
    //    foreach (var i in properties)
    //    {
    //        viewModel.Properties.Add(i.CategoryProperty.Title, i.Value);
    //    }        

    //    return viewModel;
    //}


    public async Task<int> AddAsync(BlogPostDto dto)
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
        var entity = _mapper.Map<BlogPost>(dto);
        await _blogPostRepository.AddAsync(entity);
        //await _docSerialRepository.IncrementSerieByDocumentId((int)SysDocumentTypeEnum.InventoryItems);
        var result = _unitOfWork.SaveChanges();
        //var vm = _mapper.Map<BlogPostVm>(addedItem);
        return result;
    }

    public async Task<int> UpdateAsync(BlogPostDto dto)
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
        var entity = _mapper.Map<BlogPost>(dto);
        _blogPostRepository.Update(entity);
        var res = await _unitOfWork.SaveChangesAsync();
        return res;
    }

    public bool DeleteById(int id)
    {
        var result = _blogPostRepository.DeleteBy(id);

        if (result == false)
            return false;

        _unitOfWork.SaveChanges();
        return true;
    }

    public Task<List<BlogPostVm>> GetForReport()
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertGalleryImage(List<IFormFile> imageGalleries, int itemId)
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
    //        List<BlogPostAttachment> imageGalleries = [];

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


    //                imageGalleries.Add(new BlogPostAttachment
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
