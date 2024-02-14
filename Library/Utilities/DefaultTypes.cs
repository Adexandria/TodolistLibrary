using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services;

namespace TasksLibrary.Utilities
{
    public static class DefaultTypes
    {
        public static string Repository => typeof(IRepository<>).Name;

        public static string Command => typeof(IValidator).Name;

        public static string Authentication => typeof(IAuthToken).Name;
    }
}
