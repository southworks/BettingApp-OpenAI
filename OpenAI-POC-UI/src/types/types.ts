export interface Player {
    name: string;
    position: string;

}

export interface Team {
    name: string;
    squad: Player[];

}

export interface TeamSelectorProps {
    teams: Team[];
    isHomeTeam: boolean;
    onFinalSelection: (team: string, players: Player[], isHomeTeam: boolean) => void;
}

export interface TeamSelectionProps {
    teams: Team[];
    selectedTeam: Team | null;
    onSelectTeam: (team: Team | null) => void;
    onConfirmSelection: () => void;
}

export interface StartersLayoutProps {
    selectedPlayers: { [key: string]: Player | null };
    handlePositionClick: (position: string) => void;
}

export interface ReservesLayoutProps {
    reserves: Player[];
    handlePositionClick: () => void;
}

export interface ReadyLayoutProps {
    selectedPlayers: Player[];
    reserves: Player[];
}

export interface PlayerSelectionModalProps {
    position: string | null;
    filteredPlayers: Player[];
    onCancel: () => void;
    onSelectPlayer: (player: Player) => void;
    isPlayerSelected: (player: Player) => boolean;
}

export interface FieldLayoutProps {
    selectedPlayers: { [key: string]: Player | null };
    reserves: Player[];
    handlePositionClick: (position: string) => void;
}

export interface BackButtonProps {
    onClick: () => void;
}

export interface ResultPlayer {
    name: string;
    position: string;
}


export interface ResultTeam {
    name: string;
    players: {
        starters: ResultPlayer[];
        reserves: ResultPlayer[];
    };
}

export interface MatchResult {
    home_team_score: number;
    away_team_score: number;
}

export interface Odds {
    home_win: number;
    draw: number;
    away_win: number;
}

export interface Match {
    id: string;
    home_team: ResultTeam;
    away_team: ResultTeam;
    match_type: string;
    result: MatchResult;
    odds: Odds;
}

export interface Results {
    id: string;
    homeTeam: string;
    awayTeam: string;
    competition: string;
    homeStarters: Player[];
    homeSubstitutes: Player[];
    awayStarters: Player[];
    awaySubstitutes: Player[];
    predictedWinner: string;
    predictionDate: string;
    homeWinOdd: string;
    awayWinOdd: string;
    drawOdd: string;
}

export interface Competition {
    id: string;
    name: string;
}

export interface MatchWithLineup {
    homeTeam: string;             
    awayTeam: string;             
    homeStarters: Player[];  
    homeSubstitutes: Player[];       
    awayStarters: Player[];   
    awaySubstitutes: Player[];      
    competition: string;          
    bettingMargin: number;        
  }
  
