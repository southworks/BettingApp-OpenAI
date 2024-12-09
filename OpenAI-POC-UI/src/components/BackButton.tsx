import React from 'react';
import { BackButtonProps } from '../types/types';

const BackButton: React.FC<BackButtonProps> = ({ onClick }) => (
  <button className="px-3 py-1 bg-red-500 text-white rounded-lg text-sm" onClick={onClick}>
    Back
  </button>
);

export default BackButton;
