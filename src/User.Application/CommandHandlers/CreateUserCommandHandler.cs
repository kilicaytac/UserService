using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using User.Application.Commands;
using User.Domain.Kernel;
using User.Domain.UserAggregate;

namespace User.Application.CommandHandlers
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

                var user = new User.Domain.UserAggregate.User(command.Firstname, command.Lastname, command.Email, command.Password);

                await _userRepository.InsertAsync(user);

                await _unitOfWork.CommitAsync();
            }

            return true;
        }
    }
}
