using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Common.Primitives
{
    public abstract class AggregateRoot<TId>:Entity<TId>;
    
}
