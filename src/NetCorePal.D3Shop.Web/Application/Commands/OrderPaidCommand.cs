﻿using NetCorePal.D3Shop.Domain.AggregatesModel.OrderAggregate;
using NetCorePal.Extensions.Primitives;

namespace NetCorePal.D3Shop.Web.Application.Commands
{
    public record class OrderPaidCommand(OrderId OrderId) : ICommand;
}
