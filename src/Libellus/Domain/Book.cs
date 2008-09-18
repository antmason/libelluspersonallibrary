using System;
using System.Collections.Generic;
using System.Text;
using Libellus.Utilities;
using Libellus.XML;
using System.Data;
using Libellus.DataAccess;

namespace Libellus.Domain
{
    public class Book : BaseDO
    {
        #region Private Data
        private string _id;
        private string _shortTitle;
        private string _longTitle;
        private string _dewey;
        private string _deweyNormalized;
        private string _lcc;
        private string _physDesc;
        private string _edition;
        private string _language;
        private string _notes;
        private string _awards;
        private string _summary;
        private string _dateAdded;
        private string _isbn;
        private string _publisherInfo;
        private string _publisherId;
        private string _urls;
        private string _newPrice;
        private string _usedPrice;
        private string _pricePaid;
        private List<Person> _authors = new List<Person>();
        private List<Person> _editors = new List<Person>();
        private List<Subject> _subjects = new List<Subject>();
        #endregion

        #region Properties
        public List<Person> Authors
        {
            get { return _authors; }
        }

        public List<Person> Editors
        {
            get { return _editors; }
        }

        public string Urls
        {
            get { return CheckNull(_urls); }
            set { _urls = value; }
        }

        public List<Subject> Subjects
        {
            get { return _subjects; }
        }

        public string PublisherInfo
        {
            get { return CheckNull(_publisherInfo); }
            set { _publisherInfo = value; }
        }

        public string PublisherId
        {
            get { return CheckNull(_publisherId); }
            set { _publisherId = value; }
        }

        public string LibraryOfCongress
        {
            get { return CheckNull(_lcc); }
            set { _lcc = value; }
        }

        public string PhysicalDescription
        {
            get { return CheckNull(_physDesc); }
            set { _physDesc = value; }
        }

        public string Edition
        {
            get { return CheckNull(_edition); }
            set { _edition = value; }
        }

        public string Language
        {
            get { return CheckNull(_language); }
            set { _language = value; }
        }

        public string ShortTitle
        {
            get { return CheckNull(_shortTitle); }
            set { _shortTitle = value; }
        }

        public string LongTitle
        {
            get { return CheckNull(_longTitle); }
            set { _longTitle = value; }
        }

        public string Dewey
        {
            get { return CheckNull(_dewey); }
            set { _dewey = value; }
        }

        public string DeweyNormalized
        {
            get { return CheckNull(_deweyNormalized); }
            set { _deweyNormalized = value; }
        }

        public string Id
        {
            get { return CheckNull(_id); }
            set { _id = value; }
        }

        public string Notes
        {
            get { return CheckNull(_notes); }
            set { _notes = value; }
        }

        public string Awards
        {
            get { return CheckNull(_awards); }
            set { _awards = value; }
        }

        public string Summary
        {
            get { return CheckNull(_summary); }
            set { _summary = value; }
        }

        public string DateAdded
        {
            get { return CheckNull(_dateAdded); }
            set { _dateAdded = value; }
        }

        public string ISBN
        {
            get { return CheckNull(_isbn); }
            set { _isbn = value; }
        }

        public string NewPrice
        {
            get { return CheckNull(_newPrice); }
            set { _newPrice = value; }
        }

        public string UsedPrice
        {
            get { return CheckNull(_usedPrice); }
            set { _usedPrice = value; }
        }

        public string PricePaid
        {
            get { return CheckNull(_pricePaid); }
            set { _pricePaid = value; }
        }
        #endregion

