'use client';

import { useEffect } from 'react';
import { Inter } from 'next/font/google';
import Button from '@/components/common/Button';

const inter = Inter({ subsets: ['latin'], variable: '--font-inter' });

export default function GlobalError({
  error,
  reset,
}: {
  error: Error & { digest?: string };
  reset: () => void;
}) {
  useEffect(() => {
    // Log the error to an error reporting service
    console.error('Global error:', error);
  }, [error]);

  return (
    <html lang="en" className="h-full">
      <body className={`h-full ${inter.className}`}>
        <div className="min-h-screen bg-gray-100 dark:bg-gray-900 flex flex-col justify-center items-center p-4">
          <div className="bg-white dark:bg-gray-800 px-6 py-8 rounded-lg shadow-xl max-w-md w-full text-center">
            <div className="flex justify-center mb-6">
              <div className="h-20 w-20 rounded-full bg-red-100 dark:bg-red-900/30 flex items-center justify-center">
                <svg
                  className="h-10 w-10 text-red-600 dark:text-red-400"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
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
            
            <h1 className="text-2xl font-bold text-gray-900 dark:text-white">
              Application Error
            </h1>
            
            <p className="mt-3 text-gray-600 dark:text-gray-400">
              We&apos;re sorry, but something went wrong. Our team has been notified and is working to fix the issue.
            </p>
            
            {process.env.NODE_ENV !== 'production' && (
              <div className="mt-4 p-3 bg-red-50 dark:bg-red-900/20 text-red-800 dark:text-red-300 text-left rounded overflow-auto max-h-60">
                <p className="font-mono text-xs">{error.message}</p>
                <p className="font-mono text-xs mt-1">{error.stack}</p>
                {error.digest && (
                  <p className="font-mono text-xs mt-1">Digest: {error.digest}</p>
                )}
              </div>
            )}
            
            <div className="mt-6 flex flex-col sm:flex-row gap-3 justify-center">
              <Button
                type="button"
                variant="secondary"
                onClick={() => window.location.href = '/'}
              >
                Go Home
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
          
          <div className="mt-8 text-center">
            <p className="text-sm text-gray-500 dark:text-gray-400">
              If you continue to experience issues, please contact support.
            </p>
          </div>
        </div>
      </body>
    </html>
  );
}
