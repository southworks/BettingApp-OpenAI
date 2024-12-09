import axios from 'axios';

const API_URL = import.meta.env.VITE_SERVER_DOMAIN;

export const getCompetitions = async () => {
    try {
      const response = await axios.get(`${API_URL}/competitions`);
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        console.error(
          'Error fetching competitions:',
          error.response?.data || error.message
        );
      } else {
        console.error('Unknown error while fetching competitions:', error);
      }
      throw error;
    }
  };
  