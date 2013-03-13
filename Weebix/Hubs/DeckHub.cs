using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Weebix.Models;

namespace Weebix
{
    public class DeckHub : Hub
    {

        public bool RemoveCard(int cardId)
        {
            try
            {
                using (var context = new WeebixDoContext())
                {
                    var card = context.deck.FirstOrDefault(c => c.cardId == cardId);
                    context.deck.Remove(card);
                    context.SaveChanges();
                    this.Clients.Caller.cardRemoved(card.cardId);
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Clients.Caller.reportError("Error : " + ex.Message);
                return false;
            }
        }



        public void GetAll()
        {
            using (var context = new WeebixDoContext())
            {
                var res = context.deck.ToArray();
                this.Clients.Caller.deckAll(res);
            }

        }


    }
}