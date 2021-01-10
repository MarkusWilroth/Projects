package dataRecording;

import pacman.controllers.*;
import pacman.game.Constants.DM;
import pacman.game.Constants.GHOST;
import pacman.game.Game;
import pacman.game.Constants.MOVE;

import java.util.ArrayList;

/**
 * The DataCollectorHumanController class is used to collect training data from playing PacMan.
 * Data about game state and what MOVE chosen is saved every time getMove is called.
 * @author andershh
 *
 */
public class DataCollectorController extends HumanController{
	private ArrayList<DataTuple> allData = new ArrayList<DataTuple>();
	
	public DataCollectorController(KeyBoardInput input){
		super(input);
	}
	
	@Override
	public MOVE getMove(Game game, long dueTime) {
		MOVE move = super.getMove(game, dueTime);
		
		DataTuple data = new DataTuple(game, move);
		allData.add(data);
		
		if (game.getScore() >= 4000) {
			for (DataTuple dataToAdd : allData) {
				DataSaverLoader.SavePacManData(dataToAdd);
			}
			allData.clear();
		}
				
				
		return move;
	}

}
