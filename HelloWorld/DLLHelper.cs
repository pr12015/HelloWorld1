using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public static class DLLHelper
    {
        [System.Runtime.InteropServices.DllImport(@"C:\Users\stefan\Desktop\Dll1\Debug\Dll1.dll")]
        public static extern void dllGreeting();
    }

}