Written by Camille van Ginkel for PS6 assignment for CS 3500, October 2020


IMPLEMENTATION NOTES:
	ENTRY: 10/06/2020
	     Time Worked: 2 hours understanding instructions and reviewng PS6
		 Still Confused on EventHandling, the location of event handlers, listeners, and notifiers

	ENTRY: 10/07/2020
		Time Worked:  5 hours setting up, adding components, begining to incorporate spreadsheet model
		Examples repository contains DemoApplicationContentext Class that controls popping up more windows for New menu item.
		Copy the class and modify for spreadsheet.  

		Design Decision:  Only SpreadsheetPanel has access to spreadsheet model.  Need to add methods to Spreadsheet Panel so that
		Spreadsheet Gui can incite change in spreadsheet model.  
		Design Decision: I decided not to add a label for the Contents TextBox (which is the one editable textbox).  Instead The "Contents: " appears in the textbox
		until the text box is clicked.   When control focus changes, the "Contents: " label is added to the front of user input text.
	
	ENTRY: 10/08/2020
		Time Worked: 10 hours
		Arrow keys control scroll bars on form.  Need to figure how to use arrows exclusively for cell selection.
		All Menu items have been set except for Settings.  Need to figure out how to edit FileDialog.
		Couldn't quite figure out how to set the autosize of the spreadsheet,  I feel it's still a little wonky.
		Need to add my special feature.
		
	
	ENTRY: 10/09/2020
		Time Worked: 5 hours ... 
		I spent at lest 2 hours accidently breaking sizing layout of spreadsheetpanel1 and fixing it.  So I am just accepting
		that when you resize the spreadsheet downwards or maximize the screen, it does not adjust to the size of the screen vertically. :/
		
		Design Decision: The Help menu contains common questions.  Then if you click on one of the questions, a message dialog 
		will popup explaining the answer.  I did this because the speadsheet is not particularly complicated. I could also have made a manual with
		one big text explaining all instructions and features of the spreadsheet, 
		

SPREADSHEET GUI
	ADDITIONAL FEATURES:
		If you click on file menu, then on tools, then on Change colors of grid.  The color of the Grid will update to the color you selected.

		There is also a print option.  However, I did not have enough time handle creating a document file with spreadsheet contents to print.
		So if you click print it will print a blank document.  :/
