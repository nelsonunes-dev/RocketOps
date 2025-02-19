'use client';

import React from 'react';

interface CardProps {
  children: React.ReactNode;
  title?: string;
  subtitle?: string;
  footer?: React.ReactNode;
  className?: string;
  noPadding?: boolean;
  headerAction?: React.ReactNode;
  variant?: 'default' | 'outline' | 'filled';
}

export const Card: React.FC<CardProps> = ({
  children,
  title,
  subtitle,
  footer,
  className = '',
  noPadding = false,
  headerAction,
  variant = 'default',
}) => {
  // Get variant-specific classes
  const getVariantClasses = (): string => {
    switch (variant) {
      case 'outline':
        return 'bg-transparent border border-gray-200 dark:border-gray-700';
      case 'filled':
        return 'bg-gray-50 dark:bg-gray-800/50';
      case 'default':
      default:
        return 'bg-white dark:bg-gray-800 shadow-sm';
    }
  };

  return (
    <div 
      className={`
        rounded-lg overflow-hidden
        ${getVariantClasses()}
        ${className}
      `}
    >
      {/* Card header */}
      {(title || headerAction) && (
        <div className="px-6 py-4 border-b border-gray-200 dark:border-gray-700 flex justify-between items-center">
          <div>
            {title && (
              <h3 className="text-lg font-medium text-gray-900 dark:text-gray-100">
                {title}
              </h3>
            )}
            {subtitle && (
              <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
                {subtitle}
              </p>
            )}
          </div>
          {headerAction && (
            <div className="ml-4">
              {headerAction}
            </div>
          )}
        </div>
      )}

      {/* Card content */}
      <div className={noPadding ? '' : 'p-6'}>
        {children}
      </div>

      {/* Card footer */}
      {footer && (
        <div className="px-6 py-4 bg-gray-50 dark:bg-gray-800/80 border-t border-gray-200 dark:border-gray-700">
          {footer}
        </div>
      )}
    </div>
  );
};

export default Card;
