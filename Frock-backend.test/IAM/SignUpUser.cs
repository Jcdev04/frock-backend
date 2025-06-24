using Frock_backend.IAM.Application.Internal.CommandServices;
using Frock_backend.IAM.Application.Internal.OutboundServices;
using Frock_backend.IAM.Domain.Model.Commands;
using Frock_backend.IAM.Domain.Repositories;
using Frock_backend.shared.Domain.Repositories;
using Moq;

namespace Frock_backend.test;

public class SignUpUser
{
    private readonly Mock<IUserRepository> _repoMock = new();
    private readonly Mock<IUnitOfWork> _uowMock = new();
    private readonly Mock<IHashingService> _hashingService = new();
    private readonly Mock<ITokenService> _tokenService = new();

    private UserCommandService CreateUserService() =>
        new(_repoMock.Object, _tokenService.Object,_hashingService.Object, _uowMock.Object);

    [Fact]
    public void TestMethod1()
    {

        var command = new SignUpCommand(

        );
        
    }
}
