import { useState, useCallback } from 'react';

interface ApiOptions<T> {
  url: string;
  method?: 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH';
  body?: unknown;
  headers?: Record<string, string>;
  onSuccess?: (data: T) => void;
  onError?: (error: Error) => void;
}

interface ApiState<T> {
  data: T | null;
  isLoading: boolean;
  error: Error | null;
}

export function useApi<T>() {
  const [state, setState] = useState<ApiState<T>>({
    data: null,
    isLoading: false,
    error: null,
  });

  const fetchData = useCallback(async (options: ApiOptions<T>) => {
    const {
      url,
      method = 'GET',
      body,
      headers = {},
      onSuccess,
      onError,
    } = options;

    setState(prev => ({ ...prev, isLoading: true, error: null }));

    try {
      const defaultHeaders: Record<string, string> = {
        'Content-Type': 'application/json',
        ...headers,
      };

      // Get token from localStorage
      const token = localStorage.getItem('auth_token');
      if (token) {
        defaultHeaders['Authorization'] = `Bearer ${token}`;
      }

      const response = await fetch(url, {
        method,
        headers: defaultHeaders,
        body: body ? JSON.stringify(body) : undefined,
      });

      if (!response.ok) {
        let errorMessage = 'An error occurred';
        try {
          const errorData = await response.json();
          errorMessage = errorData.message || errorData.error || errorMessage;
        } catch {
          // If JSON parsing fails, use status text
          errorMessage = response.statusText || errorMessage;
        }

        throw new Error(`${response.status}: ${errorMessage}`);
      }

      let data;
      const contentType = response.headers.get('content-type');
      if (contentType && contentType.includes('application/json')) {
        data = await response.json();
      } else {
        data = await response.text();
      }

      setState({ data: data as T, isLoading: false, error: null });
      
      if (onSuccess) {
        onSuccess(data as T);
      }
      
      return data as T;
    } catch (error) {
      const errorObj = error instanceof Error ? error : new Error(String(error));
      
      setState({ data: null, isLoading: false, error: errorObj });
      
      if (onError) {
        onError(errorObj);
      }
      
      throw errorObj;
    }
  }, []);

  const resetState = useCallback(() => {
    setState({ data: null, isLoading: false, error: null });
  }, []);

  return {
    ...state,
    fetchData,
    resetState,
  };
}

export default useApi;
