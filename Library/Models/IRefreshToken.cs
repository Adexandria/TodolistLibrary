using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Models
{
    public interface IRefreshToken : IEntity
    {
        abstract string Token { get; set; }
        abstract DateTime Expires { get; set; }
        abstract bool IsRevoked { get; set; }
        abstract UserId UserId { get; set; }
    }
}
