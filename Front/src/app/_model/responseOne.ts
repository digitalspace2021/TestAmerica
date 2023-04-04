export interface PageResponseOne<T> {
    message: string;
    status: boolean;
    count: number;
    data: T;
  }