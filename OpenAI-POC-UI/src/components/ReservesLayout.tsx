import React from 'react';
import { ReservesLayoutProps } from '../types/types';

const ReservesLayout: React.FC<ReservesLayoutProps> = ({ reserves, handlePositionClick }) => {
    return (
        <div className="flex flex-col items-center justify-center min-h-[240px]">
            <h3 className="text-xl font-bold mb-4">Select Reserves</h3>

            <div className="grid grid-cols-5 gap-2">
                {Array.from({ length: 5 }).map((_, index) => (
                    <div
                        key={index}
                        className="w-15 h-16 bg-[#fde047] flex items-center justify-center cursor-pointer border-2 border-black rounded-2xl max-w-16 min-w-14 text-[12px]"
                        style={{
                            boxShadow: '0 8px 16px rgba(0, 0, 0, 0.25)',
                            transition: 'box-shadow 0.3s ease-in-out',
                        }}
                        onMouseEnter={(e) => (e.currentTarget.style.boxShadow = '0 12px 24px rgba(0, 0, 0, 0.75)')}
                        onMouseLeave={(e) => (e.currentTarget.style.boxShadow = '0 8px 16px rgba(0, 0, 0, 0.25)')}
                        onClick={handlePositionClick}
                    >
                        {reserves[index]?.name || `R${index + 1}`}
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ReservesLayout;
