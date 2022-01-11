package main;

import java.util.List;
import java.util.Timer;
import java.util.concurrent.TimeUnit;

import com.eudycontreras.othello.capsules.AgentMove;
import com.eudycontreras.othello.capsules.ObjectiveWrapper;
import com.eudycontreras.othello.capsules.MoveWrapper;
import com.eudycontreras.othello.controllers.Agent;
import com.eudycontreras.othello.controllers.AgentController;
import com.eudycontreras.othello.enumerations.BoardCellState;
import com.eudycontreras.othello.enumerations.PlayerTurn;
import com.eudycontreras.othello.models.GameBoardState;
import com.eudycontreras.othello.threading.ThreadManager;
import com.eudycontreras.othello.threading.TimeSpan;
import com.eudycontreras.othello.models.GameBoard;
import com.eudycontreras.othello.utilities.GameTreeUtility;

public class AgentOne extends Agent {

	private int maxTime, eva, depth, maxEva, minEva, value, newMaxValue, newMinValue;
	private MoveWrapper bestMove, testMove;
	private BoardCellState boardCellState;
	private GameBoardState newGameState;
	private long startTime;
	
	public AgentOne() {
		super(PlayerTurn.PLAYER_ONE);
		// TODO Auto-generated constructor stub
	}
	public AgentOne(PlayerTurn playerTurn) {
		super(playerTurn);
		// TODO Auto-generated constructor stub
	}
	
	public AgentMove getMove(GameBoardState gameState) {
		depth = 5;
		int waitTime = UserSettings.MIN_SEARCH_TIME; // 1.5 seconds
		maxTime = UserSettings.MAX_SEARCH_TIME;
		
		bestMove = null;
		
		startTime = System.currentTimeMillis(); 	//What the current time is at this moment
		
		List<ObjectiveWrapper> moves = AgentController.getAvailableMoves(gameState, playerTurn); //Every possible move
		if (playerTurn == PlayerTurn.PLAYER_ONE) { 	//Check if its PLAYER_ONE and if the AI wants high or low values
			value = Integer.MIN_VALUE; 				//Set the value the smallest possible so the first found move will be the best move
			
			for (ObjectiveWrapper move : moves) {
				if (System.currentTimeMillis() - startTime >= maxTime) {
					break;
				}
				
				newMaxValue = MiniMax(gameState, depth, Integer.MIN_VALUE, Integer.MAX_VALUE, playerTurn);
				
				if (newMaxValue >= value) { 	//If the new value is better then the old value this is a better move
					value = newMaxValue;	//Update the value
					bestMove = new MoveWrapper(move);	//Update the bestMove
				}
			} 
		}else {
			value = Integer.MAX_VALUE;
			
			for (ObjectiveWrapper move : moves) {
				if (System.currentTimeMillis() - startTime >= maxTime) {
					break;
				}
				
				newMinValue = MiniMax(gameState, depth, Integer.MIN_VALUE, Integer.MAX_VALUE, playerTurn);
				
				if (newMinValue <= value) {
					value = newMinValue;
					bestMove = new MoveWrapper(move);
				}
			}
		}
		
		//ThreadManager.pause(TimeSpan.millis(waitTime));
		searchDepth = depth;
		return bestMove;
	}
	
	private int MiniMax(GameBoardState gameState, int depth, int alpha, int beta, PlayerTurn playerTurn) {
		nodesExamined++;	//Updates the number of nodes examined
		if (depth == 0 || gameState.isTerminal()) {
			reachedLeafNodes++;
			return gameState.getWhiteCount();
			
		}
		
		if (playerTurn == PlayerTurn.PLAYER_ONE) {
			int maxEva = Integer.MIN_VALUE;
			
			List<ObjectiveWrapper> moves = AgentController.getAvailableMoves(gameState, playerTurn);
			for (ObjectiveWrapper move : moves) {
				if (System.currentTimeMillis() - startTime >= maxTime) {
					break;
				}
				
				GameBoardState newGameState = AgentController.getNewState(gameState, move);
				eva = MiniMax(newGameState, (depth-1), alpha, beta, PlayerTurn.PLAYER_TWO);
				
				maxEva = Math.max(maxEva, eva);
				alpha = Math.max(alpha, maxEva);
				
				if (beta <= alpha) {
					prunedCounter++;
					break;
				}
			}
			return maxEva;
			
		} else { //PlayerTurn = Player_Two
			int minEva = Integer.MAX_VALUE;
			
			List<ObjectiveWrapper> moves = AgentController.getAvailableMoves(gameState, playerTurn);
			for (ObjectiveWrapper move : moves) {
				if (System.currentTimeMillis() - startTime >= maxTime) { //Timer, if the current time - the time the node started is greater then maxTime - END
					break;
				}
				
				GameBoardState newGameState = AgentController.getNewState(gameState, move);
				eva = MiniMax(newGameState, (depth-1), alpha, beta, PlayerTurn.PLAYER_ONE);
				
				minEva = Math.min(minEva, eva);
				beta = Math.min(beta, minEva);
				
				if (beta <= alpha) {
					prunedCounter++;
					break;
				}
			}
			return minEva;
		}
	}
}
