using Application.Mappers;
using AutoMapper;

namespace Application.UnitTest.Fakers
{
    public class FakeCategoryMapper
    {
        public static IMapper Create()
        {
            MapperConfiguration configCategory = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryMapper());
            });

            return new Mapper(configCategory);
        }
    }
}
