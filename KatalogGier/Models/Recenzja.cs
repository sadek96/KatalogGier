using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KatalogGier.Models
{
    public class Recenzja:BsonDocument
    {
        [BsonElement("nazwa_uzytkownika")]
        public string Nazwa_uzytkownika { get; set; }

        [BsonElement("data_wstawienia")]
        public string Data_wstawienia { get; set; }

        [BsonElement("ocena")]
        public int Ocena { get; set; }

        [BsonElement("tresc")]
        public string Tresc { get; set; }
    }
}