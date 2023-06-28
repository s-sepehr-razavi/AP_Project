using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class Post
    {
        public string recieverAddress {  get; set; }
        public string senderAddress { get; set; }
        public Content content { get; set; }
        public bool expensive { get; set; }
        public double weight { get; set; }
        public string phonenumber { get; set; }
        public bool express { get; set; }
        public int id { get; set; }
        public string senderID { get; set; }

        static List<Post> posts { get; set; } = new List<Post>();

        public Post(string recieverAddress, string senderAddress, Content content, bool expensive, double weight, string phonenumber, bool express, int id)
        {
            this.recieverAddress = recieverAddress;
            this.senderAddress = senderAddress;
            this.content = content;
            this.expensive = expensive;
            this.weight = weight;
            this.phonenumber = phonenumber;
            this.express = express;
            posts.Add(this);
            this.id = posts.Count;
        }

        public double calculatePrice()
        {
            double price = 10000;

            if (content == Content.Document)
            {
                price *= 1.5;
            }
            else if (content == Content.Fragile)
            {
                price *= 2;
            }

            if (expensive)
            {
                price *= 2;
            }

            if (express)
            {
                price *= 1.5;
            }


            return price * Math.Pow(1.2,(int)((int)(weight - 0.5) / 0.5));
        }

        static public List<Post> searchByPrice(double min, double max)
        {
            List<Post> result = new List<Post>();
            foreach (var item in posts)
            {
                double price = item.calculatePrice();
                if (price >= min && price <= max)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        static public List<Post> searchByID(string id)
        {
            List<Post> result = new List<Post>();
            foreach (var item in posts)
            {                
                if (item.senderID == id)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        static public List<Post> searchByWeight(double min, double max)
        {
            List<Post> result = new List<Post>();
            foreach (var item in posts)
            {                
                if (item.weight >= min && item.weight <= max)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        static public List<Post> searchByPostType(bool exp)
        {
            List<Post> result = new List<Post>();
            foreach (var item in posts)
            {
                if (item.express == exp)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        static public List<Post> searchByContent(Content content)
        {
            List<Post> result = new List<Post>();
            foreach (var item in posts)
            {
                if (item.content == content)
                {
                    result.Add(item);
                }
            }

            return result;
        }

    }

    enum Content
    {
        Object,
        Document,
        Fragile
    }

    
}
