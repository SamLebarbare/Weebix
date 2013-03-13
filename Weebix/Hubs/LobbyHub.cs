using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Weebix.Models;

namespace Weebix.Hubs
{
    public class LobbyHub : Hub
    {

        public bool CreateParty(Party newParty)
        {
           
            try
            {
                using (var context = new WeebixDoContext())
                {
                    var party = context.games.Create();
                    party.createdAt = DateTime.Now;
                    party.name = newParty.name;
                    context.SaveChanges();
                    this.Clients.Caller.addParty(party);
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Clients.Caller.reportError("Error : " + ex.Message);
                return false;
            }
            
        }

        public bool RemoveParty(int partyId)
        {

            try
            {
                using (var context = new WeebixDoContext())
                {
                    var party = context.games.FirstOrDefault(p => p.partyId == partyId);
                    context.games.Remove(party);
                    context.SaveChanges();
                    this.Clients.Caller.removeParty(party);
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Clients.Caller.reportError("Error : " + ex.Message);
                return false;
            }

        }

        public void JoinParty()
        {

        }

        public void LeaveParty()
        {

        }

        public void GetPartyInfo()
        {

        }


    }
}