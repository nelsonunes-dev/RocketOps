import { Metadata } from 'next';
import PageContainer from '@/components/common/PageContainer';
import Card from '@/components/common/Card';
import Button from '@/components/common/Button';

export const metadata: Metadata = {
  title: 'Missions',
  description: 'Manage ongoing and planned missions',
};

export default function MissionsPage() {
  return (
    <PageContainer title="Missions">
      <div className="flex justify-between items-center mb-6">
        <div>
          <p className="text-gray-600 dark:text-gray-400">
            Track and manage mission objectives, timelines, and resources
          </p>
        </div>
        <Button variant="primary">
          Create New Mission
        </Button>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6 mb-6">
        <Card title="Active Missions" className="h-full">
          <p className="text-gray-600 dark:text-gray-400">
            Active missions will be displayed here. This page is under construction.
          </p>
        </Card>

        <Card title="Mission Statistics" className="h-full">
          <p className="text-gray-600 dark:text-gray-400">
            Mission statistics will be displayed here. This page is under construction.
          </p>
        </Card>
      </div>

      <Card title="All Missions">
        <p className="text-gray-600 dark:text-gray-400">
          A table of all missions will be displayed here. This page is under construction.
        </p>
      </Card>
    </PageContainer>
  );
}
