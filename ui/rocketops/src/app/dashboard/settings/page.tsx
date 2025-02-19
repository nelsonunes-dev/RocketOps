'use client';

import PageContainer from '@/components/common/PageContainer';
import Card from '@/components/common/Card';

export default function SettingsPage() {
  return (
    <PageContainer title="Settings">
      <div className="mb-6">
        <p className="text-gray-600 dark:text-gray-400">
          Configure your account, notification preferences, and application settings
        </p>
      </div>

      <div className="space-y-6">
        <Card title="Account Settings" className="h-full">
          <p className="text-gray-600 dark:text-gray-400 mb-4">
            Manage your account information and security settings
          </p>
          <div className="space-y-4">
            <div className="border-b border-gray-200 dark:border-gray-700 pb-4">
              <h3 className="text-md font-medium text-gray-800 dark:text-gray-200">Profile Information</h3>
              <p className="text-sm text-gray-600 dark:text-gray-400">
                Update your profile details and contact information
              </p>
            </div>
            <div className="border-b border-gray-200 dark:border-gray-700 pb-4">
              <h3 className="text-md font-medium text-gray-800 dark:text-gray-200">Password & Security</h3>
              <p className="text-sm text-gray-600 dark:text-gray-400">
                Change password, enable two-factor authentication, and manage security settings
              </p>
            </div>
            <div>
              <h3 className="text-md font-medium text-gray-800 dark:text-gray-200">Login Sessions</h3>
              <p className="text-sm text-gray-600 dark:text-gray-400">
                View and manage active login sessions across devices
              </p>
            </div>
          </div>
        </Card>

        <Card title="Notification Settings" className="h-full">
          <p className="text-gray-600 dark:text-gray-400">
            Configure how and when you receive notifications
          </p>
        </Card>

        <Card title="Display Settings" className="h-full">
          <p className="text-gray-600 dark:text-gray-400">
            Customize the appearance and behavior of the dashboard
          </p>
        </Card>

        <Card title="API & Integrations" className="h-full">
          <p className="text-gray-600 dark:text-gray-400">
            Manage API keys and third-party service integrations
          </p>
        </Card>
      </div>
    </PageContainer>
  );
}
