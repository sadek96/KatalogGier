using KatalogGier.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatalogGier.Models
{
    interface IKatalogGierRepository
    {
        IEnumerable<Gra> GetAllGames();
        Gra GetGameById(ObjectId id);
        Gra Add(Gra game);
        bool Update(ObjectId objectId, Gra game);
        bool Delete(ObjectId objectId);
        Gra GetGameByTitle(string title);
        List<Gra> SearchForGames(string text);
    }
}
