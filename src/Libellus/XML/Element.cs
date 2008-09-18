using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Libellus.XML
{
    public class Element
    {
        private string _value = "";
        private string _name = "";
        private bool _root = false;
        private List<Attribute> _attributes = new List<Attribute>();
        private List<Element> _children = new List<Element>();
        private Element _parent = null;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public bool IsRootNode
        {
            get { return _root; }
            set 
            { 
                _root = value;
                if (_root == true)
                {
                    _name = "/";
                    _value = "Fear me, for I am root!";
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public List<Attribute> Attributes
        {
            get { return _attributes; }
        }

        public List<Element> Children
        {
            get { return _children; }
        }

        public string GetAttribute(string name)
        {
            string result = "";
            foreach (Attribute a in _attributes)
            {
                if (a.Name.Equals(name))
                    result = a.Value;
            }

            return result;
        }

        public Element Parent
        {
            get 
            {
                if (_root == true)
                    return null;
                else
                    return _parent; 
            }
            set { _parent = value; }
        }
        /*
        public List<Element> GetChildElement(string name)
        {
            List<Element> l = new List<Element>();
            foreach(Element e in this.Children)
            {
                if (e.Name.Equals(name))
                    l.Add(e);
            }

            return l;
        }
        */
        public void AddChildElement(Element e)
        {
            this.Children.Add(e);
        }

        public List<Element> FindChildElements(string name)
        {
            List<Element> list = new List<Element>();
            FindChildElementsHelper(list, this, name);
            return list;
        }

        private void FindChildElementsHelper(List<Element> list, Element e, string name)
        {
            if (e.Children.Count == 0)
                return;

            foreach (Element child in e.Children)
            {
                if (child.Name.Equals(name))
                {
                    list.Add(child);
                }

                FindChildElementsHelper(list, child, name);
            }
        }

        public Element GetFirstElementOf(string name)
        {
            List<Element> l = this.FindChildElements(name);
            if (l.Count < 1)
                return new Element();
            else
                return l[0];
        }
        
    }
}
