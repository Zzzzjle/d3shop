﻿using FluentValidation;
using NetCorePal.D3Shop.Domain.AggregatesModel.Identity.AdminUserAggregate;
using NetCorePal.D3Shop.Domain.AggregatesModel.Identity.AdminUserAggregate.Dto;
using NetCorePal.D3Shop.Infrastructure.Repositories.Identity;
using NetCorePal.D3Shop.Web.Application.Queries.Identity;
using NetCorePal.Extensions.Primitives;

namespace NetCorePal.D3Shop.Web.Application.Commands.Identity;

public record CreateAdminUserCommand(string Name, string Phone, string Password, IEnumerable<AssignAdminUserRoleDto> RolesToBeAssigned)
    : ICommand<AdminUserId>;

public class CreateAdminUserCommandValidator : AbstractValidator<CreateAdminUserCommand>
{
    public CreateAdminUserCommandValidator(AdminUserQuery adminUserQuery)
    {
        RuleFor(u => u.Name).NotEmpty().WithMessage("用户名不能为空");
        RuleFor(u => u.Phone).NotEmpty().WithMessage("手机号不能为空");
        RuleFor(u => u.Password).NotEmpty().WithMessage("密码不能为空");
        RuleFor(u => u.Name).Must(n =>
                adminUserQuery.GetAdminUserByNameAsync(n, CancellationToken.None).GetAwaiter().GetResult() is null)
            .WithMessage(u => $"该用户已存在，Name={u.Name}");
    }
}

public class CreateAdminUserCommandHandler(IAdminUserRepository adminUserRepository)
    : ICommandHandler<CreateAdminUserCommand, AdminUserId>
{
    public async Task<AdminUserId> Handle(CreateAdminUserCommand request, CancellationToken cancellationToken)
    {
        var adminUser = new AdminUser(request.Name, request.Phone, request.Password, request.RolesToBeAssigned);
        await adminUserRepository.AddAsync(adminUser, cancellationToken);
        return adminUser.Id;
    }
}