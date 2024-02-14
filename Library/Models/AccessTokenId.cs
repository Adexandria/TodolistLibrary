using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Models
{
    public class AccessTokenId
    {
        protected AccessTokenId()
        {
                
        }

        public AccessTokenId(Guid id)
        {
            Id = id;
        }
        public virtual Guid Id { get; set; }
    }
}
