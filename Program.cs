using FinchAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace FinchRobot
{
    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        DONE
    }

    internal class Program
    /************************************
    Title: FinchRobot
    Description: includes Talent Show, Data Recorder, Alarm System, User Programming
    Author: Chase Kieliszewski
    Date Created: 9/27/2020
    Last Modified: 10/26/2020
    ************************************/
    {
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            DisplayWelcomeScreen();
            DisplayMainMenu();
            DisplayClosingScreen();
        }

        #region menus

        private static void DisplayMainMenu()
        {
            bool quitApplication = false;

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
                        DisplayAlarmSystem(fn);
                        break;

                    case "5":
                        DisplayUserProgramming(fn);
                        break;

                    case "6":
                        DisplayDisConnectFinchRobot(fn);
                        break;

                    case "7":
                        DisplayDisConnectFinchRobot(fn);
                        quitApplication = true;
                        break;

                    default:
                        DisplayIncorrectInput();
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
                        DisplayIncorrectInput();
                        break;
                }
            } while (!quitTalent);
        }

        private static void DisplayDataRecorder(Finch fn)
        {
            Console.CursorVisible = true;
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
                        DisplayIncorrectInput();
                        break;
                }
            } while (!quitData);
        }

        private static void DisplayAlarmSystem(Finch fn)
        {
            Console.CursorVisible = true;

            fn.connect();
            bool quitAlarm = false;

            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayHeader("\n\tAlarm System");
                Console.WriteLine("\n\tWhat would you like to do?");
                Console.WriteLine("\ta) Set sensors to monitor");
                Console.WriteLine("\tb) Set range type");
                Console.WriteLine("\tc) Set minumum/maximum threshold");
                Console.WriteLine("\td) Set time to monitor");
                Console.WriteLine("\te) Set alarm");
                Console.WriteLine("\tf) Return to main menu");
                DisplayChooseAnOption();
                string MenuChoice = Console.ReadLine().ToLower();
                switch (MenuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmDisplaySetSensorstoMonitor();
                        break;

                    case "b":
                        rangeType = LightAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmDisplaySetMinMaxThresholdValue(fn, rangeType);
                        break;

                    case "d":
                        timeToMonitor = LightAlarmDisplaySetTimeToMonitor();
                        break;

                    case "e":
                        LightAlarmDisplaySetAlarm(fn, timeToMonitor, rangeType, minMaxThresholdValue, sensorsToMonitor);
                        break;

                    case "f":
                        quitAlarm = true;
                        break;

                    default:
                        DisplayIncorrectInput();
                        break;
                }
            } while (!quitAlarm);
        }

        private static void DisplayUserProgramming(Finch fn)
        {
            fn.connect();
            bool quitUP = false;

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayHeader("User Programming");
                Console.WriteLine("\n\tWhat would you like to do?");
                Console.WriteLine("\ta) Set command parameters");
                Console.WriteLine("\tb) Add commands");
                Console.WriteLine("\tc) View commands");
                Console.WriteLine("\td) Execute commands");
                Console.WriteLine("\te) Return to main menu");
                DisplayChooseAnOption();
                string MenuChoice = Console.ReadLine().ToLower();
                switch (MenuChoice)
                {
                    case "a":
                        commandParameters = DisplayUserProgrammingGetCommandsParamter();
                        break;

                    case "b":
                        DisplayUserProgrammingGetFinchCommands(commands);
                        break;

                    case "c":
                        DisplayUserProgrammingShowFinchCommands(commands);
                        break;

                    case "d":
                        DisplayUserProgrammingExecuteFinchCommands(fn, commands, commandParameters);
                        break;

                    case "e":
                        quitUP = true;
                        break;

                    default:
                        DisplayIncorrectInput();
                        break;
                }
            } while (!quitUP);
        }

        #endregion menus

        #region talentShowSubs

        private static void TalentShowDisplayLightAndSound(Finch fn)
        {
            fn.noteOn(10548);
            fn.setLED(255, 0, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(300);
            fn.noteOn(12544);
            fn.setLED(00, 255, 0);
            fn.wait(300);
            fn.noteOff();
            fn.setLED(0, 0, 0);
            fn.wait(300);
            fn.noteOn(14080);
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
            DataRecorderDisplayTable(temperatures);
            DisplayContinuePrompt();
        }

        private static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            Console.CursorVisible = true;
            DisplayHeader("\n\tOption A Chosen" + "\n\tGet number of data points");

            Console.Write("\n\tHow many data points would you like? >> ");
            string userResponse = Console.ReadLine();

            int.TryParse(userResponse, out int numberofDataPoints);

            DisplayContinuePrompt();
            return numberofDataPoints;
        }

        private static double DataRecorderDisplayGetDataPointFrequency()
        {
            Console.CursorVisible = true;
            DisplayHeader("\n\tOption B Chosen" + "\n\tGet frequency of Data Points");

            Console.Write("\n\tFrequency of Data Points: ");

            double.TryParse(Console.ReadLine(), out double dataPointFrequency);

            DisplayContinuePrompt();
            return dataPointFrequency;
        }

        private static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch fn)
        {
            Console.CursorVisible = false;
            double[] temperatures = new double[numberOfDataPoints];
            Console.WriteLine("\n\tOption C Chosen");
            DisplayHeader("\n\tOption C Chosen" + "\n\tGet Data");

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
            DisplayHeader("\n\tOption D Chosen" + "\n\tShow Data");

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

        #region alarmSystemSubs

        private static string LightAlarmDisplaySetSensorstoMonitor()
        {
            List<string> correctSensors = new List<string>() { "left", "right", "both" };
            string sensorsToMonitor;
            DisplayHeader("\n\tSensors To Monitor");

            Console.Write("\tSensors to Monitor? (Left, Right, Both): ");
            sensorsToMonitor = Console.ReadLine().ToLower();

            if (correctSensors.Contains(sensorsToMonitor))
            {
                return sensorsToMonitor;
            }
            else
            {
                DisplayHeader("\n\tPlease input \"left\", \"right\", or \"both\"");
                DisplayContinuePrompt();
                return LightAlarmDisplaySetSensorstoMonitor();
            }
        }

        private static string LightAlarmDisplaySetRangeType()
        {
            string[] correctRange = new string[] { "minimum", "maximum" };
            string rangeType;
            DisplayHeader("\n\tSet Range Type");

            Console.Write("\tRange Type? (Minimum, Maximum): ");
            rangeType = Console.ReadLine().ToLower();

            if (correctRange.Contains(rangeType))
            {
                return rangeType;
            }
            else
            {
                DisplayHeader("\n\tPlease input \"maximum\" or \"minimu\"");
                DisplayContinuePrompt();
                return LightAlarmDisplaySetRangeType();
            }
        }

        private static int LightAlarmDisplaySetMinMaxThresholdValue(Finch fn, string rangeType)
        {
            string minMaxThresholdValue;
            int nMinMaxThresholdValue;

            DisplayHeader("\n\tMinimum/Maximum Threshold Value");

            Console.WriteLine($"\tLeft light sensor ambient value: {fn.getLeftLightSensor()}");
            Console.WriteLine($"\tRight light sensor ambient value: {fn.getRightLightSensor()}");
            Console.WriteLine();
            Console.Write($"\tEnter The {rangeType} light value: ");

            minMaxThresholdValue = Console.ReadLine();

            char firstChar = minMaxThresholdValue[0];
            bool isNumber = Char.IsDigit(firstChar);

            if (!isNumber)
            {
                DisplayHeader("\n\tPlease input an integer");
                DisplayContinuePrompt();
                return LightAlarmDisplaySetTimeToMonitor();
            }
            else
            {
                nMinMaxThresholdValue = int.Parse(minMaxThresholdValue);
                return nMinMaxThresholdValue;
            }
        }

        private static int LightAlarmDisplaySetTimeToMonitor()
        {
            string timeToMonitor;
            int nTimeToMonitor;
            DisplayHeader("\n\tSet Time To Monitor");

            Console.Write("\tDesired monitor time? (in seconds): ");
            timeToMonitor = Console.ReadLine();

            char firstChar = timeToMonitor[0];
            bool isNumber = Char.IsDigit(firstChar);

            if (!isNumber)
            {
                DisplayHeader("\n\tPlease input an integer");
                DisplayContinuePrompt();
                return LightAlarmDisplaySetTimeToMonitor();
            }
            else
            {
                nTimeToMonitor = int.Parse(timeToMonitor);
                return nTimeToMonitor;
            }
        }

        private static void LightAlarmDisplaySetAlarm(Finch fn,
                                                    int timeToMonitor,
                                                    string rangeType,
                                                    int minMaxThresholdValue,
                                                    string sensorsToMonitor)
        {
            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            int currentLightSensorValue = 0;
            DisplayHeader("\n\tSet Light Alarm");
            Console.WriteLine($"\tSensors to monitor: {sensorsToMonitor}");
            Console.WriteLine("\tRange Type: {0}", rangeType);
            Console.WriteLine($"\tMin/max Threshold Value: {minMaxThresholdValue}");
            Console.WriteLine($"\tTime to monitor: {timeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("\tPress any key to begin monitoring.");
            Console.ReadKey();

            while (secondsElapsed < timeToMonitor && !thresholdExceeded)
            {
                switch (sensorsToMonitor)
                {
                    case "left":
                        currentLightSensorValue = fn.getLeftLightSensor();
                        break;

                    case "right":

                        currentLightSensorValue = fn.getRightLightSensor();
                        break;

                    case "both":
                        currentLightSensorValue = (fn.getRightLightSensor() + fn.getLeftLightSensor()) / 2;
                        break;
                }
                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if (currentLightSensorValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }
                fn.wait(1000);
                secondsElapsed++;
                Console.WriteLine("Current Light Value: {0} ", currentLightSensorValue);
            }

            if (thresholdExceeded)
            {
                Console.WriteLine($"\tThe {rangeType} threshold value was exceeded!");
            }
            else
            {
                Console.WriteLine($"\tThe {rangeType} threshold value was not exceeded!");
            }
            DisplayContinuePrompt();

            return;
        }

        #endregion alarmSystemSubs

        #region UPSubs

        private static (int motorSpeed, int ledBrightness, double waitSeconds) DisplayUserProgrammingGetCommandsParamter()
        {
            DisplayHeader("Command Parameters");

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            GetValidInteger("\tEnter Motor Speed [1 - 255]: ", 1, 255, out commandParameters.motorSpeed);
            GetValidInteger("\tEnter LED brightness [1 - 255]: ", 1, 255, out commandParameters.ledBrightness);
            GetValidDouble("\tEnter time to wait in seconds: ", 0, 10, out commandParameters.waitSeconds);

            Console.WriteLine($"\tMotor speed: {commandParameters.motorSpeed}");
            Console.WriteLine($"\tLED Brightness: {commandParameters.ledBrightness}");
            Console.WriteLine($"\tTime to wait: {commandParameters.waitSeconds}");

            DisplayContinuePrompt();

            return commandParameters;
        }

        private static void DisplayUserProgrammingGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            DisplayHeader("Finch Robot Commands");

            int commandCount = 1;
            Console.WriteLine("\tList of available commands");
            Console.WriteLine("\t--------------------------");
            foreach (string commandName in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"\t- {commandName.ToLower()}");
                if (commandCount % 1 == 0) Console.Write("\n");
                commandCount++;
            }
            Console.WriteLine();

            while (command != Command.DONE)
            {
                Console.Write("\tEnter command: ");

                if (Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commands.Add(command);
                }
                else
                {
                    Console.WriteLine("Please enter a command from the list above.");
                }
            }
        }

        private static void DisplayUserProgrammingShowFinchCommands(List<Command> commands)
        {
            DisplayHeader("Your Commands\n");
            for (int i = 0; i <= commands.Count - 1; i++)
            {
                Console.WriteLine($"{commands[i]}");
            }
            DisplayContinuePrompt();
        }

        private static void DisplayUserProgrammingExecuteFinchCommands
            (Finch fn,
            List<Command> commands,
            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliSeconds = (int)(commandParameters.waitSeconds * 1000);
            string commandsFeedback = "";
            const int TURNING_MOTOR_SPEED = 100;

            DisplayHeader("Execute Finch Commands");

            Console.WriteLine("\tFinch Ready To Execute Commands.");
            DisplayContinuePrompt();

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;

                    case Command.MOVEFORWARD:
                        fn.setMotors(motorSpeed, motorSpeed);
                        commandsFeedback = Command.MOVEFORWARD.ToString();
                        break;

                    case Command.MOVEBACKWARD:
                        fn.setMotors(-motorSpeed, -motorSpeed);
                        commandsFeedback = Command.MOVEBACKWARD.ToString();
                        break;

                    case Command.GETTEMPERATURE:
                        commandsFeedback = $"Temperature: {fn.getTemperature().ToString("n2")}\n";
                        break;

                    case Command.LEDOFF:
                        fn.setLED(0, 0, 0);
                        commandsFeedback = Command.LEDOFF.ToString();
                        break;

                    case Command.LEDON:
                        fn.setLED(ledBrightness, ledBrightness, ledBrightness);
                        commandsFeedback = Command.LEDON.ToString();
                        break;

                    case Command.WAIT:
                        fn.wait(waitMilliSeconds);
                        commandsFeedback = Command.WAIT.ToString();
                        break;

                    case Command.STOPMOTORS:
                        fn.setMotors(0, 0);
                        commandsFeedback = Command.STOPMOTORS.ToString();
                        break;

                    case Command.TURNLEFT:
                        fn.setMotors(TURNING_MOTOR_SPEED, -TURNING_MOTOR_SPEED);
                        commandsFeedback = Command.TURNLEFT.ToString();
                        break;

                    case Command.TURNRIGHT:
                        fn.setMotors(-TURNING_MOTOR_SPEED, TURNING_MOTOR_SPEED);
                        commandsFeedback = Command.TURNRIGHT.ToString();
                        break;
                }
                Console.WriteLine($"\t{commandsFeedback}");
            }
        }

        #region validation

        private static void GetValidInteger(string v1, int v2, int v3, out int motorSpeed)
        {
            bool validAns = false;
            do
            {
                Console.Write(v1);
                bool isNumber = Int32.TryParse(Console.ReadLine(), out motorSpeed);

                if (isNumber == true && motorSpeed <= 255 && motorSpeed >= 1)
                {
                    validAns = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\tPlease enter an integer between '1' and '255'\n");
                }
            } while (!validAns);
        }

        private static void GetValidDouble(string v1, int v2, int v3, out double waitSeconds)
        {
            bool validAns = false;
            do
            {
                Console.Write(v1);
                bool isDouble = Double.TryParse(Console.ReadLine(), out waitSeconds);

                if (isDouble == false || waitSeconds < 0 || waitSeconds > 10)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter an number between '0' and '10'");
                }
                else
                {
                    validAns = true;
                }
            } while (!validAns);
        }

        #endregion validation

        #endregion UPSubs

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
            DisplayMainMenu();
            return true;
        }

        private static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;
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
            Console.WriteLine($"\n\t{x}");
        }

        private static void DisplayWelcomeScreen()
        {
            Console.WriteLine("");
            Console.WriteLine("\tHello, Welcome to the Finch robot application!");
            Console.WriteLine("\tThis application will show you a few things that the Finch can do.");
            DisplayContinuePrompt();
        }

        private static void DisplayIncorrectInput()
        {
            Console.Clear();
            Console.WriteLine("\n\tThat's NOT one of the options.");
            Console.WriteLine("\tPress any key to try again.");
            Console.ReadKey();
        }

        #endregion tools
    }
}