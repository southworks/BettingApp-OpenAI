import React, { useState } from 'react';
import { Team, Player, TeamSelectorProps } from '../types/types';

const TeamSelector: React.FC<TeamSelectorProps> = ({ teams, isHomeTeam, onFinalSelection }) => {
  const [selectedTeam, setSelectedTeam] = useState<Team | null>(null);
  const [selectedPlayers, setSelectedPlayers] = useState<Player[]>([]);

  const handlePlayerSelect = (player: Player) => {
    if (selectedPlayers.some(p => p.name === player.name)) {
      setSelectedPlayers(prev => prev.filter(p => p.name !== player.name));
    } else if (selectedPlayers.length < 16) {
      setSelectedPlayers(prev => [...prev, player]);
    }
  };

  const handleConfirmSelection = () => {
    if (selectedTeam && selectedPlayers.length === 16) {
      onFinalSelection(selectedTeam.name, selectedPlayers, isHomeTeam);
      setSelectedPlayers([]);
      setSelectedTeam(null);
    }
  };

  return (
    <div>
      <h3>{isHomeTeam ? 'Select Home Team' : 'Select Away Team'}</h3>
      <div>
        <select
          onChange={(e) => setSelectedTeam(teams.find(team => team.name === e.target.value) || null)}
        >
          <option value="">Select a team</option>
          {teams.map(team => (
            <option key={team.name} value={team.name}>
              {team.name}
            </option>
          ))}
        </select>

        {selectedTeam && (
          <div>
            <h4>{selectedTeam.name} - Select 16 Players</h4>
            <ul>
              {selectedTeam.squad.map((player, index) => (
                <li
                  key={index}
                  className={`cursor-pointer p-2 border-b ${
                    selectedPlayers.some(p => p.name === player.name)
                      ? 'bg-green-500 text-white'
                      : 'bg-white text-black'
                  }`}
                  onClick={() => handlePlayerSelect(player)}
                >
                  {player.name} - {player.position}
                </li>
              ))}
            </ul>

            <button
              onClick={handleConfirmSelection}
              disabled={selectedPlayers.length !== 16}
              className="mt-4 px-3 py-1 bg-green-500 text-white rounded-lg"
            >
              Confirm Selection
            </button>
          </div>
        )}
      </div>
    </div>
  );
};

export default TeamSelector;
