package main;

import com.eudycontreras.othello.models.GameBoardState;

import java.util.List;

import com.eudycontreras.othello.capsules.AgentMove;
import com.eudycontreras.othello.capsules.MoveWrapper;
import com.eudycontreras.othello.capsules.ObjectiveWrapper;
import com.eudycontreras.othello.controllers.AgentController;
import com.eudycontreras.othello.controllers.Agent;
import com.eudycontreras.othello.enumerations.PlayerTurn;
import com.eudycontreras.othello.threading.ThreadManager;
import com.eudycontreras.othello.threading.TimeSpan;

import sun.management.AgentConfigurationError;

public class ABPruning {
	/*
	function minimax(node, depth, alpha, beta, maximizingPlayer) is  
	if depth ==0 or node is a terminal node then  
	return static evaluation of node  
	  
	if MaximizingPlayer then      // for Maximizer Player  
	   maxEva= -infinity            
	   for each child of node do  
	   eva= minimax(child, depth-1, alpha, beta, False)  
	  maxEva= max(maxEva, eva)   
	  alpha= max(alpha, maxEva)      
	   if beta<=alpha  
	 break  
	 return maxEva  
	    
	else                         // for Minimizer player  
	   minEva= +infinity   
	   for each child of node do  
	   eva= minimax(child, depth-1, alpha, beta, true)  
	   minEva= min(minEva, eva)   
	   beta= min(beta, eva)  
	    if beta<=alpha  
	  break          
	 return minEva 
	 */
	/*
	 function minimax(node, depth, isMaximizingPlayer, alpha, beta):

    if node is a leaf node :
        return value of the node
    
    if isMaximizingPlayer :
        bestVal = -INFINITY 
        for each child node :
            value = minimax(node, depth+1, false, alpha, beta)
            bestVal = max( bestVal, value) 
            alpha = max( alpha, bestVal)
            if beta <= alpha:
                break
        return bestVal

    else :
        bestVal = +INFINITY 
        for each child node :
            value = minimax(node, depth+1, true, alpha, beta)
            bestVal = min( bestVal, value) 
            beta = min( beta, bestVal)
            if beta <= alpha:
                break
        return bestVal
	 */
	
	public static MoveWrapper Minimax(GameBoardState gameState, PlayerTurn turn) {
		int depth = AgentController.MAX_SEARCH_DEPTH;
		int reward;
		
		int j = 0;
		if (depth == 0) {
			
		}
		//System.out.println("WhiteCount: " + game);
		if (turn == PlayerTurn.PLAYER_ONE) { //isMaximizingPlayer
			double maxEva = Double.NEGATIVE_INFINITY;
			List<ObjectiveWrapper> nodes = AgentController.getAvailableMoves(gameState, turn);
			
			for (ObjectiveWrapper node : nodes) {
				//makeMove(node);
			}
			
			
			while (j <= depth) {
				
			}
			for (ObjectiveWrapper node : nodes) {
				System.out.println("Possible move: ");
				
			}
		} else {
			double minEva = Double.POSITIVE_INFINITY;
		}
		return null;
	}
	private int Maximizer() {
		return 0;
	}
	
	private int Minimizer() {
		return 0;
	}
}
