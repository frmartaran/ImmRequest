export interface Session {
    id?: number;
    email: string;
    password: string;
    token?: string;
}

export interface SnackbarInput{
  message: string;
  action: string;
  config: {}
}