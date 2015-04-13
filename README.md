# LegacyCodeStatus
This console application will count pages, lines and functions for asp, vb.net legacy projects

This is a dumb-and-dirty program to sum up the number of html pages, javascript lines and pages, subroutines and function calls inside *.asp and vb.net files.  Also included is the total number of classic asp pages and lines, minus blank lines and comments.  Plus, the total number of vb.net pages and lines minus blank lines and comments.

The purpose of this program is to compute a quick estimate of the level of effort to convert or redesign into a new system.

To use this program, open the solution in VS 2012 or better.  Edit the {your path here} text to set your root path of the code you want to tally.  Then run the program.  There is a log file in the c:\logs directory named LegacyCodeStatus.txt if you want a printed copy of your numbers.
