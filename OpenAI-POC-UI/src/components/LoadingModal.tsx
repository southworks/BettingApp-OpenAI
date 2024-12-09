import React from 'react';
import Lottie from 'lottie-react';
import loadingAnimation from '../assets/matchLoading.json'

const LoadingModal: React.FC = () => {
    return (
        <div className="flex flex-col justify-center items-center bg-white rounded-3xl w-[398px] h-[330.22px]">
            <Lottie
                animationData={loadingAnimation}
                loop={true}
                style={{ height: '156.22px', width: '159.57px' }}
            />
            <h3 className="text-[36px] font-mont leading[36px] text-center font-extrabold mt-2 text-[#0F172A]">
                Calculating result
                <span className="dot-pulse">
                    <span>.</span>
                    <span>.</span>
                    <span>.</span>
                </span>
            </h3>
            <p className="text-start font-mont font-medium text-[14px] leading-[20px] text-[#0F172A] mt-2 px-4">
                Our AI is processing the data to deliver the closest result
            </p>
        </div>
    );
};

export default LoadingModal;



