import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { addCompetition } from "../service/addCompetition.service";
import { addTeam } from "../service/addTeam.service";

const Add: React.FC = () => {
    const [selectedOption, setSelectedOption] = useState<"Competitions" | "Teams">("Competitions");
    const [competitionName, setCompetitionName] = useState("");
    const [teamName, setTeamName] = useState("");
    const [lineup, setLineup] = useState("");
    const [errorMessage, setErrorMessage] = useState<string | null>(null);
    const navigate = useNavigate();


    const addCompetitionService = async (competition: { Name: string }) => {
        try {
            const response = await addCompetition(competition);
            if (response.status === 201) {
                alert("Competition added successfully!");
                navigate("/");
            }

        } catch (error: any) {
            console.error("Failed to add competition:", error);
            setErrorMessage(error.response?.data || "An error occurred while adding the competition.");
        }
    };

    const addTeamService = async (teamData: { Name: string; Squad: { Name: string; Position: string }[] }) => {
        try {
            const response = await addTeam(teamData);
            if (response.status === 201) {
                alert("Team added successfully!");
                navigate("/");
            }
        } catch (error: any) {
            console.error("Failed to add team:", error);
            setErrorMessage(error.response?.data || "An error occurred while adding the team.");
        }
    };

    const handleAddClick = async () => {
        setErrorMessage(null);

        if (selectedOption === "Competitions") {
            const competitionData = { Name: competitionName };
            await addCompetitionService(competitionData);
            setCompetitionName("");
        } else if (selectedOption === "Teams") {
            const squad = lineup.split(",").map((entry) => {
                const [name, position] = entry.split("-");
                return { Name: name.trim(), Position: position.trim() };
            });

            const teamData = {
                Name: teamName,
                Squad: squad
            };

            await addTeamService(teamData);
            setTeamName("");
            setLineup("");
        }
    };

    return (
        <div className="w-3/4 h-3/4 bg-gray-100 p-8 rounded-lg shadow-lg mx-auto my-10">
            <div className="flex justify-between items-center mb-6">
                <h2 className="text-2xl font-semibold">What would you like to add?</h2>
                <select
                    value={selectedOption}
                    onChange={(e) => setSelectedOption(e.target.value as "Competitions" | "Teams")}
                    className="p-2 border border-gray-300 rounded bg-white"
                >
                    <option value="Competitions">Competitions</option>
                    <option value="Teams">Teams</option>
                </select>
            </div>

            {errorMessage && (
                <div className="mb-4 p-4 text-red-700 bg-red-100 rounded">
                    {errorMessage}
                </div>
            )}

            {selectedOption === "Competitions" && (
                <div className="mb-4">
                    <label className="block text-lg font-medium mb-2">Competition Name</label>
                    <input
                        type="text"
                        value={competitionName}
                        onChange={(e) => setCompetitionName(e.target.value)}
                        placeholder="Enter competition name"
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
            )}

            {selectedOption === "Teams" && (
                <div className="mb-4">
                    <label className="block text-lg font-medium mb-2">Team Name</label>
                    <input
                        type="text"
                        value={teamName}
                        onChange={(e) => setTeamName(e.target.value)}
                        placeholder="Enter team name"
                        className="w-full p-2 border border-gray-300 rounded"
                    />

                    <label className="block text-lg font-medium mt-4 mb-2">Team Lineup</label>
                    <p className="text-sm text-gray-500 mb-2">
                        Please list player names with positions next to each name (e.g., John Doe - Goalkeeper).
                    </p>
                    <textarea
                        value={lineup}
                        onChange={(e) => setLineup(e.target.value)}
                        placeholder="e.g., John Doe - Goalkeeper, Jane Smith - Forward"
                        className="w-full p-2 border border-gray-300 rounded h-32"
                    />
                </div>
            )}

            <div className="flex justify-end">
                <button
                    onClick={handleAddClick}
                    className="px-6 py-3 bg-blue-500 text-white rounded-lg text-lg hover:bg-blue-600"
                >
                    Add {selectedOption === "Competitions" ? "Competition" : "Team"}
                </button>
            </div>
        </div>
    );
};

export default Add;
