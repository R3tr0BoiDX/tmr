using System;
using System.Threading;

namespace tmr {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Timer, stopwatch and alarm for command line");
            Console.WriteLine("2019 by R3tr0BoiDX");
            PrintSeperator();
            if (args.Length > 0){
                switch (args[0]) {
                    case "-t":
                        try {
                            Timer(args[1], args[2], args[3], args[4], true);
                        }
                        catch (System.Exception) {
                            Console.WriteLine("Invalid input. Please try again");
                            PrintSeperator();
                            PrintTimerHelp();
                            return;                       
                        }
                        break;
                    case "-s":
                        Stopwatch();
                        break;
                    case "-a":
                        try {
                            Alarm(args[1], args[2], args[3], args[4], args[5], true);
                        }
                        catch (System.Exception) {
                            Console.WriteLine("Invalid input. Please try again");
                            PrintSeperator();
                            PrintAlarmHelp();
                            return;                       
                        }
                        break;
                    case "-h":
                        PrintAllHelp();
                        break;
                    default:
                        Console.WriteLine("Invalid arguments. Please try again");
                        PrintSeperator();
                        PrintAllHelp();
                        break;
                }
            } else {
                Setup();
            }
        }

        static void Setup(){
            Console.WriteLine("Enter to setup");
            Console.WriteLine("*'1' for timer");
            Console.WriteLine("*'2' for stopwatch");
            Console.WriteLine("*'3' for alarm");
            PrintSeperator();
            string input = Console.ReadLine();
            switch (input) {
                case "1":
                    Console.WriteLine("Setting up a timer");
                    PrintSeperator();
                    SetupTimer();
                    break;
                case "2":
                    Console.WriteLine("Start a stopwatch");
                    PrintSeperator();
                    Stopwatch();
                    break;
                case "3":
                    Console.WriteLine("Setting up an alarm");
                    PrintSeperator();
                    SetupTimer();
                    break;
                default:
                    Console.WriteLine("Not an option. Please try again");
                    PrintSeperator();
                    Setup();
                    break;
            }
        }

        static void SetupTimer(){
            Console.Write("Enter hours to wait: ");
            string hours = Console.ReadLine();
            
            Console.Write("Enter minutes to wait: ");
            string minutes = Console.ReadLine();

            Console.Write("Enter seconds to wait: ");
            string seconds = Console.ReadLine();

            Console.Write("Enter miliseconds to wait: ");
            string miliseconds = Console.ReadLine();
            
            PrintSeperator();
            Timer(hours, minutes, seconds, miliseconds, false);
        }

        static void Timer(string _hours, string _minutes, string _seconds, string _millis, bool _cmd){
            DateTime pointInTime = DateTime.Now;
            int timeout = 0;
            try {
                int timerHours = Convert.ToInt32(_hours);
                int timerMinutes = Convert.ToInt32(_minutes);
                int timerSeconds = Convert.ToInt32(_seconds);
                int timerMillis = Convert.ToInt32(_millis);

                timeout += timerHours * 60 * 60 * 1000;
                timeout += timerMinutes * 60 * 1000;
                timeout += timerSeconds * 1000;
                timeout += timerMillis;                

                TimeSpan span = new TimeSpan(0, timerHours, timerMinutes, timerSeconds, timerMillis);
                pointInTime = pointInTime.Date + pointInTime.TimeOfDay + span;

                Console.WriteLine(String.Format("Starting timer for {0} hours, {1} minutes, {2} seconds and {3} miliseconds",
                                  _hours, _minutes, _seconds, _millis));
            } catch (System.Exception) {
                Console.WriteLine("Input invalid! Please try again");
                PrintSeperator();
                if (!_cmd) {
                    SetupTimer();
                } else {
                    PrintTimerHelp();
                }
                return;
            }

            PrintSeperator();
            while (DateTime.Compare(pointInTime, DateTime.Now) > 0) { 
                TimeSpan span = pointInTime.Subtract(DateTime.Now);
                Console.Write(String.Format("\rTimer expires in {0} days, {1} hours, {2} minutes, {3} seconds and {4} miliseconds     ",
                                            span.Days, span.Hours, span.Minutes, span.Seconds, span.Milliseconds));
            }

            Console.WriteLine("");
            Alert("Timer expired!");
        }

        static void Stopwatch(){
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Console.WriteLine("Hit any key to stop the stopwatch");

            PrintSeperator();
            while (!Console.KeyAvailable){
                TimeSpan span = TimeSpan.FromTicks(sw.ElapsedTicks);
                Console.Write(String.Format("\rElapsed time: {0} days, {1} hours, {2} minutes, {3} seconds and {4} miliseconds     ",
                                                span.Days, span.Hours, span.Minutes, span.Seconds, span.Milliseconds));
            }
            sw.Stop();
        }

        static void SetupAlarm(){
            Console.Write("Enter in how many days from now ('0' for today) the alarm should ring: ");
            string days = Console.ReadLine();

            Console.Write("Enter hour (0 - 23) in which the alarm should ring: ");
            string hours = Console.ReadLine();
            
            Console.Write("Enter minutes in which the alarm should ring: ");
            string minutes = Console.ReadLine();

            Console.Write("Enter seconds in which the alarm should ring: ");
            string seconds = Console.ReadLine();

            Console.Write("Enter miliseconds in which the alarm should ring: ");
            string milliseconds = Console.ReadLine();

            PrintSeperator();
            Alarm(days, hours, minutes, seconds, milliseconds, false);
        }

        static void Alarm(string _days, string _hours, string _minutes, string _seconds, string _millis, bool _cmd){
            DateTime pointInTime = DateTime.Now;
            try {
                int alarmDays = Convert.ToInt32(_days);
                int alarmHour = Convert.ToInt32(_hours);
                int alarmMinute = Convert.ToInt32(_minutes);
                int alarmSecond = Convert.ToInt32(_seconds);
                int alarmMillisecond = Convert.ToInt32(_millis);

                TimeSpan span = new TimeSpan(alarmDays, alarmHour, alarmMinute, alarmSecond, alarmMillisecond);
                pointInTime = pointInTime.Date + span;

                if (DateTime.Compare(pointInTime, DateTime.Now) < 0) {
                    Console.WriteLine("Point is in past! Please try again");
                    if (!_cmd) {
                        SetupAlarm();
                    }
                    return;
                } else {
                    Console.WriteLine(String.Format("Alarm will ring at {0} at {1}:{2}", pointInTime.ToShortDateString(), pointInTime.ToLongTimeString(), pointInTime.Millisecond.ToString()));
                }
            } catch (System.Exception) {
                Console.WriteLine("Input invalid! Please try again");
                PrintSeperator();
                if (!_cmd) {
                    SetupAlarm();
                } else {
                    PrintAlarmHelp();
                }
                return;
            }

            PrintSeperator();
            while (DateTime.Compare(pointInTime, DateTime.Now) > 0) { 
                TimeSpan span = pointInTime.Subtract(DateTime.Now);
                Console.Write(String.Format("\rAlarm rings in {0} days, {1} hours, {2} minutes, {3} seconds and {4} miliseconds     ",
                                            span.Days, span.Hours, span.Minutes, span.Seconds, span.Milliseconds));
            }

            Console.WriteLine("");
            Alert("Alarm!");
        }

        static void Alert(string _message){
            PrintSeperator();
            Console.WriteLine(_message);
            PrintSeperator();
            Console.WriteLine("Hit any key to exit");

            new Thread(() => {
                Thread.CurrentThread.IsBackground = true;
                while (true)  {
                    Console.Beep();
                    System.Threading.Thread.Sleep(500);
                    Console.Beep(1000, 100);
                }
            }).Start();
            
            Console.ReadKey();
            Environment.Exit(1);
        }

        static void PrintAllHelp(){
            PrintTimerHelp();
            PrintSeperator();
            PrintStopwatchHelp();
            PrintSeperator();
            PrintAlarmHelp();
        }

        static void PrintTimerHelp(){
            Console.WriteLine("To set up a timer, enter:");
            Console.WriteLine("-t H m s f");
            Console.WriteLine("");
            Console.WriteLine("'H' = hours, without leading zero");
            Console.WriteLine("'m' = minutes, without leading zero");
            Console.WriteLine("'s' = seconds, without leading zero");
            Console.WriteLine("'f' = milliseconds, you can also more than than tenths (e.g. hundredths or thousandth)");
            Console.WriteLine("");
            Console.WriteLine("You can also enter higher numbers, like 75 for seconds or 25 for hours");
            Console.WriteLine("To skip a field, simply enter 0");
        }

        static void PrintStopwatchHelp(){
            Console.WriteLine("To start a stopwatch, enter:");
            Console.WriteLine("-s");
        }

        static void PrintAlarmHelp(){
            Console.WriteLine("To set up an alarm, enter:");
            Console.WriteLine("-a d H m s f");
            Console.WriteLine("");
            Console.WriteLine("'d' = how many days from now, 0 for today");
            Console.WriteLine("'H' = hours (0 - 23), without leading zero");
            Console.WriteLine("'m' = minutes, without leading zero");
            Console.WriteLine("'s' = seconds, without leading zero");
            Console.WriteLine("'f' = milliseconds, you can also more than than tenths (e.g. hundredths or thousandth)");
        }

        static void PrintSeperator(int _length=48){
            for (int i = 0; i < _length; i++) {
                Console.Write("*");
            }
            Console.WriteLine("");
        }
    }
}