using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Infrastructure.Primitives
{
    internal class FilePath(string path)
    {
        public string Path { get; set; } = path;
    }
}
