using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace KatalogGier.Models
{
    public class Gra
    {
        
            [BsonId]
            public ObjectId ID { get; set; }

            [BsonElement("tytul")]
            public string Tytul { get; set; }

            [BsonElement("gatunek")]
            public string[] Gatunek { get; set; }

            [BsonElement("producent")]
            public string Producent { get; set; }

            [BsonElement("wydawca")]
            public string Wydawca { get; set; }

            [BsonElement("data_premiery")]
            public string Data_premiery { get; set; }

            [BsonElement("platforma")]
            public string[] Platforma { get; set; }

            [BsonElement("recenzja")]
            public List<Recenzja> Recenzje { get; set; }

            [BsonElement("zdjecie")]
            public string Zdjecie { get; set; }
        
    }
}