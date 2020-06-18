export interface Session {
  id?: number;
  username?: string;
  email: string;
  password: string;
  token?: string;
}

export interface SnackbarInput {
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
  callback: (element: any) => void;
}

export interface Column {
  columnClass: string;
  columnName: string;
  hasButtons: boolean;
}

export interface TopicType {
  id?: number,
  name: string,
  fields: BaseField[]
}

export interface BaseField {
  id?: number,
  name: string,
  dataType: DataType,
  multipleValues: boolean,
  rangeValues: string[]

}

export interface Area {
  id: number,
  name: string,
  topics: Topic[]
}

export interface Topic {
  id: number,
  areaId: number,
  name: string,
  types: TopicType[]
}

export enum DataType {
  Number,
  Text,
  DateTime,
  Bool
}

export enum RequestStatus {
  Created,
  OnRevision,
  Acepted,
  Declined,
  Ended
}

export interface RequestSummary {
  count: number,
  status: RequestStatus,
  RequestNumbers: number[]
}

export interface TypeSummary{
  count: number,
  name: string,
  TypeCreatedAt: Date
}

export interface ReportRequest{
  email: string,
  start: Date,
  end: Date
}
