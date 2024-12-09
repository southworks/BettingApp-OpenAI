import axios from 'axios';
import { MatchWithLineup } from '../types/types';

const API_URL = import.meta.env.VITE_SERVER_DOMAIN;

export const matchWinner = async (matchData: MatchWithLineup) => {
    try {
        const response = await axios.post(`${API_URL}/pricing/match-winner`, matchData);
        return response;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.error(
                'Error calculating match winner:',
                error.response?.data || error.message
            );
        } else {
            console.error('Unknown error while calculating match winner:', error);
        }
        throw error;
    }

}