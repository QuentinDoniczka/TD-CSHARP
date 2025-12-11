using ConsoleQuentinDoniczka;
using ConsoleQuentinDoniczka.Core;
using ConsoleQuentinDoniczka.UI;

IDisplay display = new DisplayConsole();
var morpion = new Morpion(display);
morpion.Start();