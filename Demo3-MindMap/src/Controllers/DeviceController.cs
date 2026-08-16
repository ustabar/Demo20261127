using System.Collections.Generic;
using ContosoDemo.Inventory.Services;
using ContosoDemo.Inventory.Models;

namespace ContosoDemo.Inventory.Controllers
{
    // DEMO #3 - MindMap ile Proje Analizi (M17)
    // Bu kucuk katmanli uygulama, Copilot'a mimari cikartma ve teknik borc
    // tespiti yaptirmak icin ornek olarak hazirlanmistir.
    // Katmanlar: Controllers -> Services -> Repositories -> Models
    public class DeviceController
    {
        private readonly DeviceService _service;
        private readonly ReportService _reportService;

        public DeviceController()
        {
            // TEKNIK BORC (kasitli): DI yok, bagimliliklar elle new'leniyor -> yuksek coupling
            _service = new DeviceService();
            _reportService = new ReportService();
        }

        public Device GetById(int id) => _service.GetDevice(id);

        public List<Device> List() => _service.GetAllDevices();

        public void Add(Device device) => _service.AddDevice(device);

        public string DailyReport() => _reportService.BuildDailyReport();
    }
}
