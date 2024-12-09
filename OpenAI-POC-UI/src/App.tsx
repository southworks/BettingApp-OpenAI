import React from 'react';
import Main from './components/Main';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Results from './components/Results';
import Add from './components/Add';

const App: React.FC = () => {
  return (
    <Router>
      <div className="h-screen bg-gray-100 flex items-center justify-center">
        <Routes>
          <Route path="/" element={<Main />} />
          <Route path="/results" element={<Results />} />
          <Route path="/add" element={<Add />} />
          
        </Routes>
      </div>
    </Router>
  );
}

export default App;
