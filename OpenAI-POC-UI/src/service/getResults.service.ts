import axios from 'axios';

const API_URL = import.meta.env.VITE_SERVER_DOMAIN;

export const getResults = async () => {
    try {
        const response = await axios.get(`${API_URL}/matches`);
        return response.data;
      } catch (error) {
        if (axios.isAxiosError(error)) {
          console.error(
            'Error fetching matches:',
            error.response?.data || error.message
          );
        } else {
          console.error('Unknown error while fetching matches:', error);
        }
        throw error;
      }
}