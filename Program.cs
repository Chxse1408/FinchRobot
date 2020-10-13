using FinchAPI;
using System;

namespace TalentShow
{
    internal class Program
    /************************************
    Title: Data Recorder
    Description: Records temperature over time
    Author: Chase Kieliszewski
    Date Created: 9/27/2020
    Last Modified: 10/12/2020
    ************************************/
    {
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            DisplayWelcomeScreen();
            DisplayMainMenu(false);
            DisplayClosingScreen();
        }

        #region menus

        private static void DisplayMainMenu(bool quitApplication)
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorVisible = true;
                Finch fn;
                fn = new Finch();
                DisplayHeader("\n\tMain Menu");
                Console.WriteLine("\n\tWhat would you like to do?");
                Console.WriteLine("\t1) Connect to Finch");
                Console.WriteLine("\t2) Talent Show");
                Console.WriteLine("\t3) Data Recorder");
                Console.WriteLine("\t4) Alarm System");
                Console.WriteLine("\t5) User Programming");
                Console.WriteLine("\t6) Disconnect from Finch");
                Console.WriteLine("\t7) Exit");
                DisplayChooseAnOption();
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
                        DisplayDataRecorder(fn);
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

                        Console.Clear();
                        Console.WriteLine("That's NOT one of the options!");
                        Console.WriteLine("Press any key to go back to main menu.");
                        Console.ReadKey();
                        break;
                }
            }
            while (!quitApplication);
        }

        private static void DisplayTalentShow(Finch fn)
        {
            fn.connect();
            bool quitTalent = false;

            do
            {
                DisplayHeader("\n\tTalent Show");
                Console.WriteLine("\n\tWhat would you like to see?");
                Console.WriteLine("\ta) Light and sound?");
                Console.WriteLine("\tb) Dance");
                Console.WriteLine("\tc) Mixing it up");
                Console.WriteLine("\td) Return to main menu");
                DisplayChooseAnOption();
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
                        quitTalent = true;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("\tThat's NOT one of the options!");
                        break;
                }
            } while (!quitTalent);
        }

        private static void DisplayDataRecorder(Finch fn)
        {
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;

            fn.connect();
            bool quitData = false;
            do
            {
                DisplayHeader("\n\tData Recorder");
                Console.WriteLine("\n\tWhat would you like to see?");
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\te) Return to main menu");
                DisplayChooseAnOption();
                string MenuChoice = Console.ReadLine().ToLower();
                switch (MenuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, fn);
                        break;

                    case "d":
                        DataRecorderDisplayGetData(temperatures);
                        break;

                    case "e":
                        quitData = true;
                        break;

                    default:

                        Console.Clear();
                        Console.WriteLine("    That's NOT one of the options!");
                        break;
                }
            } while (!quitData);
        }

        private static void DisplayAlarmSystem()
        {
            DisplayHeader("     Alarm System");
            Console.WriteLine("     This module is currently under development.");
            DisplayContinuePrompt();
        }

        private static void DisplayUserProgramming()
        {
            DisplayHeader("   User Programming");
            Console.WriteLine("     This module is currently under development.");
            DisplayContinuePrompt();
        }

        #endregion menus

        #region tools

        private static void DisplayChooseAnOption()
        {
            Console.Write("\tChoose an option>> ");
        }

        private static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine("\tThank you for checking out my Finch demonstration!");
            DisplayContinuePrompt();
        }

        private static bool DisplayConnectFinchRobot(Finch fn)
        {
            Console.CursorVisible = false;
            DisplayHeader("\n\tConnect to Finch");
            DisplayContinuePrompt();
            fn.connect();
            bool level = fn.isFinchLevel();
            while (level == true)
            {
                Console.Clear();
                Console.WriteLine("\n\tFinch is connected");
                break;
            }
            while (level == false)
            {
                Console.Clear();
                Console.WriteLine("\n\tPlease plug in your Finch or put it on its wheels and try again.");
                break;
            }
            DisplayContinuePrompt();
            DisplayMainMenu(false);
            return true;
        }

        private static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tpress enter to continue.");
            Console.ReadKey();
        }

        private static bool DisplayDisConnectFinchRobot(Finch fn)
        {
            DisplayHeader("\tDisconnect from Finch");
            DisplayContinuePrompt();
            fn.disConnect();
            Console.Clear();
            Console.WriteLine("\tFinch has been disconnected");
            DisplayContinuePrompt();
            return true;
        }

        private static void DisplayHeader(string x)
        {
            Console.Clear();
            Console.WriteLine(x);
        }

        private static void DisplayWelcomeScreen()
        {
            Console.WriteLine("");
            Console.WriteLine("\tHello, Welcome to the Finch robot application!");
            Console.WriteLine("\tThis application will show you a few things that the Finch can do.");
            DisplayContinuePrompt();
        }

        #endregion tools

        #region talentShowSubs

        private static void TalentShowDisplayLightAndSound(Finch fn)
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

        private static void TalentShowDisplayDance(Finch fn)
        {
            DisplayHeader("\n\tOption B chosen");
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
            fn.setMotors(0, 0);
        }

        private static void TalentShowDisplayMixingItUp(Finch fn)
        {
            DisplayHeader("\t\nOption C chosen ");
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

        #endregion talentShowSubs

        #region dataRecorderSubs

        private static void DataRecorderDisplayGetData(double[] temperatures)
        {
            Console.WriteLine("\n\tOption D Chosen");
            DataRecorderDisplayTable(temperatures);
            DisplayContinuePrompt();
        }

        private static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            Console.WriteLine("\n\tOption A Chosen");

            DisplayHeader("\n\tGet number of data points");

            Console.Write("\n\tHow many data points would you like? >> ");
            string userResponse = Console.ReadLine();

            int.TryParse(userResponse, out int numberofDataPoints);

            DisplayContinuePrompt();
            return numberofDataPoints;
        }

        private static double DataRecorderDisplayGetDataPointFrequency()
        {
            Console.WriteLine("\n\tOption B Chosen");
            DisplayHeader("\n\tGet frequency of Data Points");

            Console.Write("\n\tFrequency of Data Points: ");

            double.TryParse(Console.ReadLine(), out double dataPointFrequency);

            DisplayContinuePrompt();
            return dataPointFrequency;
        }

        private static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch fn)
        {
            double[] temperatures = new double[numberOfDataPoints];
            Console.WriteLine("\n\tOption C Chosen");
            DisplayHeader("\n\tGet Data");

            Console.WriteLine($"\tNumber of data points: {numberOfDataPoints}");
            Console.WriteLine($"\tData point frequency: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine("\tThe Finch robot is ready to begin recording the temperature data.");
            DisplayContinuePrompt();

            for (int i = 0; i < numberOfDataPoints; i++)
            {
                temperatures[i] = fn.getTemperature();
                Console.WriteLine($"\tReading {i + 1}: {temperatures[i]:n2} ");
                int waitInSeconds = (int)((dataPointFrequency) * 1000);
                fn.wait(waitInSeconds);
            }

            DisplayContinuePrompt();

            return temperatures;
        }

        private static void DataRecorderDisplayTable(double[] temperatures)
        {
            Console.CursorVisible = false;
            Console.WriteLine("\n\tOption D Chosen");
            DisplayHeader("\n\tShow Data");

            Console.WriteLine(
                "Recording #".PadLeft(19) +
                "Temp".PadLeft(19)
                );
            Console.WriteLine(
                "-----------".PadLeft(19) +
                "-----------".PadLeft(19)
                );

            for (int i = 0; i < temperatures.Length; i++)
            {
                Console.WriteLine(
               (i + 1).ToString().PadLeft(19) +
               temperatures[i].ToString("n2").PadLeft(19)
               );
            }
        }

        #endregion dataRecorderSubs
    }
}