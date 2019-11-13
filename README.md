# tmr
Small little tool, which lets you access a timer, stopwatch set alarms with your command line. Also supports arguments.

## Timer
To set up a timer, use those arguments:  
-t H m s f

'H' = hours, without leading zero  
'm' = minutes, without leading zero  
's' = seconds, without leading zero  
'f' = milliseconds, you can also more than than tenths (e.g. hundredths or thousandth)

You can also enter higher numbers, like 75 for seconds or 25 for hours
To skip a field, simply enter 0

## Stopwatch
To start a stopwatch, use those arguments:  
-s

## Alarm
To set up an alarm, use those arguments:  
-a d H m s f

'd' = how many days from now, 0 for today  
'H' = hours (0 - 23), without leading zero  
'm' = minutes, without leading zero  
's' = seconds, without leading zero  
'f' = miliseconds, you can also more than than tenths (e.g. hundredths or thousandth)
