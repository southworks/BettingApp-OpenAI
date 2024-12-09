import React from 'react';
import { TeamSelectionProps } from '../types/types';

const TeamSelection: React.FC<TeamSelectionProps> = ({ teams, selectedTeam, onSelectTeam, onConfirmSelection }) => {
  const handleTeamSelect = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const team = teams.find(t => t.name === e.target.value) || null;
    onSelectTeam(team);
  };

  return (
    <div className="p-4 flex flex-col items-center h-[480px] w-[400px] justify-center bg-opacity-0">
      <select onChange={handleTeamSelect} className="mb-4 p-2 bg-white border border-gray-300 rounded">
        <option value="">Select a team</option>
        {teams.map(team => (
          <option key={team.name} value={team.name}>
            {team.name}
          </option>
        ))}
      </select>

      {selectedTeam && (
        <div className="flex flex-col items-center mb-4 h-[250px] w-[400px] justify-center">
          <h2 className="text-lg font-bold mb-2">{selectedTeam.name}</h2>
        </div>
      )}

      <button
        onClick={onConfirmSelection}
        disabled={!selectedTeam}
        className={`mt-4 px-3 py-1 text-white font-bold rounded-lg text-sm ${
          selectedTeam ? 'bg-blue-500 hover:bg-blue-700' : 'bg-gray-400 cursor-not-allowed'
        }`}
        style={{ boxShadow: selectedTeam ? '0 4px 8px rgba(0, 0, 0, 0.2)' : 'none' }}
      >
        Confirm Selection
      </button>
    </div>
  );
};

export default TeamSelection;
