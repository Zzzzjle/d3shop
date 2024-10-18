﻿using FluentValidation;
using NetCorePal.D3Shop.Domain.AggregatesModel.Identity.RoleAggregate;
using NetCorePal.D3Shop.Infrastructure.Repositories.Identity;
using NetCorePal.D3Shop.Web.Application.Queries.Identity;
using NetCorePal.Extensions.Primitives;

namespace NetCorePal.D3Shop.Web.Application.Commands.Identity;

public record UpdateRoleInfoCommand(RoleId RoleId, string Name, string Description) : ICommand;

public class UpdateRoleInfoCommandValidator : AbstractValidator<UpdateRoleInfoCommand>
{
    public UpdateRoleInfoCommandValidator(RoleQuery roleQuery)
    {
        RuleFor(x => x.RoleId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => new { x.Name, x.RoleId }).MustAsync(async (r, ct) =>
        {
            var s = await roleQuery.GetRoleByNameAsync(r.Name, ct);
            return s is null || s.Id == r.RoleId;
        });
    }
}

public class UpdateRoleInfoCommandHandler(IRoleRepository roleRepository) : ICommandHandler<UpdateRoleInfoCommand>
{
    public async Task Handle(UpdateRoleInfoCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetAsync(request.RoleId, cancellationToken) ??
                    throw new KnownException($"未找到角色，RoleId = {request.RoleId}");
        role.UpdateRoleInfo(request.Name, request.Description);
    }
}