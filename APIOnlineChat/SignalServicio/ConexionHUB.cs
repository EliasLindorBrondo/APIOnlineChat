using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace APIOnlineChat.SignalServicio
{
    public class ConexionHUB : Hub
    {

        private static Dictionary<int, string> deviceConnections;
        private static Dictionary<string, int> connectionDevices;


        public ConexionHUB()
        {
            deviceConnections = deviceConnections ?? new Dictionary<int, string>();
            connectionDevices = connectionDevices ?? new Dictionary<string, int>();
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            int? deviceId = connectionDevices.ContainsKey(Context.ConnectionId) ?
                            (int?)connectionDevices[Context.ConnectionId] :
                            null;

            if (deviceId.HasValue)
            {
                deviceConnections.Remove(deviceId.Value);
                connectionDevices.Remove(Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        //Conexion ID --------------------------------
        [HubMethodName("ConexionId")]
        public Task ConexcionId(Dispositivo info)
        {
            deviceConnections.AddOrUpdate(info.Id, Context.ConnectionId);
            connectionDevices.AddOrUpdate(Context.ConnectionId, info.Id);

            return Task.CompletedTask;
        }

        //Enviar mensajes a todos---------------------------------
        [HubMethodName("SendMessage")]
        public async Task SendMessage(string userId, string message)
        {
            await Clients.Others.SendAsync("ReceivedMessage", userId, message);
        }

        //Enviar Mensaje a un Dispositivo--------------------------------
        [HubMethodName("SendMessageToDevice")]
        public async Task SendMessageToDevice(MessageT item)
        {
            if (deviceConnections.ContainsKey(item.DestinoId))
            {
                await Clients.Client(deviceConnections[item.DestinoId]).SendAsync("NewMessage", item);
            }
        }
    }
}
