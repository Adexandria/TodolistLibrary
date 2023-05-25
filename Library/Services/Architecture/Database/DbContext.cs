﻿using TasksLibrary.Extensions;

namespace TasksLibrary.Services.Architecture.Database
{
    public abstract class DbContext<T> : IDbContext
    {
        public T Context { get; set; }
        public abstract Task<ActionResult> CommitAsync();
    }
}