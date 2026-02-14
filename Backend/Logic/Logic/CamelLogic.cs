using Data.Repository;
using Entity.Models;
using Logic.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Logic
{
    public class CamelLogic
    {
        Repository<Camel> _repository;
        DtoProvider _dtoProvider;

        public CamelLogic(Repository<Camel> repository, DtoProvider dtoProvider)
        {
            _repository = repository;
            _dtoProvider = dtoProvider;
        }
    }
}
