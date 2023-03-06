using AutoMapper;
using BuscandoMiTrago.Model;
using BuscandoMiTrago.ModelDTO;

namespace BuscandoMiTrago.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<proSolicitudBebidas, proSolicitudBebidasDTO>();
            CreateMap<proSolicitudBebidasDTO, proSolicitudBebidas>();
        }
    }
}
