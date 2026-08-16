// In-memory device store (Singleton)
class DeviceStore {
  constructor() {
    this.devices = [
      {
        id: 1,
        name: "Contoso 4K TV",
        category: "tv",
        isActive: true,
        createdAt: new Date("2026-08-14T10:00:00Z").toISOString()
      },
      {
        id: 2,
        name: "Contoso Buzdolabi",
        category: "whitegoods",
        isActive: true,
        createdAt: new Date("2026-08-14T10:15:00Z").toISOString()
      },
      {
        id: 3,
        name: "Contoso Klima",
        category: "whitegoods",
        isActive: false,
        createdAt: new Date("2026-08-14T10:30:00Z").toISOString()
      }
    ];
    this.nextId = 4;
  }

  getAll() {
    return this.devices;
  }

  getById(id) {
    return this.devices.find(d => d.id === parseInt(id));
  }

  create(deviceData) {
    // Check for duplicate name
    if (this.devices.some(d => d.name.toLowerCase() === deviceData.name.toLowerCase())) {
      throw new Error("Device with this name already exists");
    }

    const newDevice = {
      id: this.nextId++,
      name: deviceData.name,
      category: deviceData.category,
      isActive: deviceData.isActive !== false,
      createdAt: new Date().toISOString()
    };

    this.devices.push(newDevice);
    return newDevice;
  }

  update(id, deviceData) {
    const device = this.getById(id);
    if (!device) {
      throw new Error("Device not found");
    }

    // Check for duplicate name (if name is being changed)
    if (
      deviceData.name &&
      deviceData.name !== device.name &&
      this.devices.some(d => d.name.toLowerCase() === deviceData.name.toLowerCase())
    ) {
      throw new Error("Device with this name already exists");
    }

    if (deviceData.name) device.name = deviceData.name;
    if (deviceData.category) device.category = deviceData.category;
    if (deviceData.isActive !== undefined) device.isActive = deviceData.isActive;

    return device;
  }

  delete(id) {
    const index = this.devices.findIndex(d => d.id === parseInt(id));
    if (index === -1) {
      throw new Error("Device not found");
    }

    const deleted = this.devices.splice(index, 1);
    return deleted[0];
  }

  getStats() {
    const total = this.devices.length;
    const active = this.devices.filter(d => d.isActive).length;
    const byCategory = {};

    this.devices.forEach(d => {
      byCategory[d.category] = (byCategory[d.category] || 0) + 1;
    });

    return { total, active, inactive: total - active, byCategory };
  }
}

// Export singleton instance
module.exports = new DeviceStore();
