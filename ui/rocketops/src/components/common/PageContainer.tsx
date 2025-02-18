import React from 'react';

interface PageContainerProps {
  children: React.ReactNode;
  title?: string;
  className?: string;
}

export const PageContainer: React.FC<PageContainerProps> = ({ 
  children, 
  title,
  className = ''
}) => {
  return (
    <div className={`container mx-auto px-4 py-6 ${className}`}>
      {title && (
        <h1 className="text-2xl font-bold mb-6 text-gray-800 dark:text-gray-100">
          {title}
        </h1>
      )}
      {children}
    </div>
  );
};

export default PageContainer;