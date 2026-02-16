using Data.Repository;
using Entity.Dtos.Camel;
using Entity.Models;
using Logic.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Logic
{
    public class CamelLogic
    {
        IRepository<Camel> _repository;
        DtoProvider _dtoProvider;

        public CamelLogic(IRepository<Camel> repository, DtoProvider dtoProvider)
        {
            _repository = repository;
            _dtoProvider = dtoProvider;
        }

        public async Task<Camel> CreateCamel(CreateCamelDto createCamelDto)
        {
            if (createCamelDto.Name == null)
            {
                throw new ArgumentNullException("Name can not be null");
            }
            if (createCamelDto.HumpCount != null && (createCamelDto.HumpCount > 2 || createCamelDto.HumpCount < 1))
            {
                throw new ArgumentException("Hump count must be between 1 and 2");
            }
            if (createCamelDto.LastFed != null && createCamelDto.LastFed > DateTime.Now)
            {
                throw new ArgumentException("Last fed date cannot be in the future");
            }
            var camel = _dtoProvider.Mapper.Map<Camel>(createCamelDto);
            return await _repository.Create(camel);
        }

        public async Task DeleteCamel(string id)
        {
            var camel = await _repository.GetOne(id);
            if (camel == null)
            {
                throw new KeyNotFoundException("Camel not found");
            }
            await _repository.DeleteById(id);
        }

        public async Task<Camel> UpdateCamel(UpdateCamelDto updateCamelDto, string id)
        {
            var camel = await _repository.GetOne(id);
            if (camel == null)
            {
                throw new KeyNotFoundException("Camel not found");
            }
            if (updateCamelDto.HumpCount != null && (updateCamelDto.HumpCount > 2 || updateCamelDto.HumpCount < 1))
            {
                throw new ArgumentException("Hump count must be between 1 and 2");
            }
            if (updateCamelDto.LastFed != null && updateCamelDto.LastFed > DateTime.Now)
            {
                throw new ArgumentException("Last fed date cannot be in the future");
            }
            camel = _dtoProvider.Mapper.Map(updateCamelDto, camel);
            return await _repository.Update(camel);
        }

        public async Task<Camel> GetCamel(string id)
        {
            var camel = await _repository.GetOne(id);
            if (camel == null)
            {
                throw new KeyNotFoundException("Camel not found");
            }
            return camel;
        }

        public async Task<IEnumerable<Camel>> GetAllCamels()
        {
            var camels = await _repository.GetAll().ToListAsync();
            return camels;
        }
    }
}
