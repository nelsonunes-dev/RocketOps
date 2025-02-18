import { Metadata } from 'next';
import React from 'react';
import PageContainer from '@/components/common/PageContainer';
import Card from '@/components/common/Card';
import Button from '@/components/common/Button';

export const metadata: Metadata = {
  title: 'Mission Control Dashboard',
  description: 'Monitor and manage your rocket launch operations',
};

export default function DashboardPage() {
  return (
    <PageContainer title="Mission Control Dashboard">
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {/* Status Overview Card */}
        <div className="col-span-full">
          <Card title="Mission Status" variant="default">
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              <div className="bg-green-50 dark:bg-green-900/20 p-4 rounded-lg">
                <p className="text-sm text-gray-500 dark:text-gray-400">Active Missions</p>
                <p className="text-2xl font-bold text-green-600 dark:text-green-400">4</p>
              </div>
              <div className="bg-blue-50 dark:bg-blue-900/20 p-4 rounded-lg">
                <p className="text-sm text-gray-500 dark:text-gray-400">Scheduled Launches</p>
                <p className="text-2xl font-bold text-blue-600 dark:text-blue-400">7</p>
              </div>
              <div className="bg-purple-50 dark:bg-purple-900/20 p-4 rounded-lg">
                <p className="text-sm text-gray-500 dark:text-gray-400">Team Members</p>
                <p className="text-2xl font-bold text-purple-600 dark:text-purple-400">42</p>
              </div>
              <div className="bg-amber-50 dark:bg-amber-900/20 p-4 rounded-lg">
                <p className="text-sm text-gray-500 dark:text-gray-400">Issues</p>
                <p className="text-2xl font-bold text-amber-600 dark:text-amber-400">3</p>
              </div>
            </div>
          </Card>
        </div>

        {/* Next Launch Countdown */}
        <Card title="Next Launch" className="h-full">
          <div className="mb-4">
            <h3 className="text-lg font-medium text-gray-700 dark:text-gray-300">Starlink 5-10</h3>
            <p className="text-sm text-gray-500 dark:text-gray-400">Falcon 9 • Kennedy Space Center</p>
          </div>
          <div className="grid grid-cols-4 gap-2 mb-4">
            <div className="bg-gray-100 dark:bg-gray-700 p-3 rounded text-center">
              <p className="text-2xl font-bold text-gray-800 dark:text-gray-200">02</p>
              <p className="text-xs text-gray-500 dark:text-gray-400">Days</p>
            </div>
            <div className="bg-gray-100 dark:bg-gray-700 p-3 rounded text-center">
              <p className="text-2xl font-bold text-gray-800 dark:text-gray-200">18</p>
              <p className="text-xs text-gray-500 dark:text-gray-400">Hours</p>
            </div>
            <div className="bg-gray-100 dark:bg-gray-700 p-3 rounded text-center">
              <p className="text-2xl font-bold text-gray-800 dark:text-gray-200">45</p>
              <p className="text-xs text-gray-500 dark:text-gray-400">Mins</p>
            </div>
            <div className="bg-gray-100 dark:bg-gray-700 p-3 rounded text-center">
              <p className="text-2xl font-bold text-gray-800 dark:text-gray-200">20</p>
              <p className="text-xs text-gray-500 dark:text-gray-400">Secs</p>
            </div>
          </div>
          <div className="flex items-center">
            <div className="w-2 h-2 bg-green-500 rounded-full mr-2"></div>
            <p className="text-sm text-gray-600 dark:text-gray-300">Weather conditions: Favorable</p>
          </div>
          <div className="mt-4">
            <Button variant="primary" size="sm" fullWidth>View Launch Details</Button>
          </div>
        </Card>

        {/* Recent Updates */}
        <Card title="Recent Updates" className="h-full">
          <div className="space-y-4">
            <div className="border-l-4 border-blue-500 pl-3 py-1">
              <p className="text-sm font-medium text-gray-800 dark:text-gray-200">Weather monitoring updated</p>
              <p className="text-xs text-gray-500 dark:text-gray-400">10 minutes ago</p>
            </div>
            <div className="border-l-4 border-green-500 pl-3 py-1">
              <p className="text-sm font-medium text-gray-800 dark:text-gray-200">Falcon 9 pre-flight checks completed</p>
              <p className="text-xs text-gray-500 dark:text-gray-400">2 hours ago</p>
            </div>
            <div className="border-l-4 border-purple-500 pl-3 py-1">
              <p className="text-sm font-medium text-gray-800 dark:text-gray-200">New team member added: Sarah Chen</p>
              <p className="text-xs text-gray-500 dark:text-gray-400">Yesterday</p>
            </div>
            <div className="border-l-4 border-amber-500 pl-3 py-1">
              <p className="text-sm font-medium text-gray-800 dark:text-gray-200">Payload integration delayed</p>
              <p className="text-xs text-gray-500 dark:text-gray-400">Yesterday</p>
            </div>
          </div>
          <Button variant="ghost" size="sm" className="mt-4">
            View all updates
          </Button>
        </Card>

        {/* System Status */}
        <Card title="System Status" className="h-full">
          <div className="space-y-3">
            <div className="flex items-center justify-between">
              <div className="flex items-center">
                <div className="w-2 h-2 bg-green-500 rounded-full mr-2"></div>
                <p className="text-sm text-gray-700 dark:text-gray-300">Communication Systems</p>
              </div>
              <span className="text-xs bg-green-100 text-green-800 dark:bg-green-900/50 dark:text-green-400 px-2 py-1 rounded">Operational</span>
            </div>
            <div className="flex items-center justify-between">
              <div className="flex items-center">
                <div className="w-2 h-2 bg-green-500 rounded-full mr-2"></div>
                <p className="text-sm text-gray-700 dark:text-gray-300">Launch Control Network</p>
              </div>
              <span className="text-xs bg-green-100 text-green-800 dark:bg-green-900/50 dark:text-green-400 px-2 py-1 rounded">Operational</span>
            </div>
            <div className="flex items-center justify-between">
              <div className="flex items-center">
                <div className="w-2 h-2 bg-amber-500 rounded-full mr-2"></div>
                <p className="text-sm text-gray-700 dark:text-gray-300">Weather Monitoring</p>
              </div>
              <span className="text-xs bg-amber-100 text-amber-800 dark:bg-amber-900/50 dark:text-amber-400 px-2 py-1 rounded">Degraded</span>
            </div>
            <div className="flex items-center justify-between">
              <div className="flex items-center">
                <div className="w-2 h-2 bg-green-500 rounded-full mr-2"></div>
                <p className="text-sm text-gray-700 dark:text-gray-300">Tracking Systems</p>
              </div>
              <span className="text-xs bg-green-100 text-green-800 dark:bg-green-900/50 dark:text-green-400 px-2 py-1 rounded">Operational</span>
            </div>
            <div className="flex items-center justify-between">
              <div className="flex items-center">
                <div className="w-2 h-2 bg-green-500 rounded-full mr-2"></div>
                <p className="text-sm text-gray-700 dark:text-gray-300">Propellant Systems</p>
              </div>
              <span className="text-xs bg-green-100 text-green-800 dark:bg-green-900/50 dark:text-green-400 px-2 py-1 rounded">Operational</span>
            </div>
          </div>
          <Button variant="ghost" size="sm" className="mt-4">
            View detailed status
          </Button>
        </Card>
      </div>
    </PageContainer>
  );
}
