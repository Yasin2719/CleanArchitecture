using Application.Mappers;
using AutoMapper;

namespace Application.UnitTest.Fakers
{
    public class FakeProductMapper
    {
        public static IMapper Create()
        {
            MapperConfiguration configProduct = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMapper());
            });

            return new Mapper(configProduct);
        }
    }
}
