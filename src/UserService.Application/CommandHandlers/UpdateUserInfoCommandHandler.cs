using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.Application.Commands;
using UserService.Domain.Kernel;
using UserService.Domain.UserAggregate;

namespace UserService.Application.CommandHandlers
{
    public class UpdateUserInfoCommandHandler : IRequestHandler<UpdateUserInfoCommand, bool>
    {
        private IUnitOfWork _unitOfWork;
        private IUserRepository _userRepository;

        public UpdateUserInfoCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public Task<bool> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
