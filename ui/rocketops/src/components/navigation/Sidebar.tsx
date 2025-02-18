'use client';

import React, { useState } from 'react';
import Link from 'next/link';
import { usePathname } from 'next/navigation';

interface NavItem {
  label: string;
  href: string;
  icon: React.ReactNode;
  badge?: number | string;
}

interface SidebarProps {
  navItems: NavItem[];
  logo: React.ReactNode;
  userSection: React.ReactNode;
  isCollapsed?: boolean;
  onToggleCollapse?: () => void;
}

export const Sidebar: React.FC<SidebarProps> = ({
  navItems,
  logo,
  userSection,
  isCollapsed = false,
  onToggleCollapse,
}) => {
  const pathname = usePathname();
  const [mobileOpen, setMobileOpen] = useState(false);

  const toggleMobileMenu = () => {
    setMobileOpen(!mobileOpen);
  };

  const isActive = (href: string) => {
    return pathname === href || pathname.startsWith(`${href}/`);
  };

  return (
    <>
      {/* Mobile menu button (visible on small screens) */}
      <div className="lg:hidden fixed top-4 left-4 z-50">
        <button
          type="button"
          className="p-2 rounded-md bg-gray-800 text-white focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white"
          onClick={toggleMobileMenu}
          aria-label="Toggle navigation menu"
        >
          <span className="sr-only">Open sidebar</span>
          <svg
            className="h-6 w-6"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            aria-hidden="true"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d={mobileOpen ? "M6 18L18 6M6 6l12 12" : "M4 6h16M4 12h16M4 18h16"}
            />
          </svg>
        </button>
      </div>

      {/* Sidebar */}
      <aside
        className={`
          fixed inset-y-0 left-0 z-40
          flex flex-col
          bg-gray-900 text-white
          transition-all duration-300 ease-in-out
          ${isCollapsed ? 'w-20' : 'w-64'}
          ${mobileOpen ? 'translate-x-0' : '-translate-x-full lg:translate-x-0'}
        `}
      >
        {/* Logo section */}
        <div className={`
          flex items-center 
          ${isCollapsed ? 'justify-center' : 'justify-between'} 
          h-16 px-4 border-b border-gray-800
        `}>
          {logo}
          
          {!isCollapsed && onToggleCollapse && (
            <button
              type="button"
              onClick={onToggleCollapse}
              className="text-gray-400 hover:text-white focus:outline-none"
              aria-label="Collapse sidebar"
              title="Collapse sidebar"
            >
              <svg
                className="h-5 w-5"
                viewBox="0 0 20 20"
                fill="currentColor"
                aria-hidden="true"
              >
                <path
                  fillRule="evenodd"
                  d="M15 10a.75.75 0 01-.75.75H7.612l2.158 1.96a.75.75 0 11-1.04 1.08l-3.5-3.25a.75.75 0 010-1.08l3.5-3.25a.75.75 0 111.04 1.08L7.612 9.25h6.638A.75.75 0 0115 10z"
                  clipRule="evenodd"
                />
              </svg>
              <span className="sr-only">Collapse sidebar</span>
            </button>
          )}
          
          {isCollapsed && onToggleCollapse && (
            <button
              type="button"
              onClick={onToggleCollapse}
              className="absolute -right-3 top-6 bg-primary-600 text-white rounded-full p-1 shadow-md"
              aria-label="Expand sidebar"
              title="Expand sidebar"
            >
              <svg
                className="h-4 w-4"
                viewBox="0 0 20 20"
                fill="currentColor"
                aria-hidden="true"
              >
                <path
                  fillRule="evenodd"
                  d="M10.21 14.77a.75.75 0 001.06-1.06L8.832 11.5h6.668a.75.75 0 000-1.5H8.832l2.438-2.21a.75.75 0 10-1.06-1.06l-3.75 3.75a.75.75 0 000 1.06l3.75 3.75z"
                  clipRule="evenodd"
                />
              </svg>
              <span className="sr-only">Expand sidebar</span>
            </button>
          )}
        </div>

        {/* Navigation items */}
        <nav className="flex-1 overflow-y-auto py-4">
          <ul className="space-y-1 px-2">
            {navItems.map((item) => (
              <li key={item.href}>
                <Link
                  href={item.href}
                  className={`
                    flex items-center p-2 rounded-md
                    ${isCollapsed ? 'justify-center' : 'justify-start'}
                    ${isActive(item.href)
                      ? 'bg-primary-700 text-white'
                      : 'text-gray-300 hover:bg-gray-800 hover:text-white'}
                    transition-colors duration-200
                  `}
                  aria-current={isActive(item.href) ? 'page' : undefined}
                >
                  <span className="flex-shrink-0" aria-hidden="true">{item.icon}</span>
                  
                  {!isCollapsed && (
                    <span className="ml-3 flex-1">{item.label}</span>
                  )}
                  
                  {!isCollapsed && item.badge && (
                    <span className="ml-auto bg-primary-600 text-xs font-medium px-2 py-0.5 rounded-full">
                      {item.badge}
                    </span>
                  )}
                  
                  {isCollapsed && (
                    <span className="sr-only">{item.label}</span>
                  )}
                </Link>
              </li>
            ))}
          </ul>
        </nav>

        {/* User section */}
        <div className={`
          mt-auto border-t border-gray-800 p-4
          ${isCollapsed ? 'items-center justify-center' : ''}
        `}>
          {userSection}
        </div>
      </aside>
    </>
  );
};

export default Sidebar;