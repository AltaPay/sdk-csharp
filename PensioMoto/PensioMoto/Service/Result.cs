using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto.Service
{
	[GuidAttribute("ca48c455-9967-40b9-a800-1c884f27ec5b")]
	[ComVisible(true)]
    public enum Result
    {
        Success, Failed, Error, SystemError, AbortedByUser
    }
}
