using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_Integration.Core.domain.Models
{
   public class SapConfig
{
    public string ServiceLayerUrl { get; set; }
    public string CompanyDB { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string SessionId { get; set; }
    public string Version { get; set; }
    public int SessionTimeout { get; set; }
}
}