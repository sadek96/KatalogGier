using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KatalogGier.Models
{
    public class Recenzja
    {
        [BsonElement("nazwa_uzytkownika")]
        public string Nazwa_uzytkownika { get; set; }

        [BsonElement("data_wstawienia")]
        [BsonRequired]
        public string Data_wstawienia { get; set; }

        [BsonElement("ocena")]
        [BsonRequired]
        public int Ocena { get; set; }

        [BsonElement("tresc")]
        public string Tresc { get; set; }
    }

}

