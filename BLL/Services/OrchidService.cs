using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class OrchidService : IOrchidService
    {
        private readonly IOrchidRepository _orchidRepository;
        private readonly IMapper _mapper;

        public OrchidService(IOrchidRepository orchidRepository, IMapper mapper)
        {
            _orchidRepository = orchidRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrchidDTO>> GetAllAsync()
        {
            var orchids = await _orchidRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrchidDTO>>(orchids);
        }

        public async Task<OrchidDTO?> GetByIdAsync(int id)
        {
            var orchid = await _orchidRepository.GetByIdAsync(id);
            return orchid == null ? null : _mapper.Map<OrchidDTO>(orchid);
        }

        public async Task<IEnumerable<OrchidDTO>> SearchAsync(string keyword)
        {
            var results = await _orchidRepository.SearchAsync(keyword);
            return _mapper.Map<IEnumerable<OrchidDTO>>(results);
        }

        public async Task<OrchidDTO> CreateAsync(OrchidDTO orchidDto)
        {
            var orchid = _mapper.Map<Orchid>(orchidDto);
            await _orchidRepository.AddAsync(orchid);
            return _mapper.Map<OrchidDTO>(orchid);
        }

        public async Task<OrchidDTO> UpdateAsync(OrchidDTO orchidDto)
        {
            var orchid = await _orchidRepository.GetByIdAsync(orchidDto.Id);
            if (orchid == null)
                throw new KeyNotFoundException("Orchid not found");

            _mapper.Map(orchidDto, orchid);
            await _orchidRepository.UpdateAsync(orchid);

            return _mapper.Map<OrchidDTO>(orchid);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exists = await _orchidRepository.ExistsAsync(id);
            if (!exists) return false;

            await _orchidRepository.DeleteAsync(id);
            return true;
        }
    }
}

