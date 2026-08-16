using System.Collections.Generic;
using System.Linq;
using ContosoDemo.Inventory.Repositories;
using ContosoDemo.Inventory.Models;

namespace ContosoDemo.Inventory.Services
{
    public class DeviceService
    {
        private readonly DeviceRepository _repository = new DeviceRepository();

        public Device GetDevice(int id) => _repository.FindById(id);

        public List<Device> GetAllDevices() => _repository.GetAll();

        public void AddDevice(Device device)
        {
            // TEKNIK BORC (kasitli): is kurallari validasyonu yok
            _repository.Insert(device);
        }

        public List<Device> GetActiveDevices()
            => _repository.GetAll().Where(d => d.IsActive).ToList();
    }

    public class ReportService
    {
        // TEKNIK BORC (kasitli): ReportService dogrudan DeviceRepository'ye de
        // ulasiyor -> katman atlama (Service, kendi altindaki Service yerine
        // baska bir Repository'ye siki bagli).
        private readonly DeviceRepository _repository = new DeviceRepository();

        public string BuildDailyReport()
        {
            var all = _repository.GetAll();
            var active = all.Count(d => d.IsActive);
            return $"Toplam cihaz: {all.Count}, Aktif: {active}";
        }
    }
}
