'use client';

import { useEffect } from 'react';
import PageContainer from '@/components/common/PageContainer';
import Card from '@/components/common/Card';
import Button from '@/components/common/Button';

export default function DashboardError({
  error,
  reset,
}: {
  error: Error & { digest?: string };
  reset: () => void;
}) {
  useEffect(() => {
    // Log the error to an error reporting service
    console.error('Dashboard error:', error);
  }, [error]);

  return (
    <PageContainer>
      <Card className="max-w-2xl mx-auto">
        <div className="text-center">
          <div className="flex justify-center mb-4">
            <div className="h-16 w-16 rounded-full bg-red-100 dark:bg-red-900/30 flex items-center justify-center">
              <svg
                className="h-8 w-8 text-red-600 dark:text-red-400"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
                aria-hidden="true"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z"
                />
              </svg>
            </div>
          </div>
          
          <h3 className="text-lg font-medium text-gray-900 dark:text-white">
            Something went wrong
          </h3>
          
          <div className="mt-2 text-sm text-gray-600 dark:text-gray-400">
            <p>
              We encountered an error while loading the dashboard. Please try again or contact support if the problem persists.
            </p>
            {process.env.NODE_ENV !== 'production' && (
              <div className="mt-2 p-2 bg-red-50 dark:bg-red-900/20 text-red-800 dark:text-red-300 text-left rounded overflow-auto">
                <p className="font-mono text-xs">{error.message}</p>
                <p className="font-mono text-xs mt-1">{error.stack}</p>
              </div>
            )}
          </div>
          
          <div className="mt-6 flex justify-center gap-3">
            <Button
              type="button"
              variant="secondary"
              onClick={() => window.location.href = '/dashboard'}
            >
              Refresh Page
            </Button>
            <Button
              type="button"
              variant="primary"
              onClick={() => reset()}
            >
              Try Again
            </Button>
          </div>
        </div>
      </Card>
    </PageContainer>
  );
}
