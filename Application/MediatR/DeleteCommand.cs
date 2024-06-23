using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR
{
    public class DeleteCommand<T> : IRequest<DeleteResult> where T : class
    {
        public int Id { get; set; }
    }

    public class DeleteResult
    {
        public bool Success { get; set; }
    }

    public class DeleteCommandHandler<T> : IRequestHandler<DeleteCommand<T>, DeleteResult> where T : class
    {
        private readonly IRepository<T> _repository;

        public DeleteCommandHandler(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<DeleteResult> Handle(DeleteCommand<T> request, CancellationToken cancellationToken)
        {
            var result = new DeleteResult();
            try
            {
                await _repository.DeleteAsync(request.Id);
                result.Success = true;
            }
            catch (Exception)
            {
                result.Success = false;
            }
            return result;
        }
    }
}
