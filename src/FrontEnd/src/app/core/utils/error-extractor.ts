import { HttpErrorResponse } from '@angular/common/http';

export function extractErrorMessage(err: HttpErrorResponse): string {
  const errorResponse = err.error;

  if (errorResponse?.errors) {
    return Object.values(errorResponse.errors).flat().join(' ');
  }

  if (errorResponse?.error) {
    return errorResponse.error;
  }

  if (errorResponse?.detail) {
    return errorResponse.detail;
  }

  return err.message || 'An unexpected error occurred.';
}
