import axios, { AxiosResponse } from 'axios'

const apiClient = axios.create({
  baseURL: 'https://localhost:5001/api',
  headers: {
    'Content-Type': 'application/json',
  },
})

export const getOperations = async (id: number): Promise<AxiosResponse> => apiClient.get(`/Operations/user/${id}`)
export const getNotifications= async (id: number): Promise<AxiosResponse> => apiClient.get(`/Notification/user/${id}`)
export const getGoals = async (id: number): Promise<AxiosResponse> => apiClient.get(`/Goals/user/${id}`)
export const getUserEmail = async (id: number): Promise<AxiosResponse> => apiClient.get(`/User/${id}/email`)

export const postGoal= async (data: any): Promise<AxiosResponse> => apiClient.post('/Goals', data)
export const postNotification= async (data: any): Promise<AxiosResponse> => apiClient.post('/Notification', data)
export const postOperation= async (data: any): Promise<AxiosResponse> => apiClient.post('/Operations', data)
export const postUser= async (data: any): Promise<AxiosResponse> => apiClient.post('/User', data)

export const deleteOperation = async (id: number): Promise<AxiosResponse> => apiClient.delete(`/Operations/${id}`);
export const deleteNotification = async (id: number): Promise<AxiosResponse> => apiClient.delete(`/Notification/${id}`);
export const deleteGoal = async (id: number): Promise<AxiosResponse> => apiClient.delete(`/Goals/${id}`);

export const updateOperation = async (id: number, data: any): Promise<AxiosResponse> => apiClient.put(`/Operations/${id}`, data);
export const updateNotification = async (id: number, data: any): Promise<AxiosResponse> => apiClient.put(`/Notification/${id}`, data);
export const updateGoal = async (id: number, data: any): Promise<AxiosResponse> => apiClient.put(`/Goals/${id}`, data);
export const updateUserPass = async (id: number, data: any): Promise<AxiosResponse> => apiClient.patch(`/User/${id}/password`, data);

export const authenticateUser = async (email: any, password: any): Promise<AxiosResponse> => {
    try {
      const response = await apiClient.post('/auth/login', { email, password });
      return response; // Returning the response to handle it in the component
    } catch (error) {
      throw error; // Rethrow the error to handle it in the component
    }
  }

