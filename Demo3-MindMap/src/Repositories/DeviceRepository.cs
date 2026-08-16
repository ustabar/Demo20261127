using System.Collections.Generic;
using System.Linq;
using ContosoDemo.Inventory.Models;

namespace ContosoDemo.Inventory.Repositories
{
    // TEKNIK BORC (kasitli): kalici depolama yerine bellek-ici statik liste,
    // thread-safe degil, ayni repository her serviste ayri ornekleniyor.
    public class DeviceRepository
    {
        private static readonly List<Device> _store = new List<Device>
        {
            new Device { Id = 1, Name = "Contoso 4K TV", Category = "tv", IsActive = true },
            new Device { Id = 2, Name = "Contoso Buzdolabi", Category = "whitegoods", IsActive = true },
            new Device { Id = 3, Name = "Contoso Klima", Category = "whitegoods", IsActive = false }
        };

        public Device FindById(int id) => _store.FirstOrDefault(d => d.Id == id);

        public List<Device> GetAll() => _store;

        public void Insert(Device device)
        {
            // TEKNIK BORC (kasitli): Id cakismasi/duplicate kontrolu yok
            _store.Add(device);
        }
    }
}
