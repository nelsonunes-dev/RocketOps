'use client';

import Link from 'next/link';
import Button from '@/components/common/Button';

export default function NotFound() {
  return (
    <div className="min-h-screen bg-gray-100 dark:bg-gray-900 flex flex-col justify-center items-center px-4 py-12">
      <div className="text-center max-w-md">
        <h1 className="text-9xl font-bold text-primary-600 dark:text-primary-500">404</h1>
        
        <h2 className="mt-4 text-3xl font-bold text-gray-900 dark:text-white">
          Page not found
        </h2>
        
        <p className="mt-3 text-lg text-gray-600 dark:text-gray-400">
          Sorry, we couldn&apos;t find the page you&apos;re looking for. It might have been moved or deleted.
        </p>
        
        <div className="mt-8 flex flex-col sm:flex-row justify-center gap-3">
          <Button 
            variant="secondary"
            onClick={() => window.history.back()}
          >
            Go Back
          </Button>
          
          <Link href="/dashboard" passHref>
            <Button variant="primary">
              Return to Dashboard
            </Button>
          </Link>
        </div>
      </div>
    </div>
  );
}
