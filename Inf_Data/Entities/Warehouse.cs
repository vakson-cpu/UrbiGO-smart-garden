using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inf_Data.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string Name { get; set; }    
        public List<Plant> Plants { get; set; } = new();

    }
}
