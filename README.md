# Hexapon-AI
Hexapon AI is a game similar to Hexapawn but I've included an AI program that becomes smarter the longer you play and learns faster the better player is. It is played on a 3x3 board, 3 white pawn pieces for the player and 3 black piece for the black player. 

# Rules
The player or white pawn always starts first so the AI always plays every even turn. 
Pawns may advance straight foward one tile to an an empty space.
A pawn may capture an opponent pawn by moving one tile dagonally left or right, but the tile must be occupid by the opponent 

# Game is won by
Advancing a pawn to the opponents side
Capturing all the opponenets pieces
Achieving a position in which the opponent cannot move.

# How the AI becomes smarter?
The AI has 24 algorithms, and each algorithm has a number of AI-moves it will random choose from.(lets call them beeds that represents a move)
If the AI loses a round, it will remove one beed from the last algorithm

# Program
Run on Unity coded with c#. 
Available for Android
Test Cases to test if everything works 
link http://www.mediafire.com/file/b9gdt46v66bbjji/hexapon-ai.apk/file
and here are the Test Cases https://github.com/ceeleemee/Hexapon-AI/tree/master/Test%20Cases

# Sources
The Game That Learns by vsauce2 [https://www.youtube.com/watch?v=sw7UAZNgGg8]
article [http://cs.williams.edu/~freund/cs136-073/GardnerHexapawn.pdf]
