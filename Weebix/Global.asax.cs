using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Weebix.Models;
using YamlDotNet.RepresentationModel;

namespace Weebix
{
    // Remarque : pour obtenir des instructions sur l'activation du mode classique IIS6 ou IIS7, 
    // visitez http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapHubs();
            routes.MapRoute(
                "Default", // Nom d'itinéraire
                "{controller}/{action}/{id}", // URL avec des paramètres
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Paramètres par défaut
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Utiliser LocalDB pour Entity Framework par défaut
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

			Database.SetInitializer (new DropCreateDatabaseAlways<WeebixDoContext> ());

            InitYamlData();
        }


        public void InitYamlData()
        {
            var input = new StreamReader(Path.Combine(Server.MapPath("~/yaml/"), "deck.yml"));

            // Load the stream
            var yaml = new YamlStream();
            yaml.Load(input);


            // Examine the stream
            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            foreach (var entry in mapping.Children)
            {
                Console.WriteLine(((YamlScalarNode)entry.Key).Value);
            }

            // Load all the cards in the deck
            var cards = (YamlSequenceNode)mapping.Children[new YamlScalarNode("deck")];
            foreach (YamlMappingNode card in cards)
            {

               /* var file = File( card.Single().Value.ToString());
                using (var context = new WeebixDoContext())
                {
                    var cardToAdd = context.deck.Create();

                    cardToAdd.path = ...
                    context.deck.Add(cardToAdd);
                    context.SaveChanges();


                }*/
            }
        }
    }
}