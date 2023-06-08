using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeknoMarket.Models
{
    public class Test
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<Adress> AdressList { get; set; }



    }

    public class Adress
    {
        public string Title { get; set; }
        public string Description { get; set; }

    }
}