// import { Metadata } from 'next';
import Link from 'next/link';
import PageContainer from '@/components/common/PageContainer';
import Card from '@/components/common/Card';
import Button from '@/components/common/Button';

// export const metadata: Metadata = {
//   title: 'Launches',
//   description: 'View upcoming and past rocket launches',
// };

// Mock launch data
const upcomingLaunches = [
  {
    id: 'launch-001',
    name: 'Starlink 5-10',
    vehicle: 'Falcon 9',
    launchSite: 'Kennedy Space Center',
    launchDate: '2025-03-15T14:30:00Z',
    status: 'scheduled',
    mission: 'Starlink Group 5'
  },
  {
    id: 'launch-002',
    name: 'Crew-9',
    vehicle: 'Falcon 9',
    launchSite: 'Kennedy Space Center',
    launchDate: '2025-04-02T08:45:00Z',
    status: 'scheduled',
    mission: 'ISS Crew Rotation'
  },
  {
    id: 'launch-003',
    name: 'Mars Sample Return',
    vehicle: 'Starship',
    launchSite: 'Starbase',
    launchDate: '2025-05-10T20:00:00Z',
    status: 'scheduled',
    mission: 'Mars Sample Return'
  }
];

const pastLaunches = [
  {
    id: 'launch-004',
    name: 'Polaris Dawn',
    vehicle: 'Falcon 9',
    launchSite: 'Cape Canaveral',
    launchDate: '2024-12-10T15:20:00Z',
    status: 'successful',
    mission: 'Commercial Spaceflight'
  },
  {
    id: 'launch-005',
    name: 'Europa Clipper',
    vehicle: 'Falcon Heavy',
    launchSite: 'Kennedy Space Center',
    launchDate: '2024-11-22T12:15:00Z',
    status: 'successful',
    mission: 'Outer Planets Exploration'
  },
  {
    id: 'launch-006',
    name: 'CRS-30',
    vehicle: 'Falcon 9',
    launchSite: 'Cape Canaveral',
    launchDate: '2024-10-05T09:30:00Z',
    status: 'successful',
    mission: 'ISS Resupply'
  }
];

// Format date for display
const formatDate = (dateString: string) => {
  const date = new Date(dateString);
  return date.toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  });
};

// Get status badge style
const getStatusBadge = (status: string) => {
  const statusMap: Record<string, { color: string; label: string }> = {
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

export default function LaunchesPage() {
  return (
    <PageContainer title="Launches">
      <div className="flex justify-between items-center mb-6">
        <div>
          <p className="text-gray-600 dark:text-gray-400">
            View and manage all scheduled and past launches
          </p>
        </div>
        <Button variant="primary">
          Schedule New Launch
        </Button>
      </div>

      <Card title="Upcoming Launches" className="mb-6">
        <div className="overflow-x-auto">
          <table className="min-w-full divide-y divide-gray-200 dark:divide-gray-700">
            <thead>
              <tr>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Launch</th>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Vehicle</th>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Launch Date</th>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Status</th>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Mission</th>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800"></th>
              </tr>
            </thead>
            <tbody className="bg-white dark:bg-gray-900 divide-y divide-gray-200 dark:divide-gray-800">
              {upcomingLaunches.map((launch) => (
                <tr key={launch.id} className="hover:bg-gray-50 dark:hover:bg-gray-800/50">
                  <td className="px-4 py-4 whitespace-nowrap">
                    <div className="flex items-center">
                      <div>
                        <div className="text-sm font-medium text-gray-900 dark:text-white">{launch.name}</div>
                        <div className="text-sm text-gray-500 dark:text-gray-400">{launch.launchSite}</div>
                      </div>
                    </div>
                  </td>
                  <td className="px-4 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">{launch.vehicle}</td>
                  <td className="px-4 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">{formatDate(launch.launchDate)}</td>
                  <td className="px-4 py-4 whitespace-nowrap">
                    {getStatusBadge(launch.status)}
                  </td>
                  <td className="px-4 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">{launch.mission}</td>
                  <td className="px-4 py-4 whitespace-nowrap text-right text-sm font-medium">
                    <Link href={`/launches/${launch.id}`} className="text-primary-600 hover:text-primary-900 dark:text-primary-400 dark:hover:text-primary-300">
                      View
                    </Link>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        {upcomingLaunches.length === 0 && (
          <p className="text-gray-600 dark:text-gray-400 py-4 text-center">
            No upcoming launches scheduled
          </p>
        )}
      </Card>

      <Card title="Past Launches">
        <div className="overflow-x-auto">
          <table className="min-w-full divide-y divide-gray-200 dark:divide-gray-700">
            <thead>
              <tr>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Launch</th>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Vehicle</th>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Launch Date</th>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Status</th>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Mission</th>
                <th className="px-4 py-3 bg-gray-50 dark:bg-gray-800"></th>
              </tr>
            </thead>
            <tbody className="bg-white dark:bg-gray-900 divide-y divide-gray-200 dark:divide-gray-800">
              {pastLaunches.map((launch) => (
                <tr key={launch.id} className="hover:bg-gray-50 dark:hover:bg-gray-800/50">
                  <td className="px-4 py-4 whitespace-nowrap">
                    <div className="flex items-center">
                      <div>
                        <div className="text-sm font-medium text-gray-900 dark:text-white">{launch.name}</div>
                        <div className="text-sm text-gray-500 dark:text-gray-400">{launch.launchSite}</div>
                      </div>
                    </div>
                  </td>
                  <td className="px-4 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">{launch.vehicle}</td>
                  <td className="px-4 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">{formatDate(launch.launchDate)}</td>
                  <td className="px-4 py-4 whitespace-nowrap">
                    {getStatusBadge(launch.status)}
                  </td>
                  <td className="px-4 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">{launch.mission}</td>
                  <td className="px-4 py-4 whitespace-nowrap text-right text-sm font-medium">
                    <Link href={`/launches/${launch.id}`} className="text-primary-600 hover:text-primary-900 dark:text-primary-400 dark:hover:text-primary-300">
                      View
                    </Link>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        {pastLaunches.length === 0 && (
          <p className="text-gray-600 dark:text-gray-400 py-4 text-center">
            No past launches found
          </p>
        )}

        <div className="mt-4 flex justify-center">
          <Button variant="ghost" size="sm">
            View All Past Launches
          </Button>
        </div>
      </Card>
    </PageContainer>
  );
}
