import { Department } from "./department.model";

export interface User {
    id: number;  
    name: string;
    lastName?: string;
    phone?: string;
    userName: string;
    password: string;
    isDeleted: boolean;
    departments: Department[];
  }

export interface CreateUserDTO {
    name: string;
    lastName?: string;
    phone?: string;
    userName: string;
    password: string;
    isDeleted: boolean;
    departmentIds: number[];
}

export interface UpdateUserDTO {
    id: number,
    name: string;
    lastName?: string;
    phone?: string;
    userName: string;
    password: string;
    isDeleted: boolean;
    departmentIds: number[];
}