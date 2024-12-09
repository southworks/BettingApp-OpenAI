import React, { useEffect, useState } from "react";
import type { Results } from "../types/types";
import { getResults } from "../service/getResults.service";

const Results: React.FC = () => {
  const [results, setResults] = useState<Results[]>([]);

  useEffect(() => {
    fetchResults();
  }, []);

  const fetchResults = async () => {
    try {
      const response = await getResults();
      const sortedResults = response.sort((a: Results, b: Results) => 
        new Date(b.predictionDate).getTime() - new Date(a.predictionDate).getTime()
      );
      setResults(sortedResults);
    } catch (error) {
      console.log(error);
    }
  };

  const latestResult = results[0];

  return (
    <div className="h-screen flex flex-col w-screen">
      {latestResult && (
        <div className="p-4 bg-blue-200 flex-shrink-0 h-[50vh] flex items-center justify-center">
          <div className="p-4 border rounded-lg shadow-md bg-white">
            <h2 className="text-lg font-bold">
              Latest Match: {latestResult.homeTeam} vs {latestResult.awayTeam}
            </h2>
            <p>Match Type: {latestResult.competition}</p>
            <p>
              Odds: Home win {latestResult.homeWinOdd}, Draw {latestResult.drawOdd}, Away win {latestResult.awayWinOdd}
            </p>
          </div>
        </div>
      )}

      <div className="flex-grow overflow-y-auto p-4 bg-gray-100">
        {results.map((result) => (
          <details key={result.id} className="mb-4 w-full border rounded-lg shadow-lg">
            <summary className="cursor-pointer p-4 bg-gray-200 hover:bg-gray-300 transition flex justify-between items-center">
              <h2 className="flex items-center">
                Match: {result.homeTeam} vs {result.awayTeam}
              </h2>
              <p>Match type: {result.competition}</p>
              <p>Odds: Home Win {result.homeWinOdd} | Draw {result.drawOdd} | Away Win {result.awayWinOdd}</p>
            </summary>

            <div className="p-4 bg-white flex flex-col lg:flex-row lg:space-x-4">
              {/* Home Team */}
              <div className="flex-1">
                <h3 className="font-bold mb-2">Home Team - {result.homeTeam}</h3>
                <ul className="mb-4">
                  <li className="font-semibold">Starters:</li>
                  {result.homeStarters.map((player, index) => (
                    <li key={index}>{player.name} - {player.position}</li>
                  ))}
                </ul>
                <ul className="mb-4">
                  <li className="font-semibold">Reserves:</li>
                  {result.homeSubstitutes.map((player, index) => (
                    <li key={index}>{player.name} - {player.position}</li>
                  ))}
                </ul>
              </div>

              {/* Away Team */}
              <div className="flex-1">
                <h3 className="font-bold mb-2">Away Team - {result.awayTeam}</h3>
                <ul className="mb-4">
                  <li className="font-semibold">Starters:</li>
                  {result.awayStarters.map((player, index) => (
                    <li key={index}>{player.name} - {player.position}</li>
                  ))}
                </ul>
                <ul className="mb-4">
                  <li className="font-semibold">Reserves:</li>
                  {result.awaySubstitutes.map((player, index) => (
                    <li key={index}>{player.name} - {player.position}</li>
                  ))}
                </ul>
              </div>
            </div>
          </details>
        ))}
      </div>
    </div>
  );
};

export default Results;
