using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Entity.Helper
{
    public interface IIdentity
    {
        string Id { get; set; }
    }
}
