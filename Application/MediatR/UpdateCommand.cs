using Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatR
{
    public class UpdateCommand<T> : IRequest<UpdateResult> where T : class
    {
        public T Entity { get; set; }
    }
    public class UpdateResult
    {
        public bool Success { get; set; }
    }
    public class UpdateCommandHandler<T> : IRequestHandler<UpdateCommand<T>, UpdateResult> where T : class
    {
        private readonly IRepository<T> _repository;

        public UpdateCommandHandler(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<UpdateResult> Handle(UpdateCommand<T> request, CancellationToken cancellationToken)
        {

            var result = new UpdateResult();
            try
            {
                await _repository.UpdateAsync(request.Entity);
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
