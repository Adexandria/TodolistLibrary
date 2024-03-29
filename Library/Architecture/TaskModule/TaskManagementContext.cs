﻿using NHibernate;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Architecture.TaskModule
{
    public class TaskManagementContext : DatabaseManagementContext<TaskManagement>
    {
        public TaskManagementContext(ISession session, TaskManagement context,
            INoteRepository noteRepository,
             IUserRepository userRepository) : base(session)
        {
            Context = context;
            Context.NoteRepository = noteRepository;
            Context.UserRepository = userRepository;
        }
    }
}
