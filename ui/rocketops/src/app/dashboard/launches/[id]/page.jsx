'use client';

import { notFound } from 'next/navigation';
import PageContainer from '@/components/common/PageContainer';
import Card from '@/components/common/Card';
import Button from '@/components/common/Button';
import Link from 'next/link';

// In a real app, this would be fetched from an API
const getLaunchData = (id) => {
  // Mock data for demonstration purposes
  const mockLaunch = {
    id,
    name: 'Starlink 5-10',
    vehicle: 'Falcon 9',
    launchSite: 'Kennedy Space Center',
    launchDate: '2025-03-15T14:30:00Z',
    status: 'scheduled',
    description: 'Deployment of 60 Starlink satellites to low Earth orbit.',
    payload: [
      { id: 'payload-1', name: 'Starlink satellites', type: 'Communication', mass: 15400 }
    ],
    weather: {
      status: 'favorable',
      temperature: 22,
      windSpeed: 12,
      precipitation: 0,
      visibility: 10
    },
    mission: {
      id: 'mission-123',
      name: 'Starlink Group 5'
    },
    team: [
      { id: 'user-1', name: 'Alex Johnson', role: 'Mission Director' },
      { id: 'user-2', name: 'Sarah Chen', role: 'Flight Engineer' },
      { id: 'user-3', name: 'Raj Patel', role: 'Payload Specialist' }
    ]
  };

  return mockLaunch;
};

// This would be replaced with a dynamic metadata function in a real app
// export const metadata = {
//   title: 'Launch Details',
//   description: 'View details of a specific launch',
// };

