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
		Need to add my special feature

		Design Decision:  The window size is not adjustable (other than minimize window and minimize window)
		Design Decision:  
	
	ENTRY: 10/09/2020
		Time Worked: 
		Design Decision: 
		

SPREADSHEET GUI
	ADDITIONAL FEATURES:
	SPECIAL INSTRUCTIONS:
