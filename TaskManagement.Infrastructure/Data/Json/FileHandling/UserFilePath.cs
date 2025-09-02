using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Infrastructure.Primitives;

namespace TaskManagement.Infrastructure.Data.Json.FileHandling
{
    internal class UserFilePath(string path) : FilePath(path);
}
