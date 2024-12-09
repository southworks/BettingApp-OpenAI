import React, { useEffect, useState } from 'react';
import { Team, Player } from '../types/types';
import TeamSelector from './TeamSelector';
import LoadingModal from './LoadingModal';
import ReadyLayout from './ReadyLayout';
import LoadingTeam from './LoadingTeam';
import { useNavigate } from 'react-router-dom';
import soccerField from '../assets/Soccer_Field.png'
import { Competition } from '../types/types';
import { getCompetitions } from '../service/getCompetitions.service';
import { getTeams } from '../service/getTeams.service';
import { choseLineups } from '../service/choseLineups.service';
import { matchWinner } from '../service/matchWinner.service';

const Main: React.FC = () => {
  const [teams, setTeams] = useState<Team[]>([]);
  const [competitions, setCompetitions] = useState<Competition[]>([])
  const [matchType, setMatchType] = useState<string>('Friendly');
  const [homeTeam, setHomeTeam] = useState<{ team: string; players: Player[] } | null>(null);
  const [awayTeam, setAwayTeam] = useState<{ team: string; players: Player[] } | null>(null);
  const [teamLoading, setTeamLoading] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [modalMessage, setModalMessage] = useState('');
  const [homeLineup, setHomeLineup] = useState<{ starters: Player[], reserves: Player[] } | null>(null);
  const [awayLineup, setAwayLineup] = useState<{ starters: Player[], reserves: Player[] } | null>(null);
  const [bettingMargin, setBettingMargin] = useState<number>();
  const navigate = useNavigate();

  useEffect(() => {
    fetchCompetitions();
    fetchTeams();
  }, []);

  const fetchCompetitions = async () => {
    try {
      const response = await getCompetitions();
      setCompetitions(Array.isArray(response) ? response : []);
    } catch (error) {
      console.log(error);
      setCompetitions([]);
    }
  };

  const fetchTeams = async () => {
    try {
      const response = await getTeams();
      setTeams(Array.isArray(response) ? response : []);
    } catch (error) {
      console.log(error);
      setTeams([]);
    }
  };

  const handleMatchTypeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setMatchType(e.target.value);
  };

  const handleBettingMargin = (e: React.ChangeEvent<HTMLInputElement>) => {
    setBettingMargin(parseFloat(e.target.value))
  }


  const handleFinalTeamSelection = (team: string, players: Player[], isHomeTeam: boolean) => {
    if (isHomeTeam) {
      setHomeTeam({ team, players });
      setModalMessage('Now select the Away Team');
    } else {
      setAwayTeam({ team, players });
      if (homeTeam) {
        setModalMessage('Confirm lineups?');
      } else {
        setModalMessage('Now select the Home Team');
      }
    }
    setShowModal(true);
  };

  const handleSubmitTeams = async () => {
    if (homeTeam?.players && Array.isArray(homeTeam.players) &&
      awayTeam?.players && Array.isArray(awayTeam.players) &&
      matchType) {
      setTeamLoading(true);

      const matchData = {
        homeTeam: homeTeam.players.map(player => ({
          name: player.name,
          position: player.position
        })),
        awayTeam: awayTeam.players.map(player => ({
          name: player.name,
          position: player.position
        })),
        competition: matchType
      };

      try {
        const response = await choseLineups(matchData);

        const homeLineup = {
          starters: Array.isArray(response.homeStarters) ? response.homeStarters.map((player: Player) => ({
            name: player.name,
            position: player.position
          })) : [],
          reserves: Array.isArray(response.homeSubstitutes) ? response.homeSubstitutes.map((player: Player) => ({
            name: player.name,
            position: player.position
          })) : []
        };

        const awayLineup = {
          starters: Array.isArray(response.awayStarters) ? response.awayStarters.map((player: Player) => ({
            name: player.name,
            position: player.position
          })) : [],
          reserves: Array.isArray(response.awaySubstitutes) ? response.awaySubstitutes.map((player: Player) => ({
            name: player.name,
            position: player.position
          })) : []
        };


        setHomeLineup(homeLineup);
        setAwayLineup(awayLineup);
      } catch (error) {
        console.error("Error choosing lineups:", error);
      } finally {
        setTeamLoading(false);
      }
    }
  };

  const handleCalculateMatch = async () => {
    try {
      if (homeTeam && awayTeam && homeLineup && awayLineup && matchType && bettingMargin) {
        setIsLoading(true);

        const matchData = {
          homeTeam: homeTeam?.team,
          awayTeam: awayTeam?.team,
          homeStarters: homeLineup.starters,
          homeSubstitutes: homeLineup.reserves,
          awayStarters: awayLineup.starters,
          awaySubstitutes: awayLineup.reserves,
          competition: matchType,
          bettingMargin: bettingMargin
        };

        const response = await matchWinner(matchData);
        if (response.status === 200 && response.data) {
          navigate('/results');
        } else {
          console.log("Error while calculating winner")
        }
      } else {
        console.log('Missing properties');
        return;
      }
    } catch (error) {
      console.error("Error to calculate match winner:", error);
    } finally {
      setIsLoading(false);
    }
  }

  const handleCloseModal = () => {
    setShowModal(false);
  }


  const handleAdd = () => {
    navigate('/add')
  }


  return (
    <div
      className="flex flex-col items-center justify-center p-6 h-screen w-screen overflow-y-auto"
      style={{
        background: `radial-gradient(circle at top left, #50c878, #6a0dad 25%, #3b003b 60%, #1a001a 100%), 
                     linear-gradient(135deg, rgba(255, 255, 255, 0.1), rgba(0, 0, 0, 0.1))`,
        backgroundBlendMode: 'overlay',
        backgroundSize: '200% 200%',
        animation: 'moveGradient 10s ease infinite',
      }}
    >
      {isLoading && (
        <div
          className="absolute inset-0 flex items-center justify-center z-50"
          style={{ backgroundColor: '#0F172A99' }}
        >
          <LoadingModal />
        </div>
      )}


      <div className="flex justify-around w-full h-full overflow-hidden">
        {teamLoading && (
          <div
            className="absolute inset-0 flex items-center justify-center z-20 h-screen"
            style={{ backgroundColor: '#0F172A99' }}
          >
            <LoadingTeam />
          </div>
        )}

        {homeLineup && awayLineup ? (
          <div className="w-full flex justify-around">
            <div
              className="w-[30%] bg-cover bg-center rounded-xl p-5 shadow-xl hover:shadow-2xl transition-shadow duration-300"
              style={{ backgroundImage: `url(${soccerField})` }}
            >
              <h2 className="text-center text-xl font-bold mb-7">{homeTeam?.team} Lineup</h2>
              <ReadyLayout selectedPlayers={homeLineup.starters} reserves={homeLineup.reserves} />
            </div>

            <button
              onClick={handleCalculateMatch}
              className="h-[100px] my-auto px-6 py-3 bg-blue-500 text-white rounded-lg shadow-lg hover:bg-blue-600 hover:shadow-xl transition-all duration-300"
            >
              Calculate Match
            </button>

            <div
              className="w-[30%] bg-cover bg-center rounded-xl p-5 shadow-xl hover:shadow-2xl transition-shadow duration-300"
              style={{ backgroundImage: `url(${soccerField})` }}
            >
              <h2 className="text-center text-xl font-bold mb-7">{awayTeam?.team} Lineup</h2>
              <ReadyLayout selectedPlayers={awayLineup.starters} reserves={awayLineup.reserves} />
            </div>
          </div>

        ) : (
          <>

            {teamLoading && (
              <div
                className="absolute inset-0 flex items-center justify-center z-20 h-screen"
                style={{ backgroundColor: '#0F172A99' }}
              >
                <LoadingTeam />
              </div>
            )}

            {homeLineup && awayLineup ? (
              <div className="w-full flex justify-around">
                <div
                  className="w-[30%] bg-cover bg-center rounded-xl p-5 shadow-xl hover:shadow-2xl transition-shadow duration-300"
                  style={{ backgroundImage: `url(${soccerField})` }}
                >
                  <h2 className="text-center text-xl font-bold mb-7">{homeTeam?.team} Lineup</h2>
                  <ReadyLayout selectedPlayers={homeLineup.starters} reserves={homeLineup.reserves} />
                </div>

                <button
                  onClick={handleCalculateMatch}
                  className="h-[100px] my-auto px-6 py-3 bg-blue-500 text-white rounded-lg shadow-lg hover:bg-blue-600 hover:shadow-xl transition-all duration-300"
                >
                  Calculate Match
                </button>

                <div
                  className="w-[30%] bg-cover bg-center rounded-xl p-5 shadow-xl hover:shadow-2xl transition-shadow duration-300"
                  style={{ backgroundImage: `url(${soccerField})` }}
                >
                  <h2 className="text-center text-xl font-bold mb-7">{awayTeam?.team} Lineup</h2>
                  <ReadyLayout selectedPlayers={awayLineup.starters} reserves={awayLineup.reserves} />
                </div>
              </div>

            ) : (
              <>
                {/* Home Team */}
                <div
                  className="w-1/3 flex flex-col items-center rounded-xl mt-5 h-[80vh] overflow-y-auto scrollbar-hide"
                  style={{
                    backgroundColor: '#234580',
                    boxShadow: '0 4px 8px rgba(0, 0, 0, 0.2)',
                    transition: 'box-shadow 0.3s ease-in-out',
                  }}
                >
                  <h3 className="text-2xl font-semibold mb-4">Home Team</h3>
                  <TeamSelector
                    teams={teams}
                    isHomeTeam={true}
                    onFinalSelection={handleFinalTeamSelection}
                  />
                </div>


                {/* Match Type */}
                <div className="w-1/5 text-center mt-5 flex justify-around flex-col">
                  <select
                    onChange={handleMatchTypeChange}
                    value={matchType}
                    className="p-2 border border-gray-300 rounded bg-white"
                  >
                    <option value="Friendly">Friendly</option>

                    {Array.isArray(competitions) && competitions.map((competition) => (
                      <option key={competition.id} value={competition.name}>
                        {competition.name}
                      </option>
                    ))}

                  </select>

                  <div>
                    <label
                      htmlFor="betting-margin"
                      className="text-white items-start"
                    >
                      Define the Betting Margin
                    </label>
                    <input
                      type="text"
                      id="betting-margin"
                      onChange={handleBettingMargin}
                      placeholder="Betting margin (e.g., 5.3)"
                      className="w-full border-2 border-gray-300 rounded-md px-4 py-2 
                   focus:outline-none focus:border-blue-500 focus:ring-2 
                   focus:ring-blue-200 transition duration-300 ease-in-out"
                    />

                  </div>





                  {homeTeam && awayTeam && (
                    <div className="w-full text-center mt-[240px]">
                      <button
                        className="px-6 py-3 bg-green-500 text-white rounded-lg text-xl hover:bg-green-600"
                        onClick={handleSubmitTeams}
                      >
                        Chose lineups
                      </button>
                    </div>
                  )}

                  <div>
                    <h4 className='text-white'>Didn’t find the team or match you were looking for? <br /> Click below to add them.</h4>
                    <button onClick={handleAdd} className="mt-2 px-4 py-2 bg-green-500 text-white rounded-lg">Add</button>
                  </div>
                </div>


                {/* Away Team */}
                <div
                  className="w-1/3 flex flex-col items-center rounded-xl mt-5 h-[80vh] overflow-y-auto scrollbar-hide"
                  style={{
                    backgroundColor: '#b91c1c',
                    boxShadow: '0 4px 8px rgba(0, 0, 0, 0.2)',
                    transition: 'box-shadow 0.3s ease-in-out',
                  }}
                >
                  <h3 className="text-2xl font-semibold mb-4">Away Team</h3>
                  <TeamSelector
                    teams={teams}
                    isHomeTeam={false}
                    onFinalSelection={handleFinalTeamSelection}
                  />
                </div>
              </>
            )}

          </>
        )}
      </div>

      {/* Modal */}
      {showModal && (
        <div className="fixed inset-0 z-50 flex items-center justify-center">
          <div className="relative z-50 bg-white p-6 rounded shadow-lg text-center">
            <p className="text-xl font-bold mb-4">{modalMessage}</p>
            <button
              onClick={handleCloseModal}
              className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
            >
              OK
            </button>
          </div>

          <div
            className="fixed inset-0 bg-black opacity-50 z-40"
            onClick={handleCloseModal}
          ></div>
        </div>
      )}
    </div>
  );
};


export default Main;
