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

        public bool CreateParty(string name)
        {
           
            try
            {
                using (var context = new WeebixDoContext())
                {
                    var distrib = context.distributors.Create();
                    distrib.lastIndex = 0;
                    context.distributors.Add(distrib);
                    context.SaveChanges();
                    
              
                    var party = context.games.Create();
                    party.createdAt = DateTime.Now;
                    party.name = name;
					party.playersInGame = 1;
                    party.ditributorId = distrib.distributorId;
					context.games.Add (party);
                    context.SaveChanges();
                    this.Clients.All.addParty(party);
					this.Clients.Caller.goToParty (party.partyId);
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

        public bool JoinParty(int partyId)
        {
			try
			{
				using (var context = new WeebixDoContext ())
				{
					var party = context.games.FirstOrDefault (p => p.partyId == partyId);
					party.playersInGame +=1;
					context.SaveChanges ();
					this.Clients.Caller.goToParty (party.partyId);
					return true;
				}
			}
			catch (Exception ex)
			{
				this.Clients.Caller.reportError ("Error : " + ex.Message);
				return false;
			}
        }

		

		public void GetAll()
		{
			using (var context = new WeebixDoContext ())
			{
				var res = context.games.ToArray ();
				this.Clients.All.allGames (res);
			}

		}


    }
}