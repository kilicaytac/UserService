using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserService.Application.Commands;
using UserService.Domain.Kernel;
using UserService.Domain.UserAggregate;

namespace UserService.Application.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private IUnitOfWork _unitOfWork;
        private IUserRepository _userRepository;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            //do some command validation checks..
            //email uniqueness check..

            using (_unitOfWork)
            {
                await _unitOfWork.BeginAsync();

                var user = new User(command.Firstname, command.Lastname, command.Email, command.Password);

                await _userRepository.InsertAsync(user);

                await _unitOfWork.CommitAsync();
            }

            return true;
        }
    }
}
