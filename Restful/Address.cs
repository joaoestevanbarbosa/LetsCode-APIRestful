using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restful
{
    public struct Address
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int PersonId { get; set; }
        public string StreetName { get; set; }
        public string Zip { get; set; }
    }
}
