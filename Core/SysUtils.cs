using System;
using System.Collections.Generic;
using System.Text;

namespace OperatingSystem.Core
{
    public class SysUtils
    {
        public static void Delay(int millis) { Cosmos.HAL.Global.PIT.Wait((uint)millis); }
    }
}
