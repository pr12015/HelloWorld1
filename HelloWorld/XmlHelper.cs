using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace HelloWorld
{
    public class XmlHelper
    {
        private string fileName;
        private DirectoryInfo dirInfo;

        public XmlHelper()
        {
            string uri = "";
            using (XmlReader reader = XmlReader.Create(@"C:\Users\stefan\Desktop\HelloWorld\HelloWorld\package.xml"))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "location")
                    {
                        uri = reader.ReadElementContentAsString();
                        break;
                    }
                }  
            }

            dirInfo = new DirectoryInfo(uri);

            try
            {
                if (!dirInfo.Exists)              
                    dirInfo.Create();                
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not create directory. Reason: {0}", e.ToString());
            }
        }

       // DirectoryInfo DirInfo { get { return dirInfo; } }

        private async Task Read(string inputUri)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            using (XmlReader reader = XmlReader.Create(inputUri, settings)) 
            {
                while(await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.WriteLine("Start Element {0}", reader.Name);
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine("Text node: {0}", await reader.GetValueAsync());
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine("End element {0}", reader.Name);
                            break;
                        default:
                            Console.WriteLine("Other node {0} whith value {1}", reader.NodeType, reader.Value);
                            break;
                    }
                }
            }
        }

        public async Task AsyncRead()
        {
            while (true)
            {

                FileInfo[] files = dirInfo.GetFiles("*.xml");
                if (files.Length == 1)
                {
                    if (files[0].Name != fileName)
                        fileName = files[0].Name;

                    Task readTask = Read(files[0].FullName);

                    await readTask;
                }
                else if(files.Length > 1)
                {
                    Console.WriteLine("ERROR: Only 1 config file allowed.");
                    Console.WriteLine("Choose one.");
                }
                else
                {
                    Console.WriteLine("WARNING: Config file could not be found.");
                    Console.WriteLine("Add a config file to {0}", dirInfo.FullName); // dirInfo.FullName should be replaced with configurable xml element value!
                }
                await Task.Delay(1000);
            }
        }
    }
}
