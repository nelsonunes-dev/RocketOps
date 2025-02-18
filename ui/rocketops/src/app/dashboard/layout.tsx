'use client';

import React, { useState } from 'react';
import Link from 'next/link';
import Sidebar from '../../components/navigation/Sidebar';
import { useTheme } from '../../components/theme/ThemeProvider';

// Icons (placeholder for real icons you might import)
const DashboardIcon = () => (
  <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2H6a2 2 0 01-2-2V6z" />
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M14 6a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2V6z" />
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 16a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2H6a2 2 0 01-2-2v-2z" />
  </svg>
);

const LaunchIcon = () => (
  <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 10V3L4 14h7v7l9-11h-7z" />
  </svg>
);

const MissionIcon = () => (
  <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 12l2 2 4-4m5.618-4.016A11.955 11.955 0 0112 2.944a11.955 11.955 0 01-8.618 3.04A12.02 12.02 0 003 9c0 5.591 3.824 10.29 9 11.622 5.176-1.332 9-6.03 9-11.622 0-1.042-.133-2.052-.382-3.016z" />
  </svg>
);

const ReportIcon = () => (
  <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 17v-2m3 2v-4m3 4v-6m2 10H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
  </svg>
);

const SettingsIcon = () => (
  <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z" />
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
  </svg>
);

// The layout for all dashboard pages
export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const [sidebarCollapsed, setSidebarCollapsed] = useState(false);
  const { theme, toggleTheme } = useTheme();

  // Navigation items for the sidebar
  const navItems = [
    {
      label: 'Dashboard',
      href: '/dashboard',
      icon: <DashboardIcon />,
    },
    {
      label: 'Launches',
      href: '/launches',
      icon: <LaunchIcon />,
      badge: 3,
    },
    {
      label: 'Missions',
      href: '/missions',
      icon: <MissionIcon />,
    },
    {
      label: 'Reports',
      href: '/reports',
      icon: <ReportIcon />,
    },
    {
      label: 'Settings',
      href: '/settings',
      icon: <SettingsIcon />,
    },
  ];

  // Logo component for the sidebar
  const Logo = (
    <Link href="/dashboard" className="flex items-center space-x-2">
      {!sidebarCollapsed && (
        <>
          <span className="text-xl font-bold text-white">
            Rocket<span className="text-primary-400">Ops</span>
          </span>
        </>
      )}
      {sidebarCollapsed && (
        <span className="text-2xl font-bold text-primary-400">R</span>
      )}
    </Link>
  );

  // User section component for the sidebar
  const UserSection = (
    <div className={`flex ${sidebarCollapsed ? 'justify-center' : 'items-center space-x-3'}`}>
      {!sidebarCollapsed && (
        <div>
          <p className="text-sm font-medium text-white">Nelson Nunes</p>
          <p className="text-xs text-gray-400">Senior Fullstack Developer</p>
        </div>
      )}
      <div className="relative">
        <button
          type="button"
          className="w-10 h-10 rounded-full bg-gray-700 flex items-center justify-center"
          aria-label="User menu"
        >
          <span className="text-sm font-medium text-white">NN</span>
        </button>
        <span className="absolute bottom-0 right-0 block w-2.5 h-2.5 bg-green-400 rounded-full ring-2 ring-gray-900"></span>
      </div>
    </div>
  );
  
  // Toggle theme button
  const ToggleThemeButton = (
    <button
      type="button"
      onClick={toggleTheme}
      className="fixed bottom-6 right-6 z-50 bg-primary-600 text-white p-3 rounded-full shadow-lg hover:bg-primary-700 transition-colors"
      aria-label={theme === 'dark' ? 'Switch to light mode' : 'Switch to dark mode'}
    >
      {theme === 'dark' ? (
        <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 3v1m0 16v1m9-9h-1M4 12H3m15.364 6.364l-.707-.707M6.343 6.343l-.707-.707m12.728 0l-.707.707M6.343 17.657l-.707.707M16 12a4 4 0 11-8 0 4 4 0 018 0z" />
        </svg>
      ) : (
        <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20.354 15.354A9 9 0 018.646 3.646 9.003 9.003 0 0012 21a9.003 9.003 0 008.354-5.646z" />
        </svg>
      )}
    </button>
  );

  return (
    <div className="min-h-screen bg-gray-100 dark:bg-gray-900">
      {/* Sidebar */}
      <Sidebar
        navItems={navItems}
        logo={Logo}
        userSection={UserSection}
        isCollapsed={sidebarCollapsed}
        onToggleCollapse={() => setSidebarCollapsed(!sidebarCollapsed)}
      />
      
      {/* Main content */}
      <main className={`transition-all duration-300 ${sidebarCollapsed ? 'lg:ml-20' : 'lg:ml-64'}`}>
        {children}
      </main>
      
      {/* Theme toggle button */}
      {ToggleThemeButton}
    </div>
  );
}
