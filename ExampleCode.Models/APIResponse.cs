using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCode.Models
{

    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }

        public bool isExitoso { get; set; }
        public List<string> errorMessage { get; set;}

        public object result { get; set; }
    }
}