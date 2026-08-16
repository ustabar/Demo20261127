// Device model and validation

const VALID_CATEGORIES = ["tv", "whitegoods", "accessory"];

function validateDevice(deviceData) {
  const errors = [];

  // Validate name
  if (!deviceData.name || typeof deviceData.name !== "string") {
    errors.push("Name is required and must be a string");
  } else if (deviceData.name.trim().length < 3) {
    errors.push("Name must be at least 3 characters");
  }

  // Validate category
  if (!deviceData.category || !VALID_CATEGORIES.includes(deviceData.category)) {
    errors.push(`Category must be one of: ${VALID_CATEGORIES.join(", ")}`);
  }

  // Validate isActive (optional, defaults to true)
  if (deviceData.isActive !== undefined && typeof deviceData.isActive !== "boolean") {
    errors.push("isActive must be a boolean");
  }

  return errors.length > 0 ? errors.join("; ") : null;
}

module.exports = {
  validateDevice,
  VALID_CATEGORIES
};
