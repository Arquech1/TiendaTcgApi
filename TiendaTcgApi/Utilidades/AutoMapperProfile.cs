using AutoMapper;
using TiendaTcgApi.DTOS;
using TiendaTcgApi.Entidades;

namespace TiendaTcgApi.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Producto, ProductoDTO>().ReverseMap();
        }
    }
    
    
}
