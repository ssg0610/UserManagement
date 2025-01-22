import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from 'axios';

const ENDPOINTS = {
  USERS: 'api/users',
  DEPARTMENTS: 'api/departments'
};

@Injectable({
  providedIn: 'root'
})
export class AxiosService {
  private axiosInstance: AxiosInstance;

  constructor() {
    this.axiosInstance = axios.create({
      baseURL: 'https://localhost:7023/'
    });
  }

  getUsers() {
    return this.axiosInstance.get(ENDPOINTS.USERS);
  }

  getUsersWithDepartments() {
    return this.axiosInstance.get(`${ENDPOINTS.USERS}/departments`)
  }

  getUser(id: number) {
    return this.axiosInstance.get(`${ENDPOINTS.USERS}/${id}`);
  }

  getUserWithDepartments(id: number) {
    return this.axiosInstance.get(`${ENDPOINTS.USERS}/${id}/departments`)
  }

  //createUser(user: User) {
  //  
  //}

  getdepartments() {
    return this.axiosInstance.get(ENDPOINTS.DEPARTMENTS);
  }

}