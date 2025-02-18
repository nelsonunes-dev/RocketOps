/**
 * Environment variables utility
 * 
 * This file provides type-safe access to environment variables
 * and ensures defaults are provided when variables are not set.
 */

// API Configuration
export const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:8000/api';
export const API_TIMEOUT = Number(process.env.NEXT_PUBLIC_API_TIMEOUT || 30000);

// Authentication
export const AUTH_COOKIE_NAME = process.env.NEXT_PUBLIC_AUTH_COOKIE_NAME || 'rocketops_auth_token';
export const AUTH_COOKIE_EXPIRES = Number(process.env.NEXT_PUBLIC_AUTH_COOKIE_EXPIRES || 7);

// Feature Flags
export const ENABLE_ANALYTICS = process.env.NEXT_PUBLIC_ENABLE_ANALYTICS === 'true';
export const ENABLE_NOTIFICATIONS = process.env.NEXT_PUBLIC_ENABLE_NOTIFICATIONS === 'true';
export const MAINTENANCE_MODE = process.env.NEXT_PUBLIC_MAINTENANCE_MODE === 'true';

// External Services
export const WEATHER_API_KEY = process.env.NEXT_PUBLIC_WEATHER_API_KEY || '';
export const MAPBOX_TOKEN = process.env.NEXT_PUBLIC_MAPBOX_TOKEN || '';

// App Configuration
export const APP_NAME = process.env.NEXT_PUBLIC_APP_NAME || 'RocketOps';
export const APP_VERSION = process.env.NEXT_PUBLIC_APP_VERSION || '0.1.0';
export const APP_ENVIRONMENT = process.env.NEXT_PUBLIC_APP_ENVIRONMENT || 'development';

// Environment checks
export const IS_DEVELOPMENT = APP_ENVIRONMENT === 'development';
export const IS_PRODUCTION = APP_ENVIRONMENT === 'production';
export const IS_TEST = APP_ENVIRONMENT === 'test';

/**
 * Get all environment variables for debugging
 * (Don't expose sensitive values in production)
 */
export const getEnvConfig = () => {
  if (IS_PRODUCTION) {
    return {
      environment: APP_ENVIRONMENT,
      version: APP_VERSION,
    };
  }

  return {
    apiUrl: API_URL,
    apiTimeout: API_TIMEOUT,
    authCookieName: AUTH_COOKIE_NAME,
    authCookieExpires: AUTH_COOKIE_EXPIRES,
    enableAnalytics: ENABLE_ANALYTICS,
    enableNotifications: ENABLE_NOTIFICATIONS,
    maintenanceMode: MAINTENANCE_MODE,
    environment: APP_ENVIRONMENT,
    version: APP_VERSION,
  };
};
