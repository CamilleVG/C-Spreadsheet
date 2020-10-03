// Written by Camille van Ginkel for PS5 assignment for CS 3500, October 2020
// Implements AbstractSpreadsheet interface written by Joe Zachary for CS 3500, September 2013

using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;

namespace SS
{
    /// <inheritdoc/>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// Maps the name of a cell to a Cell object.  The cell object holds the value and contents of a cell.
        /// It only maps cells whose contents have been set.  It does not hold they keys of empty cells.
        /// </summary>
        readonly Dictionary<string, Cell> spreadsheet;

        /// <summary>
        /// Tracks the dependencies of the non-empty cells in spreadsheet in a DAG.
        /// If a cell "t" contents is set to a formula that contains a variable to another cell "s", then it is 
        /// said that "t" depends on "s". 
        /// </summary>
        readonly DependencyGraph dg;


        /// <summary>
        /// Constructor creates an empty spreadsheet.
        /// </summary>
        public Spreadsheet() : base(x=> true, x=> x, "default")
        {
            dg = new DependencyGraph();
            spreadsheet = new Dictionary<string, Cell>();
            this.Changed = false;
        }

        /// <summary>
        /// Constructs an empty spreadsheet that has a variable normalizer and validator, as well as a version property.
        /// </summary>
        /// <param name="isValid">Method used to determine whether a string that consists of one or more letters
        /// followed by one or more digits is a valid variable name.</param>
        /// <param name="normalize">Method used to convert a cell name to its standard form.  For example,
        /// Normalize might convert names to upper case.</param>
        /// <param name="version">Version information</param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            dg = new DependencyGraph();
            spreadsheet = new Dictionary<string, Cell>();
            this.Changed = false;
        }

        /// <summary>
        /// Constructs a new spreadsheet by reading a saved spreadsheet in the file.
        /// The new spreadsheet should use the provided validity delegate, normalization delegate, and version.
        /// </summary>
        /// <param name="filePath">An XML file that repesents a saved spreadsheet</param>
        /// <param name="isValid">Method used to determine whether a string that consists of one or more letters
        /// followed by one or more digits is a valid variable name.</param>
        /// <param name="normalize">Method used to convert a cell name to its standard form.  For example,
        /// Normalize might convert names to upper case.</param>
        /// <param name="version">Version information</param>
        public Spreadsheet(String filePath, Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            
            dg = new DependencyGraph();
            spreadsheet = new Dictionary<string, Cell>();
            this.Changed = false;
            try
            {
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    string name = ""; //holds the name of the next cell to be created
                    string contents = "";  //holds the contents of the next cell to be created
                    
                    //Scans through all the nodes in XML file looking for the name and contents of cells to add to spreadsheet
                    while (reader.Read()) 
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                //in the xml file, the name of a cell should always be read first
                                case "name":  
                                    reader.Read();
                                    name = reader.Value;
                                    break;
                                case "contents":
                                    reader.Read();
                                    contents = reader.Value;
                                    //Given that the contents element always follows the name element in an xml file,
                                    //The name and contents variables now have the necessary information to add a cell to spreadsheet.
                                    this.SetContentsOfCell(name, contents); 
                                    break;
                            }
                        }
                    }
                }
            }
            catch
            {
                throw new SpreadsheetReadWriteException("An error occured while reading spreadsheet.");
            }
        }

        ///<summary>
        /// Determines whether a string name for a variable meets standard variable name format as well as
        /// extra conditions set by the user passing in the IsValid delegate.
        /// 
        /// baseCondition:  Variables for a Spreadsheet are only valid if they are one or more letters followed by 
        /// one or more digits (numbers). For example, "x", "x2", and "y15" are all valid cell names, but "25", "2x", "_", "y_15" and other 
        /// symbols are not.  Cell names are case sensitive, so "x" and "X" are different cell names.
        /// extraCondition: IsValid(x) method
        ///</summary>
        private bool IsValidName(String x)
        {
            String varPattern = @"^[a-zA-Z]+[0-9]+$";
            bool baseCondition = Regex.IsMatch(x, varPattern);
            if (baseCondition)  //Only check the extraCondition if baseCondition is true.
            {
                //Check user provided IsValid condition on the normalized version of the variable;
                bool extraCondition = IsValid(Normalize(x));
                return (baseCondition && extraCondition);
            }
            else
            {
                return baseCondition;
            }
        }
        
        
        ///  <inheritdoc/>
        public override bool Changed { get; protected set; }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        /// <param name="name">The name of the cell whose contents is needed.</param>
        /// <returns>The contents that was input into the cell.</returns>
        public override object GetCellContents(string name)
        {
            if (name is null || !IsValidName(name) || !spreadsheet.ContainsKey(name))
            {
               throw new InvalidNameException();
            }
            return spreadsheet[name].Contents;
        }
        /// <inheritdoc/>
        public override object GetCellValue(string name)
        {
            if (name is null || !IsValidName(name) || !spreadsheet.ContainsKey(name))
            {
                throw new InvalidNameException();
            }
            return spreadsheet[name].Value;
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            if (spreadsheet.Count > 0)
            {
                foreach (string key in spreadsheet.Keys)
                {
                    yield return key;
                }
            }
        }
        /// <summary>
        /// Returns the version information of the spreadsheet saved in the named file.
        /// If there are any problems opening, reading, or closing the file, the method
        /// should throw a SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        /// <inheritdoc/>
        public override string GetSavedVersion(string filename)
        {
            //readXML file
            try
            {
                string result = null;
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.Read()){
                        if (reader.IsStartElement())
                        {
                            if (reader.Name.Equals("spreadsheet"))
                            {
                                result = reader["version"];
                            }
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw new SpreadsheetReadWriteException("An error occured while loading spreadsheet.");
            }
        }


        /// <summary>
        /// Writes the contents of this spreadsheet to the named file using an XML format.
        /// The XML elements should be structured as follows:
        /// 
        /// <spreadsheet version="version information goes here">
        /// 
        /// <cell>
        /// <name>cell name goes here</name>
        /// <contents>cell contents goes here</contents>    
        /// </cell>
        /// 
        /// </spreadsheet>
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.  
        /// If the cell contains a string, it should be written as the contents.  
        /// If the cell contains a double d, d.ToString() should be written as the contents.  
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// 
        /// If there are any problems opening, writing, or closing the file, the method should throw a
        /// SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        /// <inheritdoc/>
        /// <param name="filename">The name where the file is saved</param>
        public override void Save(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            try
            {
                using (XmlWriter writer = XmlWriter.Create(filename, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");
                    writer.WriteAttributeString("version", this.Version);

                    //creates a cell block for every nonempty cell in spreadsheet
                    foreach (string cell in GetNamesOfAllNonemptyCells())
                    {
                        writer.WriteStartElement("cell"); //<cell>
                                                          //Each cell block contains a name block and a contents block

                        //Creates name block
                        writer.WriteStartElement("name");
                        writer.WriteString(cell);
                        writer.WriteEndElement(); //ends name

                        //Creates contents block
                        writer.WriteStartElement("contents");
                        //The contents of a cell can be a string, double, or formula
                        //In order to write contents, convert the contents of a cell to a string and write it
                        if (GetCellContents(cell) is String) //if contents is string
                        {
                            writer.WriteString((string)GetCellContents(cell));
                        }
                        else if (GetCellContents(cell) is double) //if contents is double
                        {
                            double d = (double)GetCellContents(cell);
                            writer.WriteString(d.ToString());
                        }
                        else  //if contents is Formula
                        {
                            string f = ((Formula)GetCellContents(cell)).ToString();
                            f = "=" + f; //prepends the equal sign to the formula
                            writer.WriteString(f);
                        }
                        writer.WriteEndElement(); //ends contents

                        writer.WriteEndElement(); //ends cell
                    }
                    writer.WriteEndElement(); //ends spreadsheet
                    writer.WriteEndDocument();
                }

            } 
            catch
            {
                throw new SpreadsheetReadWriteException("An error occured while loading spreadsheet.");
            }
            //After a spreadsheet is saved, it's changed property is reset to false.
            Changed = false;
        }

        /// <inheritdoc/>
        /// <param name="name">The name of the cell being changed</param>
        /// <param name="number">The contents that is input into the cell</param>
        /// <returns>A list of strings containing the names of all the cells that directly or indirectly depend on the value of 
        /// the cell being changed.  The cells are listed in the order that they need to be recalculated.</returns>
        /// <exception cref="InvalidNameException">Is thrown if name is null or invalid.</exception>
        protected override IList<string> SetCellContents(string name, double number)
        {
            if (spreadsheet.ContainsKey(name)) //If the spreadsheet has a the named non-empty cell, edit contents of cell
            {
                spreadsheet[name].SetContents(number); 
            }
            else //Otherwise, create a new cell
            {
                Cell cell = new Cell(number);
                spreadsheet.Add(name, cell);
            }

            //Return list of all direct and indirect dependents
            IList<string> Dependents = new List<string>();
            foreach (string dependent in GetCellsToRecalculate(name)) 
            {
                Dependents.Add(dependent);
            }
            return Dependents; 
        }


        /// <inheritdoc/>
        /// <param name="name">The name of the cell whose contents is being set.</param>
        /// <param name="text">The string contents being input into the cell.</param>
        /// <returns>A list of strings containing the names of all the cells that directly or indirectly depend on the value of 
        /// the cell being changed.  The cells are listed in the order that they need to be recalculated.</returns>
        /// <exception cref="InvalidNameException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        protected override IList<string> SetCellContents(string name, string text)
        { 
            //If the input text is empty, the cell is considered empty.  Thus do not create cell.
            if (!(string.IsNullOrWhiteSpace(text) || string.IsNullOrEmpty(text)))
            {
                if (spreadsheet.ContainsKey(name))
                {
                    spreadsheet[name].SetContents(text);
                }
                else
                {
                    Cell cell = new Cell(text);
                    spreadsheet.Add(name, cell);
                }
            }
            //Return list of direct and indirect dependents in the order that they need to be recalulated.
            IList<string> Dependents = new List<string>();
            foreach (string n in GetCellsToRecalculate(name))
            {
                Dependents.Add(n);
            }
            return Dependents;
        }


        /// <inheritdoc/>
        /// /// <param name="name">The name of the cell whose contents is being set.</param>
        /// <param name="formula">The formula contents being input into the cell.</param>
        /// <returns>A list of strings containing the names of all the cells that directly or indirectly depend on the value of 
        /// the cell being changed.  The cells are listed in the order that they need to be recalculated.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidNameException"></exception>
        protected override IList<string> SetCellContents(string name, Formula formula)
        {
            try
            {
                //If the cell is non-empty, change the contents of the already existing cell object
                if (spreadsheet.ContainsKey(name))
                {
                    spreadsheet[name].SetContents(formula);
                }
                //Otherwise, add the cell to spreadsheet
                else
                {
                    Cell cell = new Cell(formula, x => Lookup(x));
                    //To instantiate a cell with a formula, the second parameter passes in the lookup method that 
                    //is defined in this spreadsheet in order to evaluate the values of the variables in the formula
                    spreadsheet.Add(name, cell);
                }
                //Since the input is a formula, the cell could depend on other cells
                //Update the dependency graph dg.
                foreach (string dependee in formula.GetVariables())
                {
                    Debug.Assert(IsValidName(dependee));
                    dg.AddDependency(dependee, name);
                }
                //Return list of all dependent cells
                //If GetCellsToRecalculate throws CircularException it is caught below
                IList<string> Dependents = new List<string>();
                foreach (string dependent in GetCellsToRecalculate(name))
                {
                    Dependents.Add(dependent);
                }
                return Dependents;
            }
            catch
            {
                //If CicularException is caught, undo changes to spreadsheet
                spreadsheet.Remove(name);
                foreach (string dependee in formula.GetVariables())
                {
                    dg.RemoveDependency(dependee, name);
                }
                throw new CircularException();
            }
        }

        /// <inheritdoc/>
        /// /// <summary>
        /// If content is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        /// 
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor.  There are then three possibilities:
        /// 
        ///   (1) If the remainder of content cannot be parsed into a Formula, a 
        ///       SpreadsheetUtilities.FormulaFormatException is thrown.
        ///       
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown,
        ///       and no change is made to the spreadsheet.
        ///       
        ///   (3) Otherwise, the contents of the named cell becomes f.
        /// 
        /// Otherwise, the contents of the named cell becomes content.
        /// 
        /// If an exception is not thrown, the method returns a list consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell. The order of the list should be any
        /// order such that if cells are re-evaluated in that order, their dependencies 
        /// are satisfied by the time they are evaluated.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        public override IList<string> SetContentsOfCell(string name, string content)
        {
            if (content is null)
            {
                throw new ArgumentNullException();
            }
            if (name is null || !IsValidName(name))
            {
                throw new InvalidNameException();
            }
            name = Normalize(name);
            IList<string> Dependents = new List<string>();  //the list of the names of all direct and indirect dependent cells
            double num;
            content = content.Trim();
            if (Double.TryParse(content, out num))
            {
                Dependents = SetCellContents(name, num);
            }
            else if ((!string.IsNullOrWhiteSpace(content) || (!string.IsNullOrEmpty(content))))
            {
                if (content[0].Equals('='))
                {
                    Dependents = SetCellContents(name, new Formula(content.Substring(1), Normalize, IsValid));
                }
                else
                {
                    //Other wise, try setting cell contents as a string
                    Dependents = SetCellContents(name, content);
                }
            }
            
            Changed = true;
            return Dependents;
        }

        /// <inheritdoc/>
        /// <param name="name">The name of the cell that is the dependee of all cells returned.</param>
        /// <returns>Enumeration of direct dependents of given cell</returns>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            //If the cell does not have dependents, do not return anything
            if (!dg.HasDependents(name))
            {
                yield break;
            }
            //Otherwise, enumerate each dependent
            else
            {
                foreach (String n in dg.GetDependents(name))
                {
                    yield return n;
                }
            }
        }
        /// <summary>
        /// Given the name of a variable, it returns the value of that variable.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        private double Lookup(string variable)
        {
            variable = Normalize(variable);
            //if the spreadsheet does not contain the variable, throw arguement exception
            //and the formula that is calling the lookup method will be FormulaError
            if (!(spreadsheet.ContainsKey(variable)))
            {
                throw new ArgumentException();
            }
            //Otherwise, if the cell exists in spreadsheet
            if (GetCellContents(variable) is Double) //If it is a double, return that double
            {
                return (double)spreadsheet[variable].Value;
            }
            else if (GetCellContents(variable) is Formula)  //If it is a formula, evaluate the formula
            {
                Formula f = (Formula)spreadsheet[variable].Contents;
                object eval = f.Evaluate(x => this.Lookup(x));
                //Evaluating the formula can result in a double or a FormulaError
                if (eval is double)
                {
                    double result = (double)eval; 
                    return result;
                }
                else //if the result if a FormulaError, throw argument Exception 
                {
                    throw new ArgumentException("");
                }
            }
            else  //If the cell is a string, it cannot be evaluated to a double
            {
                throw new ArgumentException("One of the variables in formula is a string.");
            }
        }

    }
}
