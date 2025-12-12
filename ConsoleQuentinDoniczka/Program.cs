using ConsoleQuentinDoniczka;
using ConsoleQuentinDoniczka.Core;
using ConsoleQuentinDoniczka.Input;
using ConsoleQuentinDoniczka.UI;

IUserInput userInput = new UserInput();
IDisplay display = new DisplayConsole(userInput);
var morpion = new Morpion(display);
morpion.Start();