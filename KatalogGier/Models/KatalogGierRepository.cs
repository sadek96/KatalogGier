﻿using KatalogGier.Properties;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace KatalogGier.Models
{
    public class KatalogGierRepository : IKatalogGierRepository
    {
        public IMongoDatabase Database;
        public IMongoCollection<Gra> GamesCollection;

        //Konstruktor
        public KatalogGierRepository()
        {
            //Połączenie z mongoDB - connection string: mongodb://localhost
            var mongoClient = new MongoClient(Settings.Default.KatalogGierConnectionString);

            //Wybierz bazę danych - nazwa: mydb
            Database = mongoClient.GetDatabase(Settings.Default.DB);

            //Pobierz referencję do kolekcji gier
            GamesCollection = Database.GetCollection<Gra>("Katalog_gier");

        }

        #region Test Data

        private Gra[] _testGamesData = new Gra[]
        {
            new Gra()
            {
                ID = ObjectId.GenerateNewId(),
                Tytul ="Far Cry 3",
                Gatunek = new string[]
                {
                    "Elementy RPG","FPS","Akcji"
                },
                Producent = "Ubisoft",
                Wydawca = "Ubisoft",
                Data_premiery = "29/11/2012",
                Platforma = new string[]
                {
                    "PC","PS3","X360"
                },
                Zdjecie = "FarCry3PC.jpg",
                Recenzje = new List<Recenzja>()
                {
                    new Recenzja()
                    {
                        Nazwa_uzytkownika = "Zielony Nowicjusz",
                        Data_wstawienia = DateTime.Now.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture),
                        Ocena = 9,
                        Tresc = "Lubie dżem bo smakuje jak Far Cry 3, słodka rozwałka"
                    }
                }
            },

            new Gra()
            {
                ID = ObjectId.GenerateNewId(),
                Tytul ="Mount & Blade: Warband",
                Gatunek = new string[]
                {
                    "RPG","Średniowiecze","Sandbox"
                },
                Producent = "TaleWorlds",
                Wydawca = "Paradox Interactive",
                Data_premiery = "30/03/2010",
                Platforma = new string[]
                {
                    "PC","PS4","XONE"
                },
                Zdjecie = "MountAndBladeWarbandPC.jpg"
            },

            new Gra()
            {
                ID = ObjectId.GenerateNewId(),
                Tytul ="Warcarft 3",
                Gatunek = new string[]
                {
                    "RTS","Fantasy"
                },
                Producent = "Blizzard Entertainment",
                Wydawca = "Blizzard Entertainment",
                Data_premiery = "03/07/2002",
                Platforma = new string[]
                {
                    "PC","PS1","Mac OS"
                },
                Recenzje = new List<Recenzja>()
                {
                    new Recenzja()
                    {
                        Nazwa_uzytkownika = "Patys",
                        Data_wstawienia = DateTime.Now.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture),
                        Ocena = 7,
                        Tresc = "Wspaniały RTS"
                    }
                }
            },
            new Gra()
            {
                ID = ObjectId.GenerateNewId(),
                Tytul ="Gothic",
                Gatunek = new string[]
                {
                    "RPG","Fantasy"
                },
                Producent = "Piranha Bytes",
                Wydawca = "JoWooD Entertainment",
                Data_premiery = "15/03/2001",
                Platforma = new string[]
                {
                    "PC"
                },
                Zdjecie="Gothic.jpg",
                Recenzje = new List<Recenzja>()
                {
                    new Recenzja()
                    {
                        Nazwa_uzytkownika = "Zdzisław",
                        Data_wstawienia = DateTime.Now.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture),
                        Ocena = 10,
                        Tresc = "Spełnienie marzeń"
                    }
                }
            }


        };

        #endregion

        private List<Gra> _gamesList = new List<Gra>();

        public Gra Add(Gra game)
        {
            GamesCollection.InsertOne(game);
            return game;
        }

        public bool Delete(ObjectId objectId)
        {
            GamesCollection.DeleteOne(Builders<Gra>.Filter.Eq("_id", objectId));
            return true;
        }

        public IEnumerable<Gra> GetAllGames()
        {
            var filter = Builders<Gra>.Filter.Ne<Gra>("tytul", null);

            if (GamesCollection.Count(filter) > 0)
            {
                _gamesList.Clear();
                var games = GamesCollection.AsQueryable();

                if (games.Count() > 0)
                {
                    foreach(var game in games)
                    {
                        _gamesList.Add(game);
                    }
                }
            }
            else
            {
                #region Add test data if DB is empty

                GamesCollection.DeleteMany(Builders<Gra>.Filter.Empty);
                foreach(var game in _testGamesData)
                {
                    //do listy
                    _gamesList.Add(game);

                    //oraz bazy danych
                    Add(game);
                }

                #endregion
            }

            var result = _gamesList;

            return result;
        }

        public Gra GetGameById(ObjectId id)
        {
            var filter = Builders<Gra>.Filter.Eq("_id", id);
            var game = GamesCollection.Find(filter).First();

            return game;
        }

        public bool Update(ObjectId objectId, Gra game)
        {
            var updateBuilder = Builders<Gra>.Update
                .Set("tytul", game.Tytul)
                .Set("gatunek", game.Gatunek)
                .Set("producent", game.Producent)
                .Set("wydawca", game.Wydawca)
                .Set("data_premiery", game.Data_premiery)
                .Set("platforma", game.Platforma)
                .Set("zdjecie", game.Zdjecie)
                .Set("recenzja", game.Recenzje);
            var filter = Builders<Gra>.Filter.Eq("_id", objectId);
            GamesCollection.UpdateOne(filter, updateBuilder);

            return true;
        }

        public Gra GetGameByTitle(string Title)
        {
            var filter = Builders<Gra>.Filter.Eq("tytul", Title);

            Gra g = GamesCollection.Find(filter).First();

            return g;
        }

        public List<Gra> SearchForGames(string text)
        {
            GetAllGames();

            List<Gra> list = new List<Gra>();

            if(text=="" || text == null)
            {
                var filter = Builders<Gra>.Filter.Empty;
                list = GamesCollection.Find(filter).ToList();

                return list;
            }
            else
            {
                var filter = Builders<Gra>.Filter.Text(text);

                list = GamesCollection.Find(filter).ToList();
                return list;
            }
           
            
        }
    }
}