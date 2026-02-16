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
                cfg.CreateMap<UpdateCamelDto, Camel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

                cfg.CreateMap<CreateCamelDto, Camel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            });
            Mapper = new Mapper(config);
        }
    }
}
