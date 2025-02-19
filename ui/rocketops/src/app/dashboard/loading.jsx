import PageContainer from '@/components/common/PageContainer';

export default function DashboardLoading() {
  return (
    <PageContainer>
      <div className="animate-pulse">
        {/* Header skeleton */}
        <div className="h-8 bg-gray-200 dark:bg-gray-700 rounded-md w-1/3 mb-6"></div>
        
        {/* Status Overview skeleton */}
        <div className="mb-6">
          <div className="h-40 bg-gray-200 dark:bg-gray-700 rounded-lg w-full"></div>
        </div>
        
        {/* Cards skeleton */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          <div className="h-64 bg-gray-200 dark:bg-gray-700 rounded-lg"></div>
          <div className="h-64 bg-gray-200 dark:bg-gray-700 rounded-lg"></div>
          <div className="h-64 bg-gray-200 dark:bg-gray-700 rounded-lg"></div>
        </div>
      </div>
    </PageContainer>
  );
}
