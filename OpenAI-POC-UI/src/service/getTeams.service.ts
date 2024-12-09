import axios from 'axios';

const API_URL = import.meta.env.VITE_SERVER_DOMAIN;

export const getTeams = async () => {
    try {
      const response = await axios.get(`${API_URL}/teams`);
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        console.error(
          'Error fetching teams:',
          error.response?.data || error.message
        );
      } else {
        console.error('Unknown error while fetching teams:', error);
      }
      throw error;
    }
  };