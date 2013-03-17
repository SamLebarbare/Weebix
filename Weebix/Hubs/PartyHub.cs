using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Weebix.Models;

namespace Weebix.Hubs
{
    public class PartyHub : Hub
    {


		public void StartGame(int partyId)
		{
			this.Clients.All.startGame (partyId);

		}
		

		public void ProposeTheme(int partyId,string theme)
		{
			this.Clients.All.setTheme (partyId,theme);
		}

        public void GetCards(int partyId,int playerId)
        {
            try
            {
                using (var context = new WeebixDoContext())
                {
                    var party = context.games.FirstOrDefault(p => p.partyId == partyId);
                    var distrib = context.distributors.FirstOrDefault(d => d.distributorId == party.ditributorId);
                    var cards = context.deck.ToArray().Skip(distrib.lastIndex).Take(3);

                    distrib.lastIndex += 3;
                    context.SaveChanges();

                    var nextPlayerId = playerId + 1;

                    this.Clients.Caller.giveCards(partyId, cards);

                    this.Clients.All.changePlayer(partyId, nextPlayerId);

                }
            }
            catch (Exception ex)
            {
                this.Clients.Caller.reportError("Error : " + ex.Message);
            }
        }

		public void GetPlayers(int partyId)
		{
			try
			{
				using (var context = new WeebixDoContext ())
				{
					var party = context.games.FirstOrDefault (p => p.partyId == partyId);
					this.Clients.All.setPlayers(partyId,party.playersInGame);
					
				}
			}
			catch (Exception ex)
			{
				this.Clients.Caller.reportError ("Error : " + ex.Message);
			}
		}

		public bool LeaveParty(int partyId)
		{
			try
			{
				using (var context = new WeebixDoContext ())
				{
					var party = context.games.FirstOrDefault (p => p.partyId == partyId);
					party.playersInGame -=1;
					context.SaveChanges ();
					this.Clients.All.setPlayers (partyId,party.playersInGame);
					this.Clients.Caller.goToLobby (partyId);
					return true;
				}
			}
			catch (Exception ex)
			{
				this.Clients.Caller.reportError ("Error : " + ex.Message);
				return false;
			}
		}

		public bool FlushParty(int partyId)
		{
			try
			{
				using (var context = new WeebixDoContext ())
				{
					var party = context.games.FirstOrDefault (p => p.partyId == partyId);
                    var distrib = context.distributors.FirstOrDefault(d => d.distributorId == party.ditributorId);
                    context.distributors.Remove(distrib);
					context.games.Remove (party);
					context.SaveChanges ();
					
					this.Clients.All.goToLobby (partyId);
					return true;
				}
			}
			catch (Exception ex)
			{
				this.Clients.Caller.reportError ("Error : " + ex.Message);
				return false;
			}
		}


    }
}