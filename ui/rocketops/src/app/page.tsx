'use client';

import { useEffect } from 'react';
import { redirect } from 'next/navigation';

export default function HomePage() {
  useEffect(() => {
    redirect('/dashboard');
  }, []);
  
  // Render nothing while redirecting
  return null;
}
