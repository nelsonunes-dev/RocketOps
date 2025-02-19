'use client';

import { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import Link from 'next/link';
import PageContainer from '@/components/common/PageContainer';
import Card from '@/components/common/Card';
import Button from '@/components/common/Button';

// Switch to a JavaScript file without TypeScript annotations
// This should avoid type checking issues completely

// In a real app, this would be fetched from an API
const getLaunchData = (id) => {
  // Mock data for demonstration purposes
  return {
    id,
    name: 'Starlink 5-10',
    vehicle: 'Falcon 9',
    launchSiteId: 'KSC',
    launchDate: '2025-03-15T14:30:00',
    status: 'scheduled',
    description: 'Deployment of 60 Starlink satellites to low Earth orbit.',
    missionId: 'mission-123',
    payload: [
      { id: 'payload-1', name: 'Starlink satellites', type: 'Communication', mass: 15400 }
    ],
  };
};

// Mock data for dropdowns
const vehicles = [
  { id: 'falcon9', name: 'Falcon 9' },
  { id: 'falconheavy', name: 'Falcon Heavy' },
  { id: 'starship', name: 'Starship' }
];

const launchSites = [
  { id: 'KSC', name: 'Kennedy Space Center' },
  { id: 'VAFB', name: 'Vandenberg Air Force Base' },
  { id: 'CCSFS', name: 'Cape Canaveral Space Force Station' }
];

const statuses = [
  { id: 'scheduled', name: 'Scheduled' },
  { id: 'countdown', name: 'Countdown' },
  { id: 'delayed', name: 'Delayed' },
  { id: 'aborted', name: 'Aborted' }
];

const missions = [
  { id: 'mission-123', name: 'Starlink Group 5' },
  { id: 'mission-124', name: 'Commercial Resupply' },
  { id: 'mission-125', name: 'Crew Rotation' }
];

// Use plain JavaScript without TypeScript annotations
export default function EditLaunchPage({ params }) {
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);
  const [formData, setFormData] = useState({
    name: '',
    vehicle: '',
    launchSiteId: '',
    launchDate: '',
    status: '',
    description: '',
    missionId: ''
  });
  const [errors, setErrors] = useState({});

  useEffect(() => {
    // In a real app, this would be a data fetching call
    const launch = getLaunchData(params.id);
    setFormData({
      name: launch.name,
      vehicle: launch.vehicle,
      launchSiteId: launch.launchSiteId,
      launchDate: launch.launchDate,
      status: launch.status,
      description: launch.description,
      missionId: launch.missionId
    });
  }, [params.id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
    
    // Clear error when field is edited
    if (errors[name]) {
      setErrors(prev => {
        const newErrors = { ...prev };
        delete newErrors[name];
        return newErrors;
      });
    }
  };

  const validateForm = () => {
    const newErrors = {};
    
    if (!formData.name.trim()) {
      newErrors.name = 'Launch name is required';
    }
    
    if (!formData.vehicle) {
      newErrors.vehicle = 'Vehicle is required';
    }
    
    if (!formData.launchSiteId) {
      newErrors.launchSiteId = 'Launch site is required';
    }
    
    if (!formData.launchDate) {
      newErrors.launchDate = 'Launch date is required';
    }
    
    if (!formData.status) {
      newErrors.status = 'Status is required';
    }
    
    if (!formData.missionId) {
      newErrors.missionId = 'Mission is required';
    }
    
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!validateForm()) {
      return;
    }
    
    setIsLoading(true);
    
    try {
      // Simulate API call
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      // In a real app, this would be an API call to update the launch
      console.log('Saving launch data:', formData);
      
      // Redirect back to launch details
      router.push(`/launches/${params.id}`);
    } catch (error) {
      console.error('Error saving launch:', error);
      // Handle error state
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <PageContainer>
      <div className="mb-6 flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
        <div>
          <div className="flex items-center mb-2">
            <Link href={`/launches/${params.id}`} className="text-primary-600 dark:text-primary-400 mr-2" aria-label="Back to launch details">
              <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                <path fillRule="evenodd" d="M9.707 16.707a1 1 0 01-1.414 0l-6-6a1 1 0 010-1.414l6-6a1 1 0 011.414 1.414L5.414 9H17a1 1 0 110 2H5.414l4.293 4.293a1 1 0 010 1.414z" clipRule="evenodd" />
              </svg>
            </Link>
            <h1 className="text-2xl font-bold text-gray-900 dark:text-white">Edit Launch: {formData.name}</h1>
          </div>
          <p className="text-gray-600 dark:text-gray-400">
            Update launch details and configuration
          </p>
        </div>
      </div>

      <form onSubmit={handleSubmit}>
        <div className="space-y-6">
          <Card title="Basic Information">
            <div className="grid grid-cols-1 gap-6 lg:grid-cols-2">
              <div>
                <label htmlFor="name" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                  Launch Name*
                </label>
                <input
                  type="text"
                  id="name"
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                  className={`mt-1 block w-full rounded-md shadow-sm sm:text-sm
                    ${errors.name 
                      ? 'border-red-300 focus:border-red-500 focus:ring-red-500 dark:border-red-700' 
                      : 'border-gray-300 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-700'}
                    dark:bg-gray-800 dark:text-white`}
                />
                {errors.name && (
                  <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.name}</p>
                )}
              </div>
              
              <div>
                <label htmlFor="vehicle" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                  Vehicle*
                </label>
                <select
                  id="vehicle"
                  name="vehicle"
                  value={formData.vehicle}
                  onChange={handleChange}
                  className={`mt-1 block w-full rounded-md shadow-sm sm:text-sm
                    ${errors.vehicle 
                      ? 'border-red-300 focus:border-red-500 focus:ring-red-500 dark:border-red-700' 
                      : 'border-gray-300 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-700'}
                    dark:bg-gray-800 dark:text-white`}
                >
                  <option value="">Select vehicle</option>
                  {vehicles.map(vehicle => (
                    <option key={vehicle.id} value={vehicle.name}>{vehicle.name}</option>
                  ))}
                </select>
                {errors.vehicle && (
                  <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.vehicle}</p>
                )}
              </div>
              
              <div>
                <label htmlFor="launchSiteId" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                  Launch Site*
                </label>
                <select
                  id="launchSiteId"
                  name="launchSiteId"
                  value={formData.launchSiteId}
                  onChange={handleChange}
                  className={`mt-1 block w-full rounded-md shadow-sm sm:text-sm
                    ${errors.launchSiteId 
                      ? 'border-red-300 focus:border-red-500 focus:ring-red-500 dark:border-red-700' 
                      : 'border-gray-300 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-700'}
                    dark:bg-gray-800 dark:text-white`}
                >
                  <option value="">Select launch site</option>
                  {launchSites.map(site => (
                    <option key={site.id} value={site.id}>{site.name}</option>
                  ))}
                </select>
                {errors.launchSiteId && (
                  <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.launchSiteId}</p>
                )}
              </div>
              
              <div>
                <label htmlFor="launchDate" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                  Launch Date & Time*
                </label>
                <input
                  type="datetime-local"
                  id="launchDate"
                  name="launchDate"
                  value={formData.launchDate}
                  onChange={handleChange}
                  className={`mt-1 block w-full rounded-md shadow-sm sm:text-sm
                    ${errors.launchDate 
                      ? 'border-red-300 focus:border-red-500 focus:ring-red-500 dark:border-red-700' 
                      : 'border-gray-300 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-700'}
                    dark:bg-gray-800 dark:text-white`}
                />
                {errors.launchDate && (
                  <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.launchDate}</p>
                )}
              </div>
              
              <div>
                <label htmlFor="status" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                  Status*
                </label>
                <select
                  id="status"
                  name="status"
                  value={formData.status}
                  onChange={handleChange}
                  className={`mt-1 block w-full rounded-md shadow-sm sm:text-sm
                    ${errors.status 
                      ? 'border-red-300 focus:border-red-500 focus:ring-red-500 dark:border-red-700' 
                      : 'border-gray-300 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-700'}
                    dark:bg-gray-800 dark:text-white`}
                >
                  <option value="">Select status</option>
                  {statuses.map(status => (
                    <option key={status.id} value={status.id}>{status.name}</option>
                  ))}
                </select>
                {errors.status && (
                  <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.status}</p>
                )}
              </div>
              
              <div>
                <label htmlFor="missionId" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                  Mission*
                </label>
                <select
                  id="missionId"
                  name="missionId"
                  value={formData.missionId}
                  onChange={handleChange}
                  className={`mt-1 block w-full rounded-md shadow-sm sm:text-sm
                    ${errors.missionId 
                      ? 'border-red-300 focus:border-red-500 focus:ring-red-500 dark:border-red-700' 
                      : 'border-gray-300 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-700'}
                    dark:bg-gray-800 dark:text-white`}
                >
                  <option value="">Select mission</option>
                  {missions.map(mission => (
                    <option key={mission.id} value={mission.id}>{mission.name}</option>
                  ))}
                </select>
                {errors.missionId && (
                  <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.missionId}</p>
                )}
              </div>
              
              <div className="lg:col-span-2">
                <label htmlFor="description" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                  Description
                </label>
                <textarea
                  id="description"
                  name="description"
                  rows={4}
                  value={formData.description}
                  onChange={handleChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-primary-500 focus:ring-primary-500 sm:text-sm dark:border-gray-700 dark:bg-gray-800 dark:text-white"
                />
              </div>
            </div>
          </Card>

          <div className="flex justify-end space-x-3">
            <Button
              type="button"
              variant="secondary"
              onClick={() => router.push(`/launches/${params.id}`)}
            >
              Cancel
            </Button>
            <Button
              type="submit"
              variant="primary"
              isLoading={isLoading}
            >
              Save Changes
            </Button>
          </div>
        </div>
      </form>
    </PageContainer>
  );
}
