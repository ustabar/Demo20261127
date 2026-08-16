// Main Express server

const express = require("express");
const path = require("path");
const deviceRoutes = require("./routes/devices");
const errorHandler = require("./middleware/errorHandler");

const app = express();
const PORT = process.env.PORT || 3000;

// Middleware
app.use(express.json());
app.use(express.static(path.join(__dirname, "../public")));

// Routes
app.use("/api", deviceRoutes);

// Health check
app.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "../public/index.html"));
});

// 404 handler
app.use((req, res) => {
  res.status(404).json({
    success: false,
    error: "Endpoint not found"
  });
});

// Error handling middleware (must be last)
app.use(errorHandler);

// Start server
app.listen(PORT, () => {
  console.log(`🚀 Device Inventory API running on http://localhost:${PORT}`);
  console.log(`📌 Endpoints:`);
  console.log(`   GET    http://localhost:${PORT}/api/devices`);
  console.log(`   GET    http://localhost:${PORT}/api/devices?isActive=true`);
  console.log(`   GET    http://localhost:${PORT}/api/devices/:id`);
  console.log(`   POST   http://localhost:${PORT}/api/devices`);
  console.log(`   PUT    http://localhost:${PORT}/api/devices/:id`);
  console.log(`   DELETE http://localhost:${PORT}/api/devices/:id`);
  console.log(`   GET    http://localhost:${PORT}/api/devices/stats/summary`);
});
