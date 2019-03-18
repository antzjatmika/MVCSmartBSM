using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace MVCSmartClient01.Components
{
    [InheritedExport]
    public interface IUserSession
    {
        string Username { get; }
        string BearerToken { get; }
    }
}