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

export interface RadioButton {
  id: number;
  checked: boolean;
  name: string;
}

export interface Column {
  columnClass: string;
  columnName: string;
  hasButtons: boolean;
}

export interface TopicType{
  id?: number,
  name: string, 
  fields: BaseField[]
}

export interface BaseField{
  id?: number,
  parentTypeId:number,
  name: string,
  dataType: DataType,
  multipleValues: boolean,
  rangeValues: string[]

}

export interface Area{
  id: number,
  name: string,
  topics: Topic[]
}

export interface Topic{
  id: number,
  areaId: number,
  name: string,
  types: TopicType[]
}

export interface RequestValue{
  fieldId:number;
  name: string;
  value:string[];
}

export interface CitizenRequest{
  description: string,
  citizenName: string,
  email: string,
  phone: string,
  areaId: number,
  topicId: number,
  topicTypeId: number
  values: RequestValue[]
}

export enum DataType{
  Number, 
  Text,
  DateTime, 
  Bool
}
