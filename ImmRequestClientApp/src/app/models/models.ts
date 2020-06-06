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

export interface Button {
  buttonTooltip: string;
  iconName: string;
  callback: (element:any) => void;
}

export interface Column {
  columnClass: string;
  columnName: string;
  hasButtons: boolean;
}

export interface BaseField{
  id?: number,
  name: string,
  range: Range,
  dataType: DataType,
  rangeValues: string[]

}

export enum DataType{
  Number, 
  Text,
  DateTime, 
  Bool
}

export interface Range{
  
}