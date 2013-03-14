using System;

namespace AI
{
	/// <summary>	/// Author: Jason Whatson
	/// Date: June 10th 2007
	/// Summary description for AI.
	/// </summary>
	public class AI
	{
		//Holds the values of how important the AI picks this many sticks.
		//For other games: This would be a list of every posible move in the game at any one time. 
		//as you could imagine some games have thousands of choices a player can make 
		//and this list can be quite large.
		public static int three = 0,two = 0,one = 0;

		/// <summary>
		/// Resets all the 'importance count' values back to 0.
		/// </summary>
		public static void resetCount()
		{
			three = 0;two = 0;one = 0;
		}

		/// <summary>
		/// Runs the Minimax logic to find out how many sticks to pick up at the current point in 
		/// the game. The amount calculated is the ultimate best choice to pick up.
		/// 
		/// For other games: Runs the Minimax logic to find out the best move at the current point in 
		/// the game. The move calculated is the ultimate best move.
		/// </summary>
		/// <param name="sticksLeft">
		/// The number of sticks left avaliable to pick up. It is 
		/// 'the current state of the game'. In other games such as chess this would be the 
		/// current board state. Ie, what pices are on the board and their positions.
		/// </param>
		/// <param name="playerAI">
		/// Holds which players turn it is. If it is the AI's turn (playerAI is true),
		/// we are maximizing the chances of the AI winning. If it is the players turn, we
		/// are minimizing the chances of the player winning. 
		/// </param>
		/// <param name="noSticksToPickUp">
		/// The number of sticks to pick up from the current game.
		/// </param>
		/// <param name="rootPickup">
		/// This holds the value that we increment (the 'root pickup amount'/'game move'
		/// results in a win for the AI when the game is over on the current game branch)
		///  or decrement (result is oposite of increment). 
		/// 
		/// Think of it as the origonal move made by the AI that started the Minimax recursion.
		/// It is the value passed to the minimax algorithum method on the bottom of the call stack.
		/// 
		/// If we want to test how good picking up 3 sticks at the current point in the game, we pass it 3.
		/// If we want to test how good moving king from A3 to A4 is we pass this logic.
		/// </param>
		/// <returns></returns>
		public static void runMinimax(int sticksLeft,bool playerAI,int noSticksToPickUp,ref int rootPickup)
		{

			int maxPickup = 3;

			//Make the move 
			sticksLeft -= noSticksToPickUp;

			//If after the pickup is made the game is over (no sticks are left) switch the turn
			//to the other player.
			if(sticksLeft == 0)
			{
				playerAI = !playerAI;
			}

			//Test to see if it was a lossing move
			if(sticksLeft <= 1)
			{
				//'Max' has lost - Min has won (BAD)
				if (playerAI)
				{
					if(rootPickup == 1) one--;
					if(rootPickup == 2) two--;
					if(rootPickup == 3) three--;
				}
				//'Min' has lost - Max has won (GOOD)
				else
				{
					if(rootPickup == 1) one++;
					if(rootPickup == 2) two++;
					if(rootPickup == 3) three++;
				}

				return;
			}

			//Next we need to actually make the move; this will be the current move in the 
			//moves list we are up to. Remember we are essentially looping through every posible move
			//in the game.
			//We now need to provide logic to actually make the move.
			
			/*----------------------------------
			 * Start move logic using game rules
			 *---------------------------------*/

			//If there is less sticks left then the maximum pick up amount then set
			//the maximim pickup amount to the number of sticks left.
			if (sticksLeft < maxPickup)
			{
				maxPickup = sticksLeft;
			}

			//Start the recursion which runs through all the posible moves at this current level
			for (int i = 1;i <= maxPickup;i++)
			{
				runMinimax(sticksLeft,!playerAI,i,ref rootPickup);
			}

			return;

		}

		/// <summary>
		/// Find and return the most important stick to pickup.
		/// It is returning the best amount of sticks to pick up.
		/// 
		/// Other games: This may be a loop that loops all posible moves and returns 
		/// the highest move.
		/// It is returning the maximum best move.
		/// </summary>
		/// <returns>
		/// The best number of sticks to pickup.
		/// 0 if it dosnt matter which stick is picked up.
		/// </returns>
		public static int getNoSticksToPickUp()
		{
			//Find highest value logic
			if(one > two && one > three)
			{
				return 1;
			}

			if(two > one && two > three)
			{
				return 2;
			}

			if(three > two && three > one)
			{
				return 3;
			}

			return 0;
		}
	}
}
