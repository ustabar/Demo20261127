import React, { useState } from "react";

/**
 * DEMO #4 - UI TEST URETIMI (M18)
 * ---------------------------------------------------------------------------
 * Bu bilesen, Copilot'a UI test taslagi urettirmek icin ornek olarak
 * hazirlanmistir. Egitmen Chat'e su prompt'u verir:
 *
 *   "Bu React bileseni icin React Testing Library + Playwright ile UI testleri
 *    uret. Su senaryolari kapsa: baslangicta miktar 1 gosterilir; + butonu
 *    miktari artirir; - butonu 1'in altina inmez; 'Sepete Ekle' dogru miktarla
 *    onAddToCart callback'ini cagirir; devre disi cihaz icin buton pasiftir."
 *
 * Beklenen test senaryolari (egitmen notu):
 *   - render: cihaz adi ve fiyati gorunur
 *   - miktar baslangicta 1
 *   - "+" tiklandiginda miktar artar
 *   - "-" tiklandiginda 1'in altina inmez
 *   - IsActive=false ise "Sepete Ekle" disabled
 *   - "Sepete Ekle" onAddToCart(device.id, quantity) cagirir
 */

export interface Device {
  id: number;
  name: string;
  price: number;
  isActive: boolean;
}

interface DeviceCardProps {
  device: Device;
  onAddToCart: (deviceId: number, quantity: number) => void;
}

export const DeviceCard: React.FC<DeviceCardProps> = ({ device, onAddToCart }) => {
  const [quantity, setQuantity] = useState(1);

  const decrease = () => setQuantity((q) => Math.max(1, q - 1));
  const increase = () => setQuantity((q) => q + 1);

  return (
    <div className="device-card" data-testid="device-card">
      <h3 data-testid="device-name">{device.name}</h3>
      <p data-testid="device-price">{device.price.toLocaleString("tr-TR")} ₺</p>

      <div className="qty">
        <button aria-label="azalt" onClick={decrease}>−</button>
        <span data-testid="qty-value">{quantity}</span>
        <button aria-label="artir" onClick={increase}>+</button>
      </div>

      <button
        data-testid="add-to-cart"
        disabled={!device.isActive}
        onClick={() => onAddToCart(device.id, quantity)}
      >
        {device.isActive ? "Sepete Ekle" : "Stokta Yok"}
      </button>
    </div>
  );
};