        #region Utility Functions
        /// <summary>
        /// Receives a HashTable of Element objects that is received
        /// from the XMLParser class, breaks down the Elements that are relevant,
        /// and fills necessary text boxes
        /// </summary>
        /// <param name="table"></param>
        public Constants.XMLResultReturnValue FillFromXMLResults(Element element)
        {
            Element bookListElement = element.GetFirstElementOf("BookList");
            Element bookDataElement = element.GetFirstElementOf("BookData");
            Element titleElement = element.GetFirstElementOf("Title");
            Element titleLongElement = element.GetFirstElementOf("TitleLong");
            Element authorsTextElement = element.GetFirstElementOf("Authors");
            Element authorElement = element.GetFirstElementOf("AuthorsText");
            Element publisherTextElement = element.GetFirstElementOf("PublisherText");
            Element detailsElement = element.GetFirstElementOf("Details");
            Element summaryElement = element.GetFirstElementOf("Summary");
            Element notesElement = element.GetFirstElementOf("Notes");
            Element awardsTextElement = element.GetFirstElementOf("AwardsText");
            Element urlsTextElement = element.GetFirstElementOf("UrlsText");
            Element editorsElement = element.GetFirstElementOf("EditorsText");
            Element subjectsElement = element.GetFirstElementOf("Subjects");
            Element pricesElement = element.GetFirstElementOf("Prices");

            if (bookListElement.GetAttribute("total_results").Equals("0"))
                return Constants.XMLResultReturnValue.NOT_FOUND; 
           
            this.Id = bookDataElement.GetAttribute("book_id");
            this.ISBN = bookDataElement.GetAttribute("isbn");
            this.ShortTitle = titleElement.Value;
            this.LongTitle = titleLongElement.Value;
            this.PublisherInfo = publisherTextElement.Value;
            this.PublisherId = publisherTextElement.GetAttribute("publisher_id");
            this.PhysicalDescription = detailsElement.GetAttribute("physical_description_text");
            this.Language = detailsElement.GetAttribute("language");
            this.Dewey = detailsElement.GetAttribute("dewey_decimal");
            this.DeweyNormalized = detailsElement.GetAttribute("dewey_decimal_normalized");
            this.Edition = detailsElement.GetAttribute("edition_info");
            this.LibraryOfCongress = detailsElement.GetAttribute("lcc_number");
            this.Summary = summaryElement.Value;
            this.Notes = notesElement.Value;
            //do subject info
            this.Urls = urlsTextElement.Value;
            this.Awards = awardsTextElement.Value;

            //if there isn't any person elements, we do the best we can with what we have
            if (authorsTextElement.Children.Count == 0 && !authorElement.Value.Equals(""))
            {
                Person p = new Person();
                p.FirstName = Person.GetFirstNameFromPersonValue(authorElement.Value);
                p.LastName = Person.GetLastNameFromPersonValue(authorElement.Value);
                p.Id = Utils.CreateIdFromString(authorElement.Value);
                this.Authors.Add(p);
            }
            else
            {
                foreach (Element child in authorsTextElement.FindChildElements("Person"))
                {
                    Person p = new Person();
                    p.FillFromElement(child);
                    this.Authors.Add(p);
                }
            }

            if (editorsElement.Children.Count == 0 && !editorsElement.Value.Equals(""))
            {
                Person p = new Person();
                p.FirstName = Person.GetFirstNameFromPersonValue(editorsElement.Value);
                p.LastName = Person.GetLastNameFromPersonValue(editorsElement.Value);
                p.Id = Utils.CreateIdFromString(editorsElement.Value);
                this.Editors.Add(p);
            }
            else
            {
                foreach (Element child in editorsElement.FindChildElements("Person"))
                {
                    Person p = new Person();
                    p.FillFromElement(child);
                    this.Editors.Add(p);
                }
            }

            foreach (Element subject in subjectsElement.FindChildElements("Subject"))
            {
                Subject s = new Subject();
                s.Id = subject.GetAttribute("subject_id");
                s.Name = subject.Value;
                this.Subjects.Add(s);
            }

            int newCount = 0;
            int usedCount = 0;
            double newTotal = 0.0;
            double usedTotal = 0.0;
            foreach (Element price in pricesElement.FindChildElements("Price"))
            {
                string val = price.GetAttribute("price");
                string isNew = price.GetAttribute("is_new");
                if(isNew.Equals("1"))
                {
                    try
                    {
                        newTotal += Double.Parse(val);
                        newCount++;
                    }
                    catch(Exception e)
                    {
                    	Log.Instance.Out(e);
                    }
                }
                else if (isNew.Equals("0"))
                {
                    try
                    {
                        usedTotal += Double.Parse(val);
                        usedCount++;
                    }
                    catch (Exception e)
                    {
                    	Log.Instance.Out(e);
                    }
                }
            }
            double newAvg = newTotal / newCount;
            double usedAvg = usedTotal / usedCount;
          
            this.NewPrice = newAvg.ToString("C");
            this.UsedPrice = usedAvg.ToString("C");

            if (this.NewPrice.Contains("NaN"))
                this.NewPrice = "";

            if (this.UsedPrice.Contains("NaN"))
                this.UsedPrice = "";

            return Constants.XMLResultReturnValue.SUCCESS;
        }


        #endregion
    }
}
