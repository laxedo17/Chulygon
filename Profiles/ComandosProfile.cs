using AutoMapper;
using Chulygon.Dtos;
using Chulygon.Models;

namespace Chulygon.Profiles
{
    /// <summary>
    /// Crea un perfil Comando para Automapper
    /// </summary>
    public class ComandosProfile : Profile
    {
        public ComandosProfile()
        {
            //Orixen -> Destino
            CreateMap<Comando, ComandoLerDto>();
            //mapeamos un obxeto ComandoLerDto a un obxeto Comando para que quede gardado como tal na base de datos
            CreateMap<ComandoCrearDto, Comando>();//mapeamos un obxeto ComandoCrearDto a un obxeto Comando para que quede gardado como tal na base de datos
            CreateMap<ComandoActualizarDto, Comando>();
            CreateMap<Comando, ComandoActualizarDto>();
        }
    }
}