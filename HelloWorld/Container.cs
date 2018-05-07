using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using System.Reflection;
using System.IO;

namespace HelloWorld
{
    public class Container : IContainer
    {
        /// <summary>
        ///     Loads the dll using reflection.
        ///     Then runs the IWorkerRole.Start(string) methot.
        /// </summary>
        /// <param name="assemblyPath"> Dll location. </param>
        /// <returns> Whether or not Load(string) was successfull </returns>
        public string Load(string assemblyPath)
        {
            bool success = false;

            try
            {
                Assembly dll = Assembly.Load(File.ReadAllBytes(assemblyPath));
                if (dll != null)
                {
                    object obj = dll.CreateInstance("DLL1.DllHello");
                    if(obj != null)
                    {
                        MethodInfo mi = obj.GetType().GetMethod("Start");
                        mi.Invoke(obj, new object[] { Server.Port.ToString() });
                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return "Loading assebmly " + (success ? "was " : "WAS NOT ") + "successfull.";
        }

        /// <summary>
        ///     Used for fault tolerance.
        /// </summary>
        /// <returns> "running" </returns>
        public string CheckState()
        {
            return "running";
        }

        /// <summary>
        ///     Loads a C dll using DLLHelper class
        /// </summary>
        /// <returns> Whether or not Load_c() was successfull </returns>
        public string Load_c()
        {
            bool success = false;
            try
            {
                DLLHelper.dllGreeting();
                success = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return "Loading C assebmly " + (success ? "was " : "was NOT ") + "successfull.";
        }
    }
}
