using AutoMapper;

namespace Domain.Mapper
{
    public static class MappingsConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new ProdutoProfile());
            });
            return config;
        }
    }
}
