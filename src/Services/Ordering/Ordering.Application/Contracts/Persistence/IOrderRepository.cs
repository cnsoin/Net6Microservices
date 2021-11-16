﻿using System.Linq.Expressions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence;

public interface IOrderRepository : IAsyncRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserName(int pageNumber, int pageSize, string userName);
}

