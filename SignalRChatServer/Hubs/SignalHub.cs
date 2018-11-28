using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

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
            Clients.All.broadCastMessage("[" + c.name + "]", Context.ConnectionId, "Ha ingresado a la sala");


        }

        public void Send (string userName, string message)
        {

            Clients.All.broadCastMessage(userName, Context.ConnectionId, message);

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var disUser = Clientes.Where(x => x.id == Context.ConnectionId).FirstOrDefault();
            Clientes.Remove(disUser);
            Clients.All.updateClientes(Clientes.Count(), Clientes.Select(x => x.name));
            Clients.All.broadCastMessage("["+disUser.name+"]", Context.ConnectionId, "Se ha desconectado");

            return base.OnDisconnected(stopCalled);
        }

    }


    public class Client
    {
        public string name { get; set; }
        public string id { get; set; }
    }
}