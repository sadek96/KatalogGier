using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KatalogGier.Models
{
    public class Recenzja
    {
        [BsonElement("nazwa_uzytkownika")]
        public string Nazwa_uzytkownika { get; set; }

        [BsonElement("data_wstawienia")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Data_wstawienia { get; set; }

        [BsonElement("ocena")]
        public int Ocena { get; set; }

        [BsonElement("tresc")]
        public string Tresc { get; set; }
    }
}