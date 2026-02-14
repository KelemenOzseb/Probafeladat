using AutoMapper;
using Entity.Dtos.Camel;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Helper
{
    public class DtoProvider
    {
        public Mapper Mapper { get; }

        public DtoProvider()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateCamelDto, Camel>();
                cfg.CreateMap<CreateCamelDto, Camel>();
            });
            Mapper = new Mapper(config);
        }
    }
}
