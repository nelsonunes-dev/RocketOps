// User related types
export interface User {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    role: UserRole;
    avatar?: string;
    createdAt: string;
    updatedAt: string;
  }
  
  export type UserRole = 'admin' | 'mission_control' | 'engineer' | 'analyst' | 'viewer';
  
  // Authentication related types
  export interface AuthState {
    user: User | null;
    token: string | null;
    isAuthenticated: boolean;
    isLoading: boolean;
    error: string | null;
  }
  
  export interface LoginCredentials {
    email: string;
    password: string;
  }
  
  export interface SignupData extends LoginCredentials {
    firstName: string;
    lastName: string;
  }
  
  // Mission related types
  export interface Mission {
    id: string;
    name: string;
    description: string;
    status: MissionStatus;
    startDate: string;
    endDate?: string;
    budget: number;
    teamSize: number;
    teamMembers: string[];
    launches: Launch[];
    createdAt: string;
    updatedAt: string;
  }
  
  export type MissionStatus = 
    | 'planning'
    | 'active'
    | 'on_hold'
    | 'completed'
    | 'cancelled';
  
  // Launch related types
  export interface Launch {
    id: string;
    missionId: string;
    name: string;
    description?: string;
    status: LaunchStatus;
    vehicle: Vehicle;
    payload: Payload[];
    launchDate: string;
    launchSite: LaunchSite;
    orbitType: OrbitType;
    weather: WeatherCondition;
    createdAt: string;
    updatedAt: string;
  }
  
  export type LaunchStatus = 
    | 'scheduled'
    | 'countdown'
    | 'in_flight'
    | 'successful'
    | 'partial_failure'
    | 'failure'
    | 'aborted'
    | 'delayed';
  
  export interface Vehicle {
    id: string;
    name: string;
    type: string;
    manufacturer: string;
    height: number;
    diameter: number;
    mass: number;
    stages: number;
    payload_capacity: number;
    success_rate: number;
    first_flight?: string;
    status: 'active' | 'in_development' | 'retired';
  }
  
  export interface Payload {
    id: string;
    name: string;
    type: string;
    mass: number;
    dimensions: {
      length: number;
      width: number;
      height: number;
    };
    customer: string;
    orbit?: OrbitType;
  }
  
  export interface LaunchSite {
    id: string;
    name: string;
    location: string;
    latitude: number;
    longitude: number;
    status: 'active' | 'under_construction' | 'inactive';
  }
  
  export type OrbitType = 
    | 'LEO'  // Low Earth Orbit
    | 'MEO'  // Medium Earth Orbit
    | 'GEO'  // Geostationary Orbit
    | 'HEO'  // Highly Elliptical Orbit
    | 'SSO'  // Sun-Synchronous Orbit
    | 'TLI'  // Trans-Lunar Injection
    | 'TMI'  // Trans-Mars Injection
    | 'GTO'  // Geostationary Transfer Orbit
    | 'other';
  
  export interface WeatherCondition {
    temperature: number;
    windSpeed: number;
    precipitation: number;
    visibility: number;
    status: 'favorable' | 'marginal' | 'unfavorable' | 'unknown';
  }
  
  // API response types
  export interface ApiResponse<T> {
    data: T;
    message?: string;
    success: boolean;
  }
  
  export interface PaginatedResponse<T> {
    data: T[];
    total: number;
    page: number;
    limit: number;
    totalPages: number;
  }
  
  // UI component types
  export interface TableColumn<T> {
    key: keyof T | string;
    header: string;
    width?: string;
    render?: (row: T) => React.ReactNode;
    sortable?: boolean;
    align?: 'left' | 'center' | 'right';
  }
  
  // Dashboard widget types
  export interface DashboardWidgetProps {
    id: string;
    title: string;
    type: 'chart' | 'status' | 'metric' | 'table' | 'countdown' | 'alert';
    size: 'small' | 'medium' | 'large';
    position: { x: number; y: number };
    data?: unknown;
    settings?: Record<string, unknown>;
  }
  
  // Form field types
  export interface FormField {
    name: string;
    label: string;
    type: 'text' | 'email' | 'password' | 'number' | 'select' | 'multiselect' | 
          'date' | 'datetime' | 'checkbox' | 'radio' | 'textarea' | 'file';
    placeholder?: string;
    defaultValue?: unknown;
    required?: boolean;
    disabled?: boolean;
    options?: Array<{value: string | number; label: string}>;
    validation?: {
      min?: number;
      max?: number;
      minLength?: number;
      maxLength?: number;
      pattern?: RegExp;
      message?: string;
    };
  }
  