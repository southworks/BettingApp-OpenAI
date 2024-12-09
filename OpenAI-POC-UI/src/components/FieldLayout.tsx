import React from 'react';
import { FieldLayoutProps } from '../types/types';

const FieldLayout: React.FC<FieldLayoutProps> = ({ selectedPlayers, reserves, handlePositionClick }) => {
  const renderPlayer = (position: string, label: string) => (
    <div
      className="w-15 h-16 bg-[#fde047] flex items-center justify-center border-2 border-black rounded-2xl max-w-16 min-w-14 cursor-pointer"
      style={{
        boxShadow: '0 8px 16px rgba(0, 0, 0, 0.25)',
        transition: 'box-shadow 0.3s ease-in-out',
      }}
      onMouseEnter={(e) => (e.currentTarget.style.boxShadow = '0 12px 24px rgba(0, 0, 0, 0.75)')}
      onMouseLeave={(e) => (e.currentTarget.style.boxShadow = '0 8px 16px rgba(0, 0, 0, 0.25)')}
      onClick={() => handlePositionClick(position)}
    >
      {selectedPlayers[position]?.name || label}
    </div>
  );

  return (
    <div className="grid grid-rows-4 gap-2">
      <div className="flex justify-center">{renderPlayer('Goalkeeper', 'GK')}</div>

      <div className="flex justify-around">
        {['Left Defender', 'Center Defender 1', 'Center Defender 2', 'Right Defender'].map(position =>
          renderPlayer(position, position.slice(0, 2).toUpperCase())
        )}
      </div>

      <div className="flex justify-around">
        {['Left Midfielder', 'Center Midfielder', 'Right Midfielder'].map(position =>
          renderPlayer(position, position.slice(0, 2).toUpperCase())
        )}
      </div>

      <div className="flex justify-around">
        {['Left Forward', 'Striker', 'Right Forward'].map(position =>
          renderPlayer(position, position.slice(0, 2).toUpperCase())
        )}
      </div>

      {reserves.length > 0 && (
        <>
          <h3 className="text-xl font-bold mt-6 mb-2">Reserves</h3>
          <div className="grid grid-cols-5 gap-2">
            {reserves.map((reserve, index) => (
              <div
                key={index}
                className="w-15 h-16 bg-[#fde047] flex items-center justify-center border-2 border-black rounded-2xl max-w-16 min-w-14"
                style={{ boxShadow: '0 8px 16px rgba(0, 0, 0, 0.25)' }}
              >
                {reserve.name}
              </div>
            ))}
          </div>
        </>
      )}
    </div>
  );
};

export default FieldLayout;
