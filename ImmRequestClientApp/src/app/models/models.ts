export interface Session {
    id?: number;
    username?: string;
    email: string;
    password: string;
    token?: string;
}

export interface SnackbarInput{
  message: string;
  action: string;
  config: {}
}

export interface Admin {
  id: string;
  username: string;
  email: string;
  password: string;
}