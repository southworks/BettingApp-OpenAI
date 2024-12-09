import React from 'react';
import Lottie from 'lottie-react';
import loadingAnimation from '../assets/soccer_animation.json'

const LoadingTeam: React.FC = () => {
    return (
        <div className="flex flex-col justify-center items-center bg-white rounded-3xl w-[398px] h-[330.22px]">
            <Lottie
                animationData={loadingAnimation}
                loop={true}
                style={{ height: '250px', width: '250px',transform: 'rotate(270deg)' }}
            />
            <h3 className="text-[24px] font-mont leading[24px] text-center font-extrabold mt-2 text-[#0F172A]">
                Selecting the best lineup
                <span className="dot-pulse">
                    <span>.</span>
                    <span>.</span>
                    <span>.</span>
                </span>
            </h3>

        </div>
    );
};

export default LoadingTeam;



