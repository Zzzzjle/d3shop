﻿using NetCorePal.D3Shop.Domain.AggregatesModel.Identity.RoleAggregate;

namespace NetCorePal.D3Shop.Domain.AggregatesModel.Identity.AdminUserAggregate
{
    public class AdminUserRole
    {
        protected AdminUserRole(){}
        public AdminUserId AdminUserId { get; private set; } = default!;
        public RoleId RoleId { get; private set; } = default!;
        public string RoleName { get; private set; } = string.Empty;

        public AdminUserRole(AdminUserId adminUserId, RoleId roleId, string roleName)
        {
            AdminUserId = adminUserId;
            RoleId = roleId;
            RoleName = roleName;
        }
    }
}