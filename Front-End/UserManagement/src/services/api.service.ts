import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from 'axios';
import { User } from '../models/user.model';
import { Department } from '../models/department.model';

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

  async getUsers() {
    const response = await this.axiosInstance.get(ENDPOINTS.USERS);

    return response.data
  }

  async getUsersWithDepartments() {
    const response = await this.axiosInstance.get(`${ENDPOINTS.USERS}/departments`)

    return response.data
  }

  async getUser(id: number) {
    const response = await this.axiosInstance.get(`${ENDPOINTS.USERS}/${id}`);

    return response.data
  }

  async getUserWithDepartments(id: number) {
    const response = await this.axiosInstance.get(`${ENDPOINTS.USERS}/${id}/departments`)

    return response.data
  }

  async createUser(user: any) {
    const response = await this.axiosInstance.post(`${ENDPOINTS.USERS}`, user)

    return response.data
  }

  async updateUser(id: number, user: any) {
    const response = await this.axiosInstance.put(`${ENDPOINTS.USERS}/${id}`, user)

    return response.data
  }

  async deleteUser(id: number) {
    const response = await this.axiosInstance.delete(`${ENDPOINTS.USERS}/${id}`)

    return response.data
  }

  // Metodos para departamentos
  async getDepartments() {
    const response = await this.axiosInstance.get(ENDPOINTS.DEPARTMENTS);
  
    return response.data
  }

  async getDepartment(id: number) {
    const response = await this.axiosInstance.get(`${ENDPOINTS.DEPARTMENTS}/${id}`)

    return response.data
  }

  async getDepartmentsWithUsers() {
    const response = await this.axiosInstance.get(`${ENDPOINTS.DEPARTMENTS}/users`)

    return response.data
  }

  async getDepartmentWithUser(id: number) {
    const response = await this.axiosInstance.get(`${ENDPOINTS.DEPARTMENTS}/${id}/users`)

    return response.data
  }

  async createDepartment(department: any) {
    const response = await this.axiosInstance.post(`${ENDPOINTS.DEPARTMENTS}`, department)

    return response.data
  }

  async updateDepartment(id: number, department: any) {
    const response = await this.axiosInstance.put(`${ENDPOINTS.DEPARTMENTS}/${id}`, department)

    return response.data
  }

  async deleteDepartment(id: number) {
    const response = await this.axiosInstance.delete(`${ENDPOINTS.DEPARTMENTS}/${id}`)

    return response.data
  }
}