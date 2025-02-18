import { useState, useEffect, useCallback } from 'react';

type SetValue<T> = T | ((val: T) => T);

function useLocalStorage<T>(
  key: string,
  initialValue: T
): [T, (value: SetValue<T>) => void] {
  // Get stored value from localStorage or use initialValue
  const readValue = useCallback((): T => {
    // Prevent build error on server
    if (typeof window === 'undefined') {
      return initialValue;
    }

    try {
      const item = window.localStorage.getItem(key);
      return item ? (JSON.parse(item) as T) : initialValue;
    } catch (error) {
      console.warn(`Error reading localStorage key "${key}":`, error);
      return initialValue;
    }
  }, [key, initialValue]);

  const [storedValue, setStoredValue] = useState<T>(readValue);

  // Return a wrapped version of useState's setter function that persists the new value to localStorage
  const setValue = useCallback((value: SetValue<T>) => {
    // Prevent build error on server
    if (typeof window === 'undefined') {
      console.warn(
        `Tried setting localStorage key "${key}" even though environment is not a client`,
      );
      return;
    }

    try {
      // Allow value to be a function so we have the same API as useState
      const valueToStore =
        value instanceof Function ? value(storedValue) : value;
      
      // Save to state
      setStoredValue(valueToStore);
      
      // Save to localStorage
      window.localStorage.setItem(key, JSON.stringify(valueToStore));

      // Dispatch a custom event so other instances of this hook can update
      window.dispatchEvent(new Event('local-storage'));
    } catch (error) {
      console.warn(`Error setting localStorage key "${key}":`, error);
    }
  }, [key, storedValue]);

  // Listen for changes to this localStorage key in other documents/tabs
  useEffect(() => {
    // Sync state with localStorage when storage changes in another document
    const handleStorageChange = () => {
      setStoredValue(readValue());
    };
    
    // Listen for the storage event
    window.addEventListener('storage', handleStorageChange);
    window.addEventListener('local-storage', handleStorageChange);
    
    return () => {
      window.removeEventListener('storage', handleStorageChange);
      window.removeEventListener('local-storage', handleStorageChange);
    };
  }, [readValue]);

  return [storedValue, setValue];
}

export default useLocalStorage;
