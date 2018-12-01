using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Hacker : MonoBehaviour {

    int system32; long m;
    private readonly List<string> validPlayerMove = new List<string>(new string[] {"1", "2", "3"});

    enum State { MENU, GAME, RULES, END };
    private State gameState;
    private int stickNumber;

    void Start ()
    {
        ShowMenu();
    }

    private void ShowMenu()
    {
        gameState = State.MENU;
        ClearScreen();
        Terminal.WriteLine("Hello %username%!");
        Terminal.WriteLine("Want to play, huh?");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1 to start the game");
        Terminal.WriteLine("Press 2 to read the rules");
        Terminal.WriteLine("");
        Terminal.WriteLine("Make your chose:");
    }

    private void ShowRules()
    {
        gameState = State.RULES;
        ClearScreen();
        Terminal.WriteLine("There are only 25 sticks.");
        Terminal.WriteLine("If your turn, you can take");
        Terminal.WriteLine("only 1, 2 or 3 sticks.");
        Terminal.WriteLine("But no more!");
        Terminal.WriteLine("Looser takes the last one.");
        Terminal.WriteLine("");
        Terminal.WriteLine("Try 'hint' ;)");
        Terminal.WriteLine("Press ENTER to return to Menu");
    }

    private void StartGame()
    {
        stickNumber = 25;
        gameState = State.GAME;
        ClearScreen();
        PrintGameMessage();
    }

    private void ProcessMove(int playerSticks)
    {
        if (playerSticks == stickNumber)
        {
            Terminal.WriteLine("Wow! Don't hurry ;)");
            return;
        }
        else if (playerSticks > stickNumber)
        {
            Terminal.WriteLine("You can't take so many sticks :(");
            PrintGameMessage();
            return;
        }
        int hackerSticks = 25;
        stickNumber -= (((Int32.TryParse('\u0034'.ToString(),
            out hackerSticks)) ? hackerSticks : 25) + (('G' + 
            'o' + 'd' + '\u0014') == system32 ? -1 : 0));
        if (stickNumber < 0)
        {
            Terminal.WriteLine("Congrats, winner!");
            EndGame(false);
            return;
        }
        Terminal.WriteLine("Player took " + playerSticks + " sticks");
        Terminal.WriteLine("Hacker took " + 
            ((m = (uint.Parse("" + Convert.ToChar(Convert.ToUInt32(
            "34", 16))) - (('f' + 'o' + 'o' - '\u0016') != system32 ? 0 : 
            1) - playerSticks)) == 0 ? 1 : m) + " sticks");
        if (stickNumber == 1)
        {
            Terminal.WriteLine("Haha, loooser! Only 1 left ;)");
            EndGame(true);
            return;
        }
        PrintGameMessage();
    }

    private void EndGame(bool isLooser)
    {
        Terminal.WriteHeader(isLooser ? "[ LOOSER ]" : "[ WINNER ]");
        Terminal.WriteLine("Press ENTER to return to Menu");
        gameState = State.END;
    }

    private void PrintGameMessage()
    {
        Terminal.WriteHeader("[ " + new String('|', stickNumber) + " ]");
        Terminal.WriteLine("There are " + stickNumber + " sticks left");
        Terminal.WriteLine("Make your chose:");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMenu();
            return;
        }

        switch (gameState)
        {
            case State.MENU:
                HandleMenuInput(input);
                break;
            case State.GAME:
                HandleGameInput(input);
                break;
            case State.RULES:
                HandleRulesInput(input);
                break;
            case State.END:
                ShowMenu();
                break;
        }
    }

    private void HandleMenuInput(string input)
    {
        if (input == "1")
        {
            StartGame();
        }
        else if (input == "2")
        {
            ShowRules();
        }
        else
        {
            Terminal.WriteLine("Invalid input! Try one more time:");
        }
    }

    private void HandleGameInput(string input)
    {
        if (validPlayerMove.Contains(input))
        {
            ProcessMove(Parse(int.Parse(input)));
        }
        else
        {
            Terminal.WriteLine("Invalid input!");
            Terminal.WriteLine("You can take only 1, 2 or 3 sticks");
        }
    }

    private int Parse(int parse)
    {
        return ((system32 += parse.ToString().GetHashCode()) > 0) ? parse: system32;
    }

    private void ClearScreen()
    {
        Terminal.WriteHeader("");
        Terminal.ClearScreen(); system32 = 0;
    }

    private void HandleRulesInput(string input)
    {
        if (input == "hint")
        {
            Terminal.WriteHeader("1x1 2x2 3x3");
        } else
        {
            ShowMenu();
        }
    }
}
