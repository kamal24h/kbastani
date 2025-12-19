//using AutoMapper;

//namespace Service.Mapper
//{
//    public enum DBFieldName
//    {
//        CreatedBy
//    }
//    public class ConditionMap : 
//        //IValueResolver<object, object, CrmCustomerVm>
//        //IValueResolver<object, object, AppUserVm>
//    {
//        private readonly DBFieldName _dBFieldName;
//        //private readonly ISysUserRepository _sysUserRepository;
        
//        public ConditionMap(DBFieldName dBFieldName)
//        {
//            _dBFieldName = dBFieldName;
//            //_sysUserRepository = sysUserRepository;
//        }

//        //public CrmCustomerVm Resolve(object source, object destination, CrmCustomerVm destMember, ResolutionContext context)
//        //{

//        //    throw new NotImplementedException();
//        //}

//        //public AppUserVm Resolve(object source, object destination, AppUserVm destMember, ResolutionContext context)
//        //{
//        //    if(source.GetType().GetProperty(_dBFieldName.ToString())?.GetValue(source) == null) return null;
//        //    var userId = (Guid)source.GetType().GetProperty(_dBFieldName.ToString())!.GetValue(source)!;
//        //    if(userId == Guid.Empty) return null;
//        //    //var userString = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
//        //    var appUser = _sysUserRepository.GetByGuid(userId);
//        //    if(appUser == null) return null;
//        //    var result = context.Mapper.Map<AppUserVm>(appUser);
//        //    return result;
//        //}
//    }
//}
