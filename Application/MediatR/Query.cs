﻿using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR
{
   
        public class GetAllQuery<T> : IRequest<IEnumerable<T>> where T : class { }

        public class GetAllQueryHandler<T> : IRequestHandler<GetAllQuery<T>, IEnumerable<T>> where T : class
        {
            private readonly IRepository<T> _repository;

            public GetAllQueryHandler(IRepository<T> repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<T>> Handle(GetAllQuery<T> request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();
            }
        }
     
}
