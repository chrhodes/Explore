using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Models
{
    public class TimeCardRepository
    {
        public IEnumerable<TimeCard> GetAll()
        {
            return _cards.Select(kv => kv.Value);
        }

        public TimeCard GetById(Guid id)
        {
            return _cards[id];
        }

        public void Add(TimeCard card)
        {
            _cards.Add(card.Id, card);
        }

        public void Delete(Guid id)
        {
            _cards.Remove(id);
        }

        private static Dictionary<Guid, TimeCard> _cards = new Dictionary<Guid, TimeCard>();
    }
}