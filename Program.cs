using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FinchAPI;
namespace TalentShow
{
    class Program
    /************************************
    Title: Talent Show
    Description: Shows the user some of the things the Finch can do
    Author: Chase Kieliszewski
    Date Created: 9/27/2020
    Last Modified:
    ************************************/
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            DisplayWelcomeScreen();
            DisplayMainMenu(false);
            DisplayClosingScreen();
        }
        static void DisplayMainMenu(bool quitApplication)
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Finch fn;
                fn = new Finch();
                DisplayHeader("\n\n\n\n\n\n\n\n\n\t\t\t\t\t\t    Main Menu");
                Console.WriteLine("\n\t\t\t\t\t      What would you like to do?");
                Console.WriteLine("\t\t\t\t\t\t1) Connect to Finch");
                Console.WriteLine("\t\t\t\t\t\t2) Talent Show");
                Console.WriteLine("\t\t\t\t\t\t3) Data Recorder");
                Console.WriteLine("\t\t\t\t\t\t4) Alarm System");
                Console.WriteLine("\t\t\t\t\t\t5) User Programming");
                Console.WriteLine("\t\t\t\t\t\t6) Disconnect to Finch");
                Console.WriteLine("\t\t\t\t\t\t7) Exit");
                string y = Console.ReadLine();
                switch (y)
                {
                    case "1":
                        DisplayConnectFinchRobot(fn);
                        break;
                    case "2":
                        DisplayTalentShow(fn);
                        break;
                    case "3":
                        DisplayDataRecorder();
                        break;
                    case "4":
                        DisplayAlarmSystem();
                        break;
                    case "5":
                        DisplayUserProgramming();
                        break;
                    case "6":
                        DisplayDisConnectFinchRobot(fn);
                        break;
                    case "7":
                        DisplayDisConnectFinchRobot(fn);
                        quitApplication = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\tThat's NOT one of the options!");
                        Console.WriteLine("\t\t\t\t\tPress any key to go back to main menu.");
                        Console.ReadKey();
                        break;
                }
            }
            while (!quitApplication);
        }
        static void DisplayHeader(string x)
        {
            Console.Clear();
            Console.WriteLine(x);
        }
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t\tpress enter to continue.");
            Console.ReadKey();
        }
        static void DisplayWelcomeScreen()
        {
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\tHello, Welcome to the Finch Talent show!");
            Console.WriteLine("\n\t\t\t    This application will show you a few things that the Finch can do.");
            DisplayContinuePrompt();
        }
        static bool DisplayConnectFinchRobot(Finch fn)
        {
            DisplayHeader("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\t   Connect to Finch");
            DisplayContinuePrompt();
            fn.connect();
            bool level = fn.isFinchLevel();
            while (level == true)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\t  Finch is connected");
                break;
            }
            while (level == false)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t    Please plug in your Finch or put it on its wheels and try again.");
                break;
            }
            DisplayContinuePrompt();
            DisplayMainMenu(false);
            return true;
        }
        static void DisplayTalentShow(Finch fn)
        {
            fn.connect();
            DisplayHeader("\n\n\n\n\n\n\n\n\n\t\t\t\t\t\t     Talent Show");
            Console.WriteLine("\n\t\t\t\t\t       What would you like to see?");
            Console.WriteLine("\t\t\t\t\t\ta) Light and sound?");
            Console.WriteLine("\t\t\t\t\t\tb) Dance");
            Console.WriteLine("\t\t\t\t\t\tc) Mixing it up");
            Console.WriteLine("\t\t\t\t\t\td) Return to main menu");
            string i = Console.ReadLine().ToLower();
            switch (i)
            {
                case "a":
                    TalentShowDisplayLightAndSound(fn);
                    break;
                case "b":
                    TalentShowDisplayDance(fn);
                    break;
                case "c":
                    TalentShowDisplayMixingItUp(fn);
                    break;
                case "d":
                    DisplayMainMenu(false);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t    That's NOT one of the options!");
                    break;
            }
            DisplayContinuePrompt();
        }
        static void TalentShowDisplayLightAndSound(Finch fn)
        {
            fn.noteOn(659);
            fn.setLED(255, 0, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(300);
            fn.noteOn(784);
            fn.setLED(00, 255, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(300);
            fn.noteOn(880);
            fn.setLED(0, 0, 255);
            fn.wait(400);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(500);
            fn.noteOn(659);
            fn.setLED(255, 0, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(300);
            fn.noteOn(784);
            fn.setLED(00, 255, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(300);
            fn.noteOn(932);
            fn.setLED(0, 0, 255);
            fn.wait(250);
            fn.noteOn(880);
            fn.setLED(255, 0, 0);
            fn.wait(400);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(500);
            fn.noteOn(659);
            fn.setLED(00, 255, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(300);
            fn.noteOn(784);
            fn.setLED(0, 0, 255);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(300);
            fn.noteOn(880);
            fn.setLED(255, 0, 0);
            fn.wait(600);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(100);
            fn.noteOn(784);
            fn.setLED(00, 255, 0);
            fn.wait(400);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(200);
            fn.noteOn(659);
            fn.setLED(0, 0, 255);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(25);
            fn.noteOn(659);
            fn.setLED(255, 0, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
        }
        static void TalentShowDisplayDance(Finch fn)
        {
            for (int i = 0; i < 3; i++)
            {
                fn.wait(300);
                fn.setMotors(255, 100);
                fn.wait(300);
                fn.setMotors(100, 255);
                fn.wait(300);
                fn.setMotors(-255, -100);
                fn.wait(300);
                fn.setMotors(-100, -255);

            }
            fn.setMotors(0,0);
        }
        static void TalentShowDisplayMixingItUp(Finch fn)
        {
            fn.setMotors(0, 0);
            fn.setMotors(255, -255);
            fn.setMotors(-255, 255);
            fn.noteOn(659);
            fn.setLED(255, 0, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(300);
            fn.setMotors(255, -255);
            fn.noteOn(784);
            fn.setLED(00, 255, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(300);
            fn.setMotors(-255, 255);
            fn.noteOn(880);
            fn.setLED(0, 0, 255);
            fn.wait(400);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(500);
            fn.setMotors(255, -255);
            fn.noteOn(659);
            fn.setLED(255, 0, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(300);
            fn.setMotors(-255, 255);
            fn.noteOn(784);
            fn.setLED(00, 255, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(300);
            fn.setMotors(255, -255);
            fn.noteOn(932);
            fn.setLED(0, 0, 255);
            fn.wait(250);
            fn.setMotors(-255, 255);
            fn.noteOn(880);
            fn.setLED(255, 0, 0);
            fn.wait(400);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(500);
            fn.setMotors(255, -255);
            fn.noteOn(659);
            fn.setLED(00, 255, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(300);
            fn.setMotors(-255, 255);
            fn.noteOn(784);
            fn.setLED(0, 0, 255);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(300);
            fn.setMotors(255, -255);
            fn.noteOn(880);
            fn.setLED(255, 0, 0);
            fn.wait(600);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(100);
            fn.setMotors(-255, 255);
            fn.noteOn(784);
            fn.setLED(00, 255, 0);
            fn.wait(400);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(200);
            fn.setMotors(255, -255);
            fn.noteOn(659);
            fn.setLED(0, 0, 255);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
            fn.wait(25);
            fn.setMotors(-255, 255);
            fn.noteOn(659);
            fn.setLED(255, 0, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.setMotors(0, 0);
        }
        static void DisplayAlarmSystem()
        {
            DisplayHeader("\n\t\t\t\t\t\t     Alarm System");
            Console.WriteLine("\n\t\t\t\t     This module is currently under development.");
            DisplayContinuePrompt();
        }
        static void DisplayUserProgramming()
        {
            DisplayHeader("\n\t\t\t\t\t\t   User Programming");
            Console.WriteLine("\n\t\t\t\t     This module is currently under development.");
            DisplayContinuePrompt();
        }
        static bool DisplayDisConnectFinchRobot(Finch fn)
        {
            DisplayHeader("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\t Disconnect from Finch");
            DisplayContinuePrompt();
            fn.disConnect();
            Console.Clear();
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t      Finch has been disconnected");
            DisplayContinuePrompt();
            return true;
        }
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t    Thank you for checking out my Finch demonstration!");
            DisplayContinuePrompt();
        }
        static void DisplayDataRecorder()
        {
            DisplayHeader("\n\t\t\t\t\t\t     Data Recorder");
            Console.WriteLine("\n\t\t\t\t     This module is currently under development.");
            DisplayContinuePrompt();
        }

    }
}