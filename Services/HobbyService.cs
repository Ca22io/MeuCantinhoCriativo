using AutoMapper;
using MeuCantinhoCriativo.Data;
using MeuCantinhoCriativo.Enum;
using MeuCantinhoCriativo.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeuCantinhoCriativo.Services
{
    public class HobbyService : IHobbyService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HobbyService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Resultado> AdicionarHobby(HobbyViewModel hobbyViewModel)
        {
            var hobby = _mapper.Map<Models.Hobby>(hobbyViewModel);

            await _context.Hobbies.AddAsync(hobby);
            
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Resultado.Sucesso;
            }
            else
            {
                return Resultado.Falha;
            }
        }

        public async Task<Resultado> AtualizarHobby(HobbyViewModel hobbyViewModel)
        {
            var hobby = await _context.Hobbies.FindAsync(hobbyViewModel.Id);

            if (hobby == null)
            {
                return Resultado.NaoEncontrado;
            }

            _mapper.Map(hobbyViewModel, hobby);

            _context.Hobbies.Update(hobby);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Resultado.Sucesso;
            }
            else
            {
                return Resultado.Falha;
            }
        }

        public async Task<IEnumerable<HobbyViewModel>> ObterHobbiesPorUsuario(string userId)
        {
            var ObterHobbies = await _context.Hobbies.Where(h => h.UserId == userId).AsNoTracking().ToListAsync();

            if (ObterHobbies == null)
            {
                return Enumerable.Empty<HobbyViewModel>();
            }
            else
            {
                return _mapper.Map<IEnumerable<HobbyViewModel>>(ObterHobbies);
            }

        }

        public Task<HobbyViewModel> ObterHobbyPorId(int hobbyId, string userId)
        {
            var hobby =  _context.Hobbies.AsNoTracking().FirstOrDefaultAsync(h => h.Id == hobbyId && h.UserId == userId);

            if (hobby == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<Task<HobbyViewModel>>(hobby);
            }
        }

        public async Task<Resultado> RemoverHobby(int hobbyId, string userId)
        {
            var hobby = await _context.Hobbies.FirstOrDefaultAsync(h => h.Id == hobbyId && h.UserId == userId);

            if (hobby == null)
            {
                return Resultado.NaoEncontrado;
            }

            _context.Hobbies.Remove(hobby);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Resultado.Sucesso;
            }
            else
            {
                return Resultado.Falha;
            }
        }
    }
}