using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Restful
{
    public class Person
    {
       
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Cpf { get; set; }

    }
}
