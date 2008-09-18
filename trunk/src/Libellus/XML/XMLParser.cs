using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using Libellus.XML;
using System.Windows.Forms;

namespace Libellus.Utilities
{
    class XMLParser
    {
        public static Element ParseXML(Stream xmlStream)
        {
            StreamReader textReader = new StreamReader(xmlStream);
            string responseString = "";
            while (!textReader.EndOfStream)
                responseString += textReader.ReadLine();

            StringReader stringReader = new StringReader(responseString);
            
            XmlTextReader reader = new XmlTextReader(stringReader);

#if DEBUG
            Console.Out.WriteLine("Original XML:");
            responseString = responseString.Replace(">", ">\n");
            Console.Out.WriteLine(responseString);
            Console.Out.WriteLine("");
#endif
            Element rootNode = new Element();
            rootNode.IsRootNode = true;
            //int lastDepth = -1;
            try
            {
                Stack<Element> stack = new Stack<Element>();
                
                stack.Push(rootNode);

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Element newElement = new Element();
                            newElement.Name = reader.Name;
                            newElement.Parent = stack.Peek();
                            stack.Peek().AddChildElement(newElement);
                            while (reader.MoveToNextAttribute())
                            {
                                Libellus.XML.Attribute a = new Libellus.XML.Attribute();
                                a.Name = reader.Name;
                                a.Value = reader.ReadContentAsString();
                                newElement.Attributes.Add(a);
                            }
                            if(!newElement.Name.Equals("Price"))
                                stack.Push(newElement);

                            break;

                        case XmlNodeType.Text:
                            stack.Peek().Value = reader.Value;
                            break;

                        case XmlNodeType.EndElement:
                            stack.Pop();
                            break;

                       
                    }
                }

                
            }
            catch (Exception e1)
            {
                ExceptionHandler.HandleException(e1);
            }

            return rootNode;
        }

        public static void GetParseTreeInfo(Element e, ref string msg, ref int level)
        {

            if (e == null) return;

            string tabs = "";


            for (int i = 0; i < level; i++)
                tabs += "\t";

            msg += tabs;

            msg += "Element " + e.Name + ": " + e.Value + "\n";
            foreach (Libellus.XML.Attribute a in e.Attributes)
            {
                msg += tabs + "\tAttribute " + a.Name + ": " + a.Value + "\n";
            }


            if (e.Children.Count == 0)
            {
                return;
            }
            else
            {
                level++;
                foreach(Element child in e.Children)
                {
                    GetParseTreeInfo(child, ref msg, ref level);
                }
                level--;
            }
            
        }
    }
}
