import axios, { AxiosResponse } from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:5001/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

export const fetchData = async (id: number): Promise<AxiosResponse> => apiClient.get(`/Operations/${id}`);

//export const postData = async (data: any): Promise<AxiosResponse> => apiClient.post('/endpoint', data);
