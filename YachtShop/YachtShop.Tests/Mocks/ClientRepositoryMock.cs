using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YachtShop.Data.Repositories.Interfaces;
using YachtShop.Models;

namespace YachtShop.Tests.Mocks
{
    public class ClientRepositoryMock : IClientRepository
    {
        Dictionary<string, Client> clients = new Dictionary<string,Client>();
        public void Add(Client client)
        {
            if (clients.Count != 0)
            {
                var lastKey = clients[clients.Count.ToString()].ClientId;
                int nextKey = Int32.Parse(lastKey);
                nextKey++;
                client.ClientId = nextKey.ToString();
            }
            else
            {
                client.ClientId = "0";
            }

            clients.Add(client.ClientId, client);
        }

        public void Delete(Client client)
        {
            if(client == null)
            {
                throw new Exception();
            }
            clients.Remove(client.ClientId);
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            var list = new List<Client>();
            foreach(Client client in clients.Values)
            {
                list.Add(client);
            }

            return await Task.FromResult(list);
        }

        public async Task<Client> GetById(string id)
        {
            var client = clients.Where(e => e.Key == id)
                .Select(e => e.Value)
                .FirstOrDefault();
            return await Task.FromResult(client);
        }

        public void Update(Client client)
        {
            var id = client.ClientId;
            if(id == null)
            {
                throw new Exception();
            }
            clients.Remove(id);
            clients.Add(id, client);
        }
    }
}
