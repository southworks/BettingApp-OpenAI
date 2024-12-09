import axios from 'axios';
import { ResultTeam } from "../types/types";

const API_URL = import.meta.env.VITE_SERVER_DOMAIN;

interface MatchDto {
  home_team: ResultTeam;
  away_team: ResultTeam;
  match_type: string;
}

export const addMatch = async (matchData: MatchDto) => {
  try {
    const response = await axios.post(`${API_URL}/matches`, matchData);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Error adding match:", error.response?.data || error.message);
    } else {
      console.error("Unknown error while adding match:", error);
    }
    throw error;
  }
};
