using System;

namespace AI
{
	/// <summary>	/// Author: Jason Whatson
	/// Date: June 10th 2007
	/// Main
	/// </summary>
	class MainClass
	{
		//The number of starting sticks
		private static int NoSticks = 10;
		
		//Max number of sticks a player can pick up at a time
		private const int MAXPICKUP = 3;

		//Holds the amount specified by the player to pickup
		private static int PickUpAmount = 0; 

		//Holds which players turn it is
	    private static bool player1 = true;		

		[STAThread]
		static void Main(string[] args)
		{
			//Let the game run while there is atleast one stick to pickup
			while(NoSticks >= 1)
			{
				/*Prompt player one how many sticks they would like to pick up
				 *when its their turn. And prompt player two when its theirs.*/
				if (player1)
				{
					Console.Write("How many sticks would you like to pick up player1? :");
				}
				else
				{
					//call AI logic
					showPlayerBestMove();
					Console.Write("How many sticks would you like to pick up player2? :");
				}
				
				//Read in the amount of sticks to pick up specified by the player
				try{
			  		PickUpAmount = Int32.Parse(Console.ReadLine());
				} catch (System.FormatException ex) {
					PickUpAmount = 0;
				}
				
				//Only make a the move if the pick up amount is valid
				if (PickUpAmount < 4 && PickUpAmount > 0)
				{
					NoSticks = NoSticks - PickUpAmount;
					Console.WriteLine("Number of sticks left " + NoSticks);
					player1 = !player1;
				}else{
					Console.WriteLine("You can only pick up between 1,2 or 3 sticks");
				}
			}
			
			/*When there are no sticks left, the game is over.
			 *who ever picks up the last stick looses*/
			showWinner();	
			Console.ReadLine();
			
		}
		
		/// <summary>
		/// Shows which player has won the game.
		/// </summary>
		private static void showWinner()
		{
			if(player1)
			{
				Console.WriteLine("Player 1 wins");
			}
			else
			{
				Console.WriteLine("Player 2 wins");
			}
		}
		
		/// <summary>
		/// Shows which player has won the game.
		/// </summary>
		private static void showPlayerBestMove()
		{			
			int maxPickup = MAXPICKUP;
			
			//If there is less sticks left then the maximum pick up amount then set
			//the maximim pickup amount to the number of sticks left.
			if (NoSticks < maxPickup)
			{
				maxPickup = NoSticks;
			}
			//Start the recursion which runs through all the posible moves at the root level
			for (int i = 1;i <= maxPickup;i++)
			{				AI.runMinimax(NoSticks,player1,i,ref i);

			}
			
			//Get the number of sticks to pick up as calculated from the AI
			Console.WriteLine("You should pickup " + AI.getNoSticksToPickUp() + 
			                                                          " sticks");
			//Resets all the moves. All moves will now be equally important 
			//until AI is run again
			AI.resetCount();
		}
	}
}
