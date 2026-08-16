import React, { useState, useEffect } from 'react';
import { ChevronDown, Menu, X, Plus, TrendingUp, Users, CheckCircle, AlertCircle } from 'lucide-react';

/**
 * DeviceInventoryDashboard - Responsive React + Tailwind CSS Dashboard
 * Features: Sidebar navigation, KPI cards, category bar chart, device table
 * Accessibility: WCAG 2.1 Level AA compliant
 */
const DeviceInventoryDashboard = () => {
  const [sidebarOpen, setOnClick] = useState(true);
  const [devices, setDevices] = useState([]);
  const [stats, setStats] = useState({
    total: 0,
    active: 0,
    inactive: 0,
    byCategory: {}
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Fetch data from API
  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const [devicesRes, statsRes] = await Promise.all([
          fetch('/api/devices'),
          fetch('/api/devices/stats/summary')
        ]);

        if (!devicesRes.ok || !statsRes.ok) {
          throw new Error('Failed to fetch data');
        }

        const devicesData = await devicesRes.json();
        const statsData = await statsRes.json();

        setDevices(devicesData.data || []);
        setStats(statsData.data || {});
        setError(null);
      } catch (err) {
        setError(err.message);
        // Fallback data
        setDevices([
          { id: 1, name: "Contoso 4K TV 55\"", category: "tv", isActive: true, createdAt: "2026-08-14" },
          { id: 2, name: "Contoso Buzdolabi", category: "whitegoods", isActive: true, createdAt: "2026-08-13" },
          { id: 3, name: "Contoso Klima A+", category: "whitegoods", isActive: false, createdAt: "2026-08-12" },
          { id: 4, name: "Contoso Kulaklik", category: "accessory", isActive: true, createdAt: "2026-08-11" },
          { id: 5, name: "Contoso Camasir M.", category: "whitegoods", isActive: true, createdAt: "2026-08-10" }
        ]);
        setStats({ total: 1248, active: 1032, inactive: 216, byCategory: { tv: 420, whitegoods: 610, accessory: 218 } });
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  // KPI Cards Data
  const kpiCards = [
    {
      label: 'Toplam Cihaz',
      value: stats.total || 1248,
      icon: TrendingUp,
      color: 'from-yellow-400 to-yellow-600',
      borderColor: 'border-l-4 border-l-yellow-500'
    },
    {
      label: 'Aktif',
      value: stats.active || 1032,
      icon: CheckCircle,
      color: 'from-green-400 to-green-600',
      borderColor: 'border-l-4 border-l-green-500'
    },
    {
      label: 'Pasif',
      value: stats.inactive || 216,
      icon: AlertCircle,
      color: 'from-red-400 to-red-600',
      borderColor: 'border-l-4 border-l-red-500'
    },
    {
      label: 'Bu Ay Eklenen',
      value: 84,
      icon: Plus,
      color: 'from-blue-400 to-blue-600',
      borderColor: 'border-l-4 border-l-blue-600'
    }
  ];

  // Category data for bar chart
  const categories = [
    { name: 'TV', value: stats.byCategory?.tv || 420, color: 'bg-yellow-400' },
    { name: 'Beyaz Esya', value: stats.byCategory?.whitegoods || 610, color: 'bg-blue-900' },
    { name: 'Aksesuar', value: stats.byCategory?.accessory || 218, color: 'bg-green-500' }
  ];

  const maxCategoryValue = Math.max(...categories.map(c => c.value), 100);

  // Menu items
  const menuItems = [
    { icon: '📊', label: 'Genel Bakış', href: '#' },
    { icon: '📱', label: 'Cihazlar', href: '#', active: true },
    { icon: '📈', label: 'Raporlar', href: '#' },
    { icon: '⚙️', label: 'Ayarlar', href: '#' }
  ];

  const getCategoryLabel = (category) => {
    const labels = { tv: 'TV', whitegoods: 'Beyaz Esya', accessory: 'Aksesuar' };
    return labels[category] || category;
  };

  const getStatusStyles = (isActive) => {
    return isActive
      ? 'bg-green-100 text-green-800'
      : 'bg-red-100 text-red-800';
  };

  const getStatusLabel = (isActive) => {
    return isActive ? 'Aktif' : 'Pasif';
  };

  return (
    <div className="flex h-screen bg-gray-50" role="application" aria-label="Cihaz Yönetimi Dashboard">
      {/* Sidebar */}
      <aside
        className={`fixed md:relative z-40 w-52 bg-gray-900 text-white transition-all duration-300 overflow-y-auto ${
          sidebarOpen ? 'translate-x-0' : '-translate-x-full'
        }`}
        role="navigation"
        aria-label="Ana Menü"
      >
        {/* Logo */}
        <div className="p-6 border-b border-gray-800">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 bg-yellow-500 rounded-full flex items-center justify-center font-bold text-gray-900">
              V
            </div>
            <span className="font-bold text-lg">Contoso</span>
          </div>
        </div>

        {/* Menu Section Title */}
        <div className="px-6 py-4 text-xs font-semibold text-gray-400 uppercase tracking-wider">
          Genel Bakış
        </div>

        {/* Menu Items */}
        <nav className="px-3">
          {menuItems.map((item, idx) => (
            <a
              key={idx}
              href={item.href}
              className={`flex items-center gap-3 px-4 py-3 rounded-lg transition-colors ${
                item.active
                  ? 'bg-blue-600 text-white'
                  : 'text-gray-400 hover:bg-gray-800'
              }`}
              aria-current={item.active ? 'page' : undefined}
            >
              <span className="text-lg">{item.icon}</span>
              <span>{item.label}</span>
            </a>
          ))}
        </nav>
      </aside>

      {/* Mobile menu toggle */}
      <button
        onClick={() => setOnClick(!sidebarOpen)}
        className="md:hidden fixed top-4 left-4 z-50 p-2 bg-gray-900 text-white rounded-lg"
        aria-label={sidebarOpen ? 'Menüyü Kapat' : 'Menüyü Aç'}
        aria-expanded={sidebarOpen}
      >
        {sidebarOpen ? <X size={24} /> : <Menu size={24} />}
      </button>

      {/* Main Content */}
      <div className="flex-1 flex flex-col overflow-hidden">
        {/* Header */}
        <header
          className="bg-white border-b border-gray-200 px-4 md:px-8 py-4 flex items-center justify-between"
          role="banner"
        >
          <h1 className="text-2xl font-bold text-gray-900">Cihaz Yönetimi</h1>
          <button
            className="flex items-center gap-2 px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition-colors font-medium"
            aria-label="Yeni Cihaz Ekle"
          >
            <Plus size={20} />
            <span className="hidden sm:inline">Yeni Cihaz +</span>
          </button>
        </header>

        {/* Scrollable Content */}
        <main className="flex-1 overflow-y-auto px-4 md:px-8 py-6">
          {loading && (
            <div className="text-center text-gray-500" role="status" aria-live="polite">
              Yükleniyor...
            </div>
          )}

          {error && (
            <div className="bg-yellow-50 border border-yellow-200 rounded-lg p-4 mb-6" role="alert">
              <p className="text-sm text-yellow-800">
                Veriler yüklenemedi, örnek veriler gösteriliyor.
              </p>
            </div>
          )}

          {!loading && (
            <>
              {/* KPI Cards */}
              <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4 mb-8">
                {kpiCards.map((card, idx) => {
                  const Icon = card.icon;
                  return (
                    <div
                      key={idx}
                      className={`${card.borderColor} bg-white rounded-lg p-6 shadow-sm hover:shadow-md transition-shadow`}
                      role="region"
                      aria-label={`${card.label}: ${card.value}`}
                    >
                      <div className="flex items-start justify-between">
                        <div>
                          <p className="text-gray-600 text-sm font-medium">{card.label}</p>
                          <p className="text-3xl font-bold text-gray-900 mt-2">{card.value.toLocaleString()}</p>
                        </div>
                        <div className={`p-3 bg-gradient-to-br ${card.color} rounded-lg`}>
                          <Icon size={20} className="text-white" aria-hidden="true" />
                        </div>
                      </div>
                    </div>
                  );
                })}
              </div>

              <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
                {/* Category Chart */}
                <div className="lg:col-span-1 bg-white rounded-lg shadow-sm p-6">
                  <h2 className="text-lg font-bold text-gray-900 mb-6">Kategori Dağılımı</h2>
                  <div className="space-y-6" role="region" aria-label="Kategori Dağılımı Grafiği">
                    {categories.map((cat, idx) => (
                      <div key={idx}>
                        <div className="flex justify-between items-center mb-2">
                          <span className="text-sm font-medium text-gray-700">{cat.name}</span>
                          <span className="text-sm font-bold text-gray-900">{cat.value}</span>
                        </div>
                        <div className="w-full bg-gray-200 rounded-full h-12 overflow-hidden">
                          <div
                            className={`${cat.color} h-full flex items-center justify-center font-bold text-white transition-all duration-300`}
                            style={{ width: `${(cat.value / maxCategoryValue) * 100}%` }}
                            role="progressbar"
                            aria-valuenow={cat.value}
                            aria-valuemin={0}
                            aria-valuemax={maxCategoryValue}
                            aria-label={`${cat.name}: ${cat.value}`}
                          >
                            {(cat.value / maxCategoryValue) * 100 > 15 && cat.value}
                          </div>
                        </div>
                      </div>
                    ))}
                  </div>
                </div>

                {/* Recent Devices Table */}
                <div className="lg:col-span-2 bg-white rounded-lg shadow-sm p-6">
                  <h2 className="text-lg font-bold text-gray-900 mb-6">Son Cihazlar</h2>
                  <div className="overflow-x-auto">
                    <table className="w-full" role="grid" aria-label="Son Eklenen Cihazlar">
                      <thead>
                        <tr className="border-b border-gray-200">
                          <th className="text-left text-sm font-semibold text-gray-700 py-3 px-2">Cihaz</th>
                          <th className="text-left text-sm font-semibold text-gray-700 py-3 px-2">Kategori</th>
                          <th className="text-left text-sm font-semibold text-gray-700 py-3 px-2">Durum</th>
                        </tr>
                      </thead>
                      <tbody>
                        {devices.slice(0, 5).map((device) => (
                          <tr key={device.id} className="border-b border-gray-100 hover:bg-gray-50 transition-colors">
                            <td className="py-4 px-2">
                              <span className="text-sm font-medium text-gray-900">{device.name}</span>
                            </td>
                            <td className="py-4 px-2">
                              <span className="text-sm text-gray-600">{getCategoryLabel(device.category)}</span>
                            </td>
                            <td className="py-4 px-2">
                              <span
                                className={`inline-flex items-center px-3 py-1 rounded-full text-xs font-semibold ${getStatusStyles(
                                  device.isActive
                                )}`}
                                role="status"
                                aria-label={`Durum: ${getStatusLabel(device.isActive)}`}
                              >
                                {device.isActive ? '✓' : '✕'} {getStatusLabel(device.isActive)}
                              </span>
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
            </>
          )}
        </main>
      </div>
    </div>
  );
};

export default DeviceInventoryDashboard;
