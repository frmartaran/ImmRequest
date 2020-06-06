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

export interface BaseField{
  id?: number,
  name: string,
  range: Range,
  dataType: DataType

}

export enum DataType{
  Number, 
  Text,
  DateTime, 
  Bool
}

export interface Range{
  
}

