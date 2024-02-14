using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Models
{
    public class RefreshTokenId
    {
        protected RefreshTokenId()
        {
                
        }

        public RefreshTokenId(Guid id)
        {
           Id = id;
        }
        public virtual Guid Id { get; set; }
    }
}
