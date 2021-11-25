# Procedural-Dungeon-Generator

Unity version 2020.3.22f1

#### Assignment
Each rogue like game needs an procedural generated terrain/dungeon which starts with generating a layout I wanted to recreated the dungeon layout of the Binding of Isaac

##### How I went to work
I needed the code to detect where it needed to place down doors in a grid while not overriding the grid and placing down doors and walls correctly which is the reason I used `depth-search first` algorithm as it will detected which room is connected with which and where the door needs to be placed
I used one standard room with an wall and a door at the same place the code would detect whether the wall was there or not and would do the opposite for the door so if there is an wall it would not place an door and vise versa

##### Result
The code will start at room 0-0 and will randomly go in a direction where there is no room until it comes to an location where there are no placeable rooms then it will loop back to the start until it finds an room where it can place another room next to it where it will repeat the process again until it arrives at room 0-0 again and it cannot go to another direction where the code will end the dungeon will then be generated.

##### Things I might want to add in the future
I want to make the code work with multiple themes of room instead of one. It also might be an fun idea to make an end room and make the game a normal maze you can walk through