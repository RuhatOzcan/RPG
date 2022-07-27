using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RPG.DTos.Character;

namespace RPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character(){Id=1, Name="Sam"}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);

            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                Character character = characters.First(c => c.Id == id);


                characters.Remove(character);
                response.Data=characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
  
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>> { Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList() };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharecter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {

                Character character = characters.FirstOrDefault(c => c.Id == updatedCharecter.Id);


                character.HitPoints = updatedCharecter.HitPoints;
                character.Name = updatedCharecter.Name;
                character.Strength = updatedCharecter.Strength;
                character.Defense = updatedCharecter.Defense;
                character.Intelligence = updatedCharecter.Intelligence;
                character.Class = updatedCharecter.Class;

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}