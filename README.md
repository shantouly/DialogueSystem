## A simple method for implementing a dialogueSystem in Unity  
The file contains two C # code files and one TextAsset file.  
Main implementation method: When the player approaches the NPC, a UI pops up prompting the player to press the R key to open the dialog box.   
The implementation method of the dialogue system is to first group the content of the dialogue into line breaks.  
Use a coroutine with a pointer to indicate the current position in a line of conversation content, and then return a time interval each time to achieve a sticky effect.

