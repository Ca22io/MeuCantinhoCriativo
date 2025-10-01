using AutoMapper;
using MeuCantinhoCriativo.Models;
using MeuCantinhoCriativo.ViewModels;

namespace MeuCantinhoCriativo.Mapping
{
    public class HobbyMapping : Profile
    {
        public HobbyMapping()
        {
            // Mapeia do Model para o ViewModel
            CreateMap<Hobby, HobbyViewModel>();

            // Mapeia do ViewModel de volta para o Model
            CreateMap<HobbyViewModel, Hobby>();
        }
    }
}