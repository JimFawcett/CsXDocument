///////////////////////////////////////////////////////////////////////////
// XDocument-Create-XML.cs - demonstrate creating and using an XML file  //
//                           with System.Xml.Linq.XDocument              //
// ver 1.1                                                               //
// Jim Fawcett, CSE681 - Software Modeling and Analysis, Fall 2015       //
///////////////////////////////////////////////////////////////////////////
/*
 * ver 1.1 : 22 Sep 2016
 * - fixed naming error
 * ver 1.0 : 27 Sep 2025
 * - first release
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DemoExtensions;
using static System.Console;

namespace XDocument_Create_XML
{
  class Program
  {
    static void Main(string[] args)
    {
      "Demonstrating XDocument class".title('=');
      WriteLine();

      "Creating XML string using XDocument".title();
      WriteLine();

      XDocument xml = new XDocument();
      xml.Declaration = new XDeclaration("1.0", "utf-8", "yes");
     /*
      *  It is a quirk of the XDocument class that the XML declaration,
      *  a valid element, cannot be added to the XDocument's element
      *  collection.  Instead, it must be assigned to the document's
      *  Declaration property.
      */
      XComment comment = new XComment("Demonstration XML");
      xml.Add(comment);

      XElement root = new XElement("root");
      xml.Add(root);
      XElement child1 = new XElement("child1", "child1 content");
      child1.SetAttributeValue("number", "first");
      XElement child2 = new XElement("child2");
      child2.SetAttributeValue("number", "second");
      XElement grandchild21 = new XElement("grandchild21", "content of grandchild21");
      
      child2.Add(grandchild21);
      root.Add(child1);
      root.Add(child2);

      Console.Write("\n{0}\n", xml.Declaration);
      Console.Write(xml.ToString());
      WriteLine();

      "Creating XML file using XDocument".title();
      xml.Save("Test.xml");
      WriteLine();

      "Reading and displaying saved file".title();
      XDocument newDoc = XDocument.Load("Test.xml");
      Console.Write("\n{0}", newDoc.Declaration);
      Console.Write("\n{0}", newDoc.ToString());
      WriteLine();

      "Reading and displaying XML from string".title();
      XDocument newerDoc = XDocument.Parse(newDoc.ToString());
      Console.Write("\n{0}", newerDoc.Declaration);
      Console.Write("\n{0}", newerDoc.ToString());
      WriteLine();

      "Find element by TagName".title();
      IEnumerable<XElement> allElements = newerDoc.Descendants("child1");
      foreach (var elem in allElements)
        Write("\n{0}", elem.ToString());
      WriteLine();

      "Find element by attribute name".title();
      allElements = newerDoc.Root.Descendants();
      foreach (var elem in allElements)
      {
        if (elem.Attributes().Count() > 0)
          if (elem.Attribute("number") != null)
            Write("\n{0}", elem.ToString());
      }
      WriteLine();

      "Find element by attribute name/value pair".title();
      foreach (var elem in allElements)
      {
        if (elem.Attributes().Count() > 0)
          if (elem.Attribute("number").Value.Equals("second"))
            Write("\n{0}", elem.ToString());
      }
      WriteLine();

      "Find element by value - Note: you may get more than you expected".title();
      foreach (var elem in allElements)
      {
        if (elem.Value != null)
          if (elem.Value.Contains("content of"))
            Write("\n{0}", elem.ToString());
      }
      Console.Write("\n\n");
    }
  }
}
