using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatServer.Hubs
{
    public class SignalHub:Hub
    {
        public static List<Client> Clientes = new List<Client>();

        
        public void Connect (string userName)
        {
            Client c = new Client(){
                name=userName,
                id=Context.ConnectionId

            };

            Clientes.Add(c);
            Clients.All.updateClientes(Clientes.Count(), Clientes.Select(x => x.name));

        }

        public void Send (string userName, string message)
        {

            Clients.All.broadCastMessage(userName, message);

        }

    }


    public class Client
    {
        public string name { get; set; }
        public string id { get; set; }
    }
}