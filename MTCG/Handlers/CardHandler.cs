using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.Repositories;
using MTCG.Models;
using System.Security.Cryptography.X509Certificates;
using Npgsql;
using System.Data;

namespace MTCG.Handlers
{
    public class CardHandler
    {
        public CardHandler(CardRepository repository)
        {
            this.repository = repository;
        }
        public void AddCard(card card,packages package)
        {
            repository.Add(card, package);
        }
        public void GetStackOfUser(user user)
        {
            user.stack = repository.GetStackOfUser(user);
        }
        public void GetDeck(user user)
        {
            user.deck = repository.GetDeck(user);
        }
        public void AddToDeck(user user, card card)
        {
            repository.AddToDeck(user, card);
        }
        public card GetCardById(string id)
        {
            return repository.GetCardById(id);
        }
        public void AddOwner(user user, packages package)
        {
            repository.AddOwner(user, package);
        }
        public void RemoveCard(card card)
        {
            repository.Delete(card);
        }
        public CardRepository repository { get; set; }
    }
}