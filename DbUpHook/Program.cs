using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace DbUpHook
{
    class Program
    {
        static int Main(string[] args)
        {
            var xml = new XmlDocument();
            xml.Load(Settings.Path);
            var contents = xml.GetElementsByTagName("Content");

            List<int> numbers = new List<int>();
            foreach (XmlNode content in contents)
            {
                if (content.Attributes != null)
                {
                    var include = content.Attributes["Include"];
                    if (include != null)
                    {
                        var value = include.Value;
                        if (value.StartsWith("Scripts") && value.EndsWith(".sql"))
                        {
                            var filename = include.Value.Split('\\').Last();
                            var numberString = filename.Split('-').FirstOrDefault();
                            if (numberString != null)
                            {
                                var success = Int32.TryParse(numberString, out var number);
                                if (success)
                                {
                                    if (numbers.Contains(number) && !Settings.Ignore.Contains(number))
                                    {
                                        return number;
                                    }
                                    numbers.Add(number);
                                }
                            }
                        }
                    }
                }
            }

            return 0;
        }
    }
}