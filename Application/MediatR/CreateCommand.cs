using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR
{
    public class CreateCommand<T> : IRequest<T> where T : class
    {
        public T Entity { get; set; }
    }

    public class CreateCommandHandler<T> : IRequestHandler<CreateCommand<T>, T> where T : class
    {
        private readonly IRepository<T> _repository;

        public CreateCommandHandler(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> Handle(CreateCommand<T> request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(request.Entity);
            return request.Entity;
        }
    }

}
