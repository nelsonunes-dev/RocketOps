'use client';

import PageContainer from '@/components/common/PageContainer';
import Card from '@/components/common/Card';
import Button from '@/components/common/Button';

export default function ProfilePage() {
  return (
    <PageContainer title="My Profile">
      {/* Existing page content */}
      <div className="grid grid-cols-12 gap-6">
        {/* Profile overview section */}
        <div className="col-span-12 lg:col-span-4">
          <Card className="h-full">
            <div className="flex flex-col items-center">
              {/* Profile picture */}
              <div className="w-32 h-32 rounded-full bg-gray-300 dark:bg-gray-700 mb-4 flex items-center justify-center">
                <span className="text-2xl font-bold text-gray-600 dark:text-gray-300">AJ</span>
              </div>
              
              {/* User info */}
              <h2 className="text-xl font-bold text-gray-800 dark:text-gray-100">Alex Johnson</h2>
              <p className="text-gray-600 dark:text-gray-400 mb-2">Mission Director</p>
              
              <div className="flex space-x-2 mb-6">
                <span className="px-2 py-1 bg-blue-100 text-blue-800 dark:bg-blue-900/30 dark:text-blue-300 text-xs rounded-full">
                  Level 3 Clearance
                </span>
                <span className="px-2 py-1 bg-green-100 text-green-800 dark:bg-green-900/30 dark:text-green-300 text-xs rounded-full">
                  5 Years
                </span>
              </div>
              
              <div className="w-full border-t border-gray-200 dark:border-gray-700 pt-4">
                <div className="grid grid-cols-2 gap-4 mb-4">
                  <div className="text-center">
                    <p className="text-2xl font-bold text-gray-800 dark:text-gray-100">42</p>
                    <p className="text-sm text-gray-600 dark:text-gray-400">Missions</p>
                  </div>
                  <div className="text-center">
                    <p className="text-2xl font-bold text-gray-800 dark:text-gray-100">87</p>
                    <p className="text-sm text-gray-600 dark:text-gray-400">Launches</p>
                  </div>
                </div>
              </div>
              
              <Button variant="secondary" className="w-full">Edit Profile</Button>
            </div>
          </Card>
        </div>
        
        {/* Main content section */}
        <div className="col-span-12 lg:col-span-8 space-y-6">
          {/* User details */}
          <Card title="Personal Information">
            <div className="space-y-4">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                    Full Name
                  </label>
                  <p className="text-gray-900 dark:text-gray-100">Alex Johnson</p>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                    Email Address
                  </label>
                  <p className="text-gray-900 dark:text-gray-100">alex.johnson@rocketops.example</p>
                </div>
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                    Department
                  </label>
                  <p className="text-gray-900 dark:text-gray-100">Mission Control</p>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                    Position
                  </label>
                  <p className="text-gray-900 dark:text-gray-100">Mission Director</p>
                </div>
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                    Location
                  </label>
                  <p className="text-gray-900 dark:text-gray-100">Houston, TX</p>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                    Join Date
                  </label>
                  <p className="text-gray-900 dark:text-gray-100">March 12, 2019</p>
                </div>
              </div>
            </div>
          </Card>
          
          {/* Recent activity */}
          <Card title="Recent Activity">
            <div className="space-y-4">
              <div className="flex items-start">
                <div className="flex-shrink-0 h-10 w-10 rounded-full bg-green-100 dark:bg-green-900/30 flex items-center justify-center text-green-600 dark:text-green-400">
                  <svg className="h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </div>
                <div className="ml-4">
                  <p className="text-sm font-medium text-gray-900 dark:text-gray-100">
                    Approved launch sequence for Mission Artemis IV
                  </p>
                  <p className="text-xs text-gray-500 dark:text-gray-400">
                    Today at 10:23 AM
                  </p>
                </div>
              </div>
              
              <div className="flex items-start">
                <div className="flex-shrink-0 h-10 w-10 rounded-full bg-blue-100 dark:bg-blue-900/30 flex items-center justify-center text-blue-600 dark:text-blue-400">
                  <svg className="h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z" />
                  </svg>
                </div>
                <div className="ml-4">
                  <p className="text-sm font-medium text-gray-900 dark:text-gray-100">
                    Updated mission parameters for Mars Sample Return
                  </p>
                  <p className="text-xs text-gray-500 dark:text-gray-400">
                    Yesterday at 3:47 PM
                  </p>
                </div>
              </div>
              
              <div className="flex items-start">
                <div className="flex-shrink-0 h-10 w-10 rounded-full bg-purple-100 dark:bg-purple-900/30 flex items-center justify-center text-purple-600 dark:text-purple-400">
                  <svg className="h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z" />
                  </svg>
                </div>
                <div className="ml-4">
                  <p className="text-sm font-medium text-gray-900 dark:text-gray-100">
                    Joined Satellite Recovery Team
                  </p>
                  <p className="text-xs text-gray-500 dark:text-gray-400">
                    Feb 15, 2023
                  </p>
                </div>
              </div>
            </div>
            
            <Button variant="ghost" size="sm" className="mt-4">
              View All Activity
            </Button>
          </Card>
          
          {/* Skills and expertise */}
          <Card title="Skills & Expertise">
            <div className="flex flex-wrap gap-2">
              <span className="px-3 py-1 bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300 text-sm rounded-full">
                Launch Procedures
              </span>
              <span className="px-3 py-1 bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300 text-sm rounded-full">
                Mission Planning
              </span>
              <span className="px-3 py-1 bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300 text-sm rounded-full">
                Risk Assessment
              </span>
              <span className="px-3 py-1 bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300 text-sm rounded-full">
                Flight Systems
              </span>
              <span className="px-3 py-1 bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300 text-sm rounded-full">
                Team Leadership
              </span>
              <span className="px-3 py-1 bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300 text-sm rounded-full">
                Crisis Management
              </span>
              <span className="px-3 py-1 bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300 text-sm rounded-full">
                Orbital Mechanics
              </span>
              <span className="px-3 py-1 bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300 text-sm rounded-full">
                Communication Systems
              </span>
            </div>
          </Card>
        </div>
      </div>
    </PageContainer>
  );
}
