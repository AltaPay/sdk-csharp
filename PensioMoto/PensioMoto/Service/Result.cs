﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AltaPay.Service
{
	[ComVisible(true)]
    public enum Result
    {
        Success, Failed, Error, SystemError, AbortedByUser
    }
}
