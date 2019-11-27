using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEfcore.Data
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
