﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Services;

namespace TasksLibrary.Models.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<IRefreshToken>
    {
        Task Delete(Guid entityId);
        Task<UserId> GetUserByRefreshToken(string refreshToken);
    }
}