export default function LaunchDetailPage({ params }) {
  // In a real app, this would use a data fetching library like React Query
  const launch = getLaunchData(params.id);

  // If launch doesn't exist
  if (!launch) {
    notFound();
  }

  // Format date
  const formattedDate = new Date(launch.launchDate).toLocaleDateString('en-US', {
    weekday: 'long',
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  });

  // Format status with proper styling
  const getStatusBadge = (status) => {
    const statusMap = {
      scheduled: { color: 'bg-blue-100 text-blue-800 dark:bg-blue-900/30 dark:text-blue-300', label: 'Scheduled' },
      countdown: { color: 'bg-yellow-100 text-yellow-800 dark:bg-yellow-900/30 dark:text-yellow-300', label: 'Countdown' },
      'in-flight': { color: 'bg-purple-100 text-purple-800 dark:bg-purple-900/30 dark:text-purple-300', label: 'In Flight' },
      successful: { color: 'bg-green-100 text-green-800 dark:bg-green-900/30 dark:text-green-300', label: 'Successful' },
      failed: { color: 'bg-red-100 text-red-800 dark:bg-red-900/30 dark:text-red-300', label: 'Failed' },
      delayed: { color: 'bg-orange-100 text-orange-800 dark:bg-orange-900/30 dark:text-orange-300', label: 'Delayed' },
      aborted: { color: 'bg-gray-100 text-gray-800 dark:bg-gray-800 dark:text-gray-300', label: 'Aborted' }
    };

    const statusInfo = statusMap[status] || { color: 'bg-gray-100 text-gray-800', label: status };
    
    return (
      <span className={`px-2 py-1 rounded-full text-xs font-medium ${statusInfo.color}`}>
        {statusInfo.label}
      </span>
    );
  };

  return (
    <PageContainer>
      <div className="mb-6 flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
        <div>
          <div className="flex items-center mb-2">
            <Link href="/launches" className="text-primary-600 dark:text-primary-400 mr-2" aria-label="Back to launches">
              <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                <path fillRule="evenodd" d="M9.707 16.707a1 1 0 01-1.414 0l-6-6a1 1 0 010-1.414l6-6a1 1 0 011.414 1.414L5.414 9H17a1 1 0 110 2H5.414l4.293 4.293a1 1 0 010 1.414z" clipRule="evenodd" />
              </svg>
            </Link>
            <h1 className="text-2xl font-bold text-gray-900 dark:text-white">{launch.name}</h1>
            <div className="ml-4">{getStatusBadge(launch.status)}</div>
          </div>
          <p className="text-gray-600 dark:text-gray-400">
            {launch.vehicle} • {launch.launchSite}
          </p>
        </div>
        <div className="flex flex-wrap gap-2">
          <Button variant="secondary">
            Download Manifest
          </Button>
          <Link href={`/launches/${params.id}/edit`}>
            <Button variant="primary">
              Edit Launch
            </Button>
          </Link>
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        {/* Main info */}
        <div className="lg:col-span-2 space-y-6">
          <Card title="Launch Information">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">Launch Date & Time</h3>
                <p className="mt-1 text-sm text-gray-900 dark:text-white">{formattedDate}</p>
              </div>
              <div>
                <h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">Launch Site</h3>
                <p className="mt-1 text-sm text-gray-900 dark:text-white">{launch.launchSite}</p>
              </div>
              <div>
                <h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">Vehicle</h3>
                <p className="mt-1 text-sm text-gray-900 dark:text-white">{launch.vehicle}</p>
              </div>
              <div>
                <h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">Mission</h3>
                <p className="mt-1 text-sm text-gray-900 dark:text-white">
                  <Link href={`/missions/${launch.mission.id}`} className="text-primary-600 hover:text-primary-800 dark:text-primary-400 dark:hover:text-primary-300">
                    {launch.mission.name}
                  </Link>
                </p>
              </div>
            </div>
            
            <div className="mt-6">
              <h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">Description</h3>
              <p className="mt-1 text-sm text-gray-900 dark:text-white">{launch.description}</p>
            </div>
          </Card>

          <Card title="Payload">
            <div className="overflow-x-auto">
              <table className="min-w-full divide-y divide-gray-200 dark:divide-gray-700">
                <thead>
                  <tr>
                    <th className="px-3 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Name</th>
                    <th className="px-3 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Type</th>
                    <th className="px-3 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Mass (kg)</th>
                  </tr>
                </thead>
                <tbody className="bg-white dark:bg-gray-900 divide-y divide-gray-200 dark:divide-gray-800">
                  {launch.payload.map((item) => (
                    <tr key={item.id}>
                      <td className="px-3 py-3 whitespace-nowrap text-sm text-gray-900 dark:text-white">{item.name}</td>
                      <td className="px-3 py-3 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">{item.type}</td>
                      <td className="px-3 py-3 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">{item.mass.toLocaleString()}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </Card>
        </div>

        {/* Sidebar */}
        <div className="space-y-6">
          <Card title="Weather Conditions">
            <div className="space-y-3">
              <div className="flex items-center justify-between">
                <span className="text-sm text-gray-600 dark:text-gray-400">Status</span>
                <span className={`px-2 py-1 rounded-full text-xs ${
                  launch.weather.status === 'favorable' 
                    ? 'bg-green-100 text-green-800 dark:bg-green-900/30 dark:text-green-300' 
                    : 'bg-amber-100 text-amber-800 dark:bg-amber-900/30 dark:text-amber-300'
                }`}>
                  {launch.weather.status.charAt(0).toUpperCase() + launch.weather.status.slice(1)}
                </span>
              </div>
              <div className="flex items-center justify-between">
                <span className="text-sm text-gray-600 dark:text-gray-400">Temperature</span>
                <span className="text-sm text-gray-900 dark:text-white">{launch.weather.temperature}°C</span>
              </div>
              <div className="flex items-center justify-between">
                <span className="text-sm text-gray-600 dark:text-gray-400">Wind Speed</span>
                <span className="text-sm text-gray-900 dark:text-white">{launch.weather.windSpeed} km/h</span>
              </div>
              <div className="flex items-center justify-between">
                <span className="text-sm text-gray-600 dark:text-gray-400">Precipitation</span>
                <span className="text-sm text-gray-900 dark:text-white">{launch.weather.precipitation}%</span>
              </div>
              <div className="flex items-center justify-between">
                <span className="text-sm text-gray-600 dark:text-gray-400">Visibility</span>
                <span className="text-sm text-gray-900 dark:text-white">{launch.weather.visibility} km</span>
              </div>
            </div>
          </Card>

          <Card title="Launch Team">
            <div className="space-y-4">
              {launch.team.map((member) => (
                <div key={member.id} className="flex items-center">
                  <div className="flex-shrink-0 h-8 w-8 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center">
                    <span className="text-xs font-medium text-gray-700 dark:text-gray-300">
                      {member.name.split(' ').map(n => n[0]).join('')}
                    </span>
                  </div>
                  <div className="ml-3">
                    <p className="text-sm font-medium text-gray-900 dark:text-white">{member.name}</p>
                    <p className="text-xs text-gray-500 dark:text-gray-400">{member.role}</p>
                  </div>
                </div>
              ))}
            </div>
            <Button variant="ghost" size="sm" className="mt-4 w-full">
              View All Team Members
            </Button>
          </Card>
        </div>
      </div>
    </PageContainer>
  );
}
