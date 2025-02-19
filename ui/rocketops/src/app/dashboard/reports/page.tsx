import { Metadata } from 'next';
import PageContainer from '@/components/common/PageContainer';
import Card from '@/components/common/Card';
import Button from '@/components/common/Button';

export const metadata: Metadata = {
  title: 'Reports',
  description: 'Generate and view mission and launch reports',
};

export default function ReportsPage() {
  return (
    <PageContainer title="Reports & Analytics">
      <div className="flex justify-between items-center mb-6">
        <div>
          <p className="text-gray-600 dark:text-gray-400">
            Generate reports and analyze mission and launch data
          </p>
        </div>
        <Button variant="primary">
          Generate New Report
        </Button>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-6">
        <Card title="Launch Success Rate" className="h-full">
          <div className="flex items-center justify-center h-48 bg-gray-100 dark:bg-gray-800 rounded-md">
            <p className="text-gray-500 dark:text-gray-400">Chart placeholder</p>
          </div>
        </Card>

        <Card title="Mission Completion" className="h-full">
          <div className="flex items-center justify-center h-48 bg-gray-100 dark:bg-gray-800 rounded-md">
            <p className="text-gray-500 dark:text-gray-400">Chart placeholder</p>
          </div>
        </Card>

        <Card title="Budget Utilization" className="h-full">
          <div className="flex items-center justify-center h-48 bg-gray-100 dark:bg-gray-800 rounded-md">
            <p className="text-gray-500 dark:text-gray-400">Chart placeholder</p>
          </div>
        </Card>
      </div>

      <Card title="Recent Reports">
        <p className="text-gray-600 dark:text-gray-400">
          A table of recent reports will be displayed here. This page is under construction.
        </p>
      </Card>
    </PageContainer>
  );
}
