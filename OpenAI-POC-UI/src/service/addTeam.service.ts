import axios from 'axios';

const API_URL = import.meta.env.VITE_SERVER_DOMAIN;

export const addTeam = async (teamData: { Name: string; Squad: { Name: string; Position: string }[] }) => {
    try {
      const response = await axios.post(`${API_URL}/teams`, teamData);
      return response;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        console.error('Error adding team:', error.response?.data || error.message);
      } else {
        console.error('Unknown error while adding team:', error);
      }
      throw error;
    }
  };