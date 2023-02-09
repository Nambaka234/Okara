using Parfumeria.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parfumeria
{
    public static class Helper
    {
        public static readonly PostgresContext postgres = new PostgresContext();
    }
}
