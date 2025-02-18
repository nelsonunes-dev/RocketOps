// API Routes
export enum ApiRoutes {
    // Auth
    LOGIN = '/api/auth/login',
    REGISTER = '/api/auth/register',
    LOGOUT = '/api/auth/logout',
    REFRESH_TOKEN = '/api/auth/refresh',
    FORGOT_PASSWORD = '/api/auth/forgot-password',
    RESET_PASSWORD = '/api/auth/reset-password',
    VERIFY_EMAIL = '/api/auth/verify-email',
    ME = '/api/auth/me',
    
    // Users
    USERS = '/api/users',
    USER_DETAIL = '/api/users/:id',
    
    // Missions
    MISSIONS = '/api/missions',
    MISSION_DETAIL = '/api/missions/:id',
    MISSION_TEAMS = '/api/missions/:id/teams',
    
    // Launches
    LAUNCHES = '/api/launches',
    LAUNCH_DETAIL = '/api/launches/:id',
    UPCOMING_LAUNCHES = '/api/launches/upcoming',
    PAST_LAUNCHES = '/api/launches/past',
    
    // Vehicles
    VEHICLES = '/api/vehicles',
    VEHICLE_DETAIL = '/api/vehicles/:id',
    
    // Payloads
    PAYLOADS = '/api/payloads',
    PAYLOAD_DETAIL = '/api/payloads/:id',
    
    // Launch Sites
    LAUNCH_SITES = '/api/launch-sites',
    LAUNCH_SITE_DETAIL = '/api/launch-sites/:id',
    
    // Weather
    WEATHER_FORECAST = '/api/weather/forecast/:siteId',
    WEATHER_CURRENT = '/api/weather/current/:siteId',
    
    // Dashboard
    DASHBOARD_STATS = '/api/dashboard/stats',
    DASHBOARD_WIDGETS = '/api/dashboard/widgets',
    
    // Reports
    REPORTS = '/api/reports',
    REPORT_DETAIL = '/api/reports/:id',
    GENERATE_REPORT = '/api/reports/generate',
    
    // Settings
    SETTINGS = '/api/settings',
    NOTIFICATION_SETTINGS = '/api/settings/notifications',
  }
  
  // API Error types
  export interface ApiError {
    status: number;
    message: string;
    errors?: Record<string, string[]>;
    code?: string;
  }
  
  // Request params
  export interface PaginationParams {
    page?: number;
    limit?: number;
    sort?: string;
    order?: 'asc' | 'desc';
  }
  
  export interface SearchParams {
    search?: string;
    filter?: Record<string, string | number | boolean | string[]>;
  }
  
  export type RequestParams = PaginationParams & SearchParams;
  
  // Auth responses
  export interface LoginResponse {
    token: string;
    refreshToken: string;
    expiresAt: number;
    user: {
      id: string;
      email: string;
      firstName: string;
      lastName: string;
      role: string;
    };
  }
  
  // Helper function to build API URLs with path parameters
  export const buildApiUrl = (
    route: string, 
    params?: Record<string, string | number>
  ): string => {
    let url = route;
    
    if (params) {
      Object.entries(params).forEach(([key, value]) => {
        url = url.replace(`:${key}`, String(value));
      });
    }
    
    return url;
  };