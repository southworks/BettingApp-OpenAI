import React from 'react';
import { Player } from '../types/types';

interface ReadyLayoutProps {
  selectedPlayers: Player[]; 
  reserves: Player[]; 
}

const ReadyLayout: React.FC<ReadyLayoutProps> = ({ selectedPlayers, reserves }) => {
  return (
    <div>
      <h3 className="text-lg font-bold mb-4 text-center">Starting 11</h3>
      <div className="grid grid-cols-4 gap-4 mb-6">
        {selectedPlayers.map((player, index) => (
          <div
            key={index}
            className="bg-[#fde047] p-1 rounded-xl shadow-md flex flex-col items-center justify-center border-2 border-black"
          >
            <h4 className="text-center font-bold text-[10px]">{player.name}</h4>
            <p className="text-center text-[10px]">{player.position}</p>
          </div>
        ))}
      </div>

      <h3 className="text-lg font-bold mb-4 text-center">Reserves</h3>
      <div className="grid grid-cols-5 gap-4">
        {reserves.map((player, index) => (
          <div
            key={index}
            className="bg-[#fde047] p-4 rounded-xl shadow-md flex flex-col items-center justify-center border-2 border-black"
          >
            <h4 className="text-center font-bold text-[10px]">{player.name}</h4>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ReadyLayout;
