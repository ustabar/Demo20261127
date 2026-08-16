// Device routes

const express = require("express");
const deviceController = require("../controllers/deviceController");

const router = express.Router();

// Device endpoints
router.get("/devices/stats/summary", deviceController.getStats); // Must be before /:id to avoid matching :id as "stats"
router.get("/devices", deviceController.getAll);
router.get("/devices/:id", deviceController.getById);
router.post("/devices", deviceController.create);
router.put("/devices/:id", deviceController.update);
router.delete("/devices/:id", deviceController.delete);

module.exports = router;
