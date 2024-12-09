import axios from 'axios';

const API_URL = import.meta.env.VITE_SERVER_DOMAIN;

export const addCompetition = async (competitionData: { Name: string }) => {
  try {
    const response = await axios.post(`${API_URL}/competitions`, competitionData);
    return response;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error('Error adding competition:', error.response?.data || error.message);
    } else {
      console.error('Unknown error while adding competition:', error);
    }
    throw error;
  }
};