using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Users.Commands;

public record CreateUserCommand(
    string Username,
    string PasswordHash,
    string? Email
) : IRequest<Guid>;

internal sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IRepository<User> _repository;

    public CreateUserCommandHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = request.PasswordHash,
            Email = request.Email,
            CreatedAt = DateTime.SpecifyKind(
                DateTime.Now,
                DateTimeKind.Unspecified
            )
        };

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();

        return user.UserId;
    }
}