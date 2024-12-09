import axios from 'axios';

const API_URL = import.meta.env.VITE_SERVER_DOMAIN;

export const choseLineups = async (matchData: {
  homeTeam: { name: string; position: string }[];
  awayTeam: { name: string; position: string }[];
  competition: string;
}) => {
  try {
    const response = await axios.post(`${API_URL}/teams/lineups`, matchData);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error(
        'Error choosing lineups:',
        error.response?.data || error.message
      );
    } else {
      console.error('Unknown error while choosing lineups:', error);
    }
    throw error;
  }
};
