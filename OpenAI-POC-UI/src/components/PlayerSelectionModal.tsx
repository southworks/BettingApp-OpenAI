import React from 'react';
import { PlayerSelectionModalProps } from '../types/types';

const PlayerSelectionModal: React.FC<PlayerSelectionModalProps> = ({
  position,
  filteredPlayers,
  onCancel,
  onSelectPlayer,
  isPlayerSelected,
}) => {
  return position ? (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
      <div className="bg-white p-4 rounded-lg w-[300px]">
        <h2 className="text-lg font-bold mb-4">Select {position}</h2>
        <ul>
          {filteredPlayers.map(player => (
            <li
              key={player.name}
              className={`cursor-pointer p-2 border-b border-gray-300 ${isPlayerSelected(player) ? 'text-gray-500 cursor-not-allowed' : ''}`}
              onClick={() => !isPlayerSelected(player) && onSelectPlayer(player)}
            >
              {player.name} {isPlayerSelected(player) ? '(Selected)' : ''}
            </li>
          ))}
        </ul>
        <button className="mt-4 px-3 py-1 bg-red-500 text-white rounded-lg" onClick={onCancel}>
          Cancel
        </button>
      </div>
    </div>
  ) : null;
};

export default PlayerSelectionModal;
