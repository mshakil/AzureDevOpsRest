using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuredevopsapi.Models.Response
{
    public class TokenResponseRest
    {
        public string accessToken { get; set; }
        public string tokenType { get; set; }
    }

}
