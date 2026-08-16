// Device controller - business logic

const store = require("../data/store");
const { validateDevice } = require("../models/Device");

module.exports = {
  // GET /devices - Get all devices (with optional isActive filter)
  getAll: (req, res, next) => {
    try {
      const { isActive } = req.query;
      let devices = store.getAll();

      if (isActive !== undefined) {
        const filterActive = isActive.toLowerCase() === "true";
        devices = devices.filter(d => d.isActive === filterActive);
      }

      res.json({
        success: true,
        count: devices.length,
        data: devices
      });
    } catch (err) {
      next(err);
    }
  },

  // GET /devices/:id - Get single device by ID
  getById: (req, res, next) => {
    try {
      const device = store.getById(req.params.id);

      if (!device) {
        return res.status(404).json({
          success: false,
          error: "Device not found"
        });
      }

      res.json({
        success: true,
        data: device
      });
    } catch (err) {
      next(err);
    }
  },

  // POST /devices - Create new device
  create: (req, res, next) => {
    try {
      const { name, category, isActive } = req.body;

      // Validate input
      const validationError = validateDevice({ name, category, isActive });
      if (validationError) {
        return res.status(400).json({
          success: false,
          error: validationError
        });
      }

      const newDevice = store.create({ name, category, isActive });

      res.status(201).json({
        success: true,
        message: "Device created successfully",
        data: newDevice
      });
    } catch (err) {
      if (err.message.includes("already exists")) {
        return res.status(409).json({
          success: false,
          error: err.message
        });
      }
      next(err);
    }
  },

  // PUT /devices/:id - Update device
  update: (req, res, next) => {
    try {
      const { name, category, isActive } = req.body;

      // Validate if any field is provided
      if (name || category || isActive !== undefined) {
        const validationError = validateDevice({
          name: name || "",
          category: category || "tv",
          isActive
        });

        if (validationError && (name || category || isActive !== undefined)) {
          return res.status(400).json({
            success: false,
            error: validationError
          });
        }
      }

      const updatedDevice = store.update(req.params.id, { name, category, isActive });

      res.json({
        success: true,
        message: "Device updated successfully",
        data: updatedDevice
      });
    } catch (err) {
      if (err.message.includes("not found")) {
        return res.status(404).json({
          success: false,
          error: err.message
        });
      }
      if (err.message.includes("already exists")) {
        return res.status(409).json({
          success: false,
          error: err.message
        });
      }
      next(err);
    }
  },

  // DELETE /devices/:id - Delete device
  delete: (req, res, next) => {
    try {
      const deletedDevice = store.delete(req.params.id);

      res.json({
        success: true,
        message: "Device deleted successfully",
        data: deletedDevice
      });
    } catch (err) {
      if (err.message.includes("not found")) {
        return res.status(404).json({
          success: false,
          error: err.message
        });
      }
      next(err);
    }
  },

  // GET /devices/stats/summary - Get device statistics
  getStats: (req, res, next) => {
    try {
      const stats = store.getStats();

      res.json({
        success: true,
        data: stats
      });
    } catch (err) {
      next(err);
    }
  }
};
