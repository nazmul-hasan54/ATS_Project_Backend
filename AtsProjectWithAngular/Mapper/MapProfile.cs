using AtsProjectWithAngular.Domain;
using AtsProjectWithAngular.DTO;
using AutoMapper;

namespace AtsProjectWithAngular.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Blog, BlogDTO>()
                .ForMember(o => o.BlogId, q => q.MapFrom(s => s.BlogId))
                .ForMember(o => o.BlogTitle, q => q.MapFrom(s => s.BlogTitle))
                .ForMember(o => o.BlogDescription, q => q.MapFrom(s => s.BlogDescription))
                .ForMember(o => o.CategoryId, q => q.MapFrom(s => s.CategoryId))
                .ForMember(o => o.BlogImage, q => q.MapFrom(s => s.BlogImage))
                .ForMember(o => o.CreatedDate, q => q.MapFrom(s => s.CreatedDate))
                .ReverseMap();

            CreateMap<Category, CategoryDTO>()
                .ForMember(o => o.Id, q => q.MapFrom(s => s.Id))
                .ForMember(o => o.CategoryName, q => q.MapFrom(s => s.CategoryName))
                .ReverseMap();

            CreateMap<User, UserLoginDTO>()
                .ForMember(o => o.UserName, q => q.MapFrom(s => s.UserName))
                .ForMember(o => o.Password, q => q.MapFrom(s => s.Password))
                .ReverseMap();
        }

    }
}
