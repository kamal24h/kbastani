using AutoMapper;
using DataAccess.Dtos;
using DataAccess.Vms;

namespace Service.Mapper
{
    public class MapperProfile : Profile
    {
        //public MapperProfile(ISysUserRepository sysUserRepository)
        public MapperProfile()
        {
            //#region Required collections for mappings
            //_dbContext = new AppDbContext();
            //var persons = _dbContext.Person.AsNoTracking().ToList();
            //var personVms = persons.Select(p => new PersonVm
            //{
            //    PersonId = p.PersonId,
            //    Name = p.Name,
            //    Family = p.Family,
            //    BirthDate = p.BirthDate
            //}).ToList();

            //var memberVms = new List<LoanMember>
            //{
            //    new LoanMember
            //    {
            //        Name = "Kamal",
            //        Family = "be",
            //        NumberOfShares = 1,
            //        MemberShipDate = DateTime.Parse("1982/09/20"),
            //        Enabled = true
            //    }
            //};
            //#endregion
            //var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var sysUserRepository = new ISysUserRepository(DbContext);

            #region IValueResolvers

            //IValueResolver<object, object, AppUserVm> userResolver;
            //userResolver = new ConditionMap(DBFieldName.CreatedBy, sysUserRepository);

            //IValueResolver<object, object, string> toothTypeValueResolver = null;



            #endregion

            #region Dto's Mapping

            BlogPostDto.ConfigureMapper(this);
            //TagDto.ConfigureMapper(this);
            UserProfileDto.ConfigureMapper(this);


            #endregion

            #region Vm's Mapping

            //AppUserVm.ConfigureMapper(this);
            BlogPostVm.ConfigureMapper(this);          
            TagVm.ConfigureMapper(this);          
            UserProfileVm.ConfigureMapper(this);

            #endregion
        }

    }
}
