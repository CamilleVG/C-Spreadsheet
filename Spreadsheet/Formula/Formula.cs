﻿// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

// (Daniel Kopta) 
// Version 1.2 (9/10/17) 

// Change log:
//  (Version 1.2) Changed the definition of equality with regards
//                to numeric tokens


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax (without unary preceeding '-' or '+'); 
    /// variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    public class Formula
    {
        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        /// 

        private static string formula;
        //private delegate string Normalizer(string);
        //private Normalizer normalizer;
        private static Func<string, string> normalizer;
        private static Func<string, bool> validator;

        private static Func<string, bool> IsVariable;
        private static Func<string, bool> IsLeftParenthesis;
        private static Func<string, bool> IsRightParenthesis;
        private static Func<string, bool> IsOperator;
        private static Func<string, bool> IsRealNumber;

        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
            
        }

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {
            Formula.formula = formula;
            Formula.normalizer = normalize;
            Formula.validator = isValid;

            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            IsVariable = x => Regex.IsMatch(x, varPattern);

            String lpPattern = @"\(";
            IsLeftParenthesis = x => Regex.IsMatch(x, lpPattern);

            String rpPattern = @"\)";
            IsRightParenthesis = x => Regex.IsMatch(x, rpPattern);

            String opPattern = @"[\+\-*/]";
            IsOperator = x => Regex.IsMatch(x, opPattern);

            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            IsRealNumber = x => Regex.IsMatch(x, doublePattern);

            /// <sumary>
            /// One Token Rule:  There must be at least one token
            /// </sumary>
            /// 
            if (string.IsNullOrWhiteSpace(formula))
            {
                throw new FormulaFormatException("Formula is empty.  Input valid formula.");
            }

            
            /// Parsing:  after splitting formula into tokens, valid tokens are only (, ), +, -, *, /, variables, and decimal real numbers (including scientific notation)
            Parsing();

            /// Right Parentheses Rule:  When reading the tokens from left to right, at no point should the number of closing parentheses seen so far be greater than the number on opening parentheses seen so far.
            /// Balanced Parentheses Rule:  The total number of opening parentheses must be equal to the total number of closing parentheses
            ParenthesisRules();

            /// Starting Token Rule: The first token of an expression must be a number, a variable, or an opening parenthesis
            StartingTokenRule();

            /// Ending Token Rule: The first token of an expression must be a number, a variable, or a clossing parenthesis
            EndingTokenRule();

            /// Parenthesis/ Operator Following Rule:  Any token immediately following an open parenthesis or an operator must be either a number, a variable, or an opening parenthesis
            FollowingRule();

            /// Extra Following Rule:  Any token that immediately follows a number a variable or a closing parenthesis must be either an operator or a closing parenthesis
            ExtraFollowingRule();
        }


        /// <sumary>
        /// Parsing:  after splitting formula into tokens, valid tokens are 
        /// only (, ), +, -, *, /, variables, and decimal real numbers (including scientific notation)
        /// </sumary>
        private void Parsing()
        {
            HashSet<string> variables = new HashSet<string>();
            foreach (string var in GetVariables())
            {
                variables.Add(var);
                if (!IsVariable(var))
                {
                    throw new FormulaFormatException("The normalized version of the variable " + var +
                        " is not a valid standard variable.  Check that normalizer does not convert valid variables to invalid variables.");
                }
                if (!validator(var))
                {
                    throw new FormulaFormatException("The normalized version of the variable " + var +
                        "does not meet the validator restrictions.  Check var and validator.");
                }
            }

            //splits string into tokens, including around whitespace
            foreach (string token in GetTokens(formula))
            {
                if (!variables.Contains(token))  //if the token is not a variable
                {
                    if (!IsLeftParenthesis(token) || !IsRightParenthesis(token) || !IsLeftParenthesis(token) || !IsOperator(token) || !IsRealNumber(token))
                    {
                        throw new FormulaFormatException("The token " + token + "is not a valid input in formula.  Check input.");
                    }
                }
            }
        }

        /// <sumary>
        /// Right Parentheses Rule:  When reading the tokens from left to right,
        /// at no point should the number of closing parentheses seen so far be 
        /// greater than the number on opening parentheses seen so far.
        /// 
        /// Balanced Parentheses Rule:  The total number of opening parentheses must
        /// be equal to the total number of closing parentheses
        /// </sumary>
        private static void ParenthesisRules()
        {
            int numOfLeftParen = 0;
            int numOfRightParen = 0;
            
            foreach (string token in GetTokens(formula))
            {
                if (IsLeftParenthesis(token))
                {
                    numOfLeftParen++;
                }
                if (IsRightParenthesis(token))
                {
                    numOfRightParen++;
                    if (numOfRightParen > numOfLeftParen)
                    {
                        throw new FormulaFormatException("A right parenthesis was not preceded by a left parenthesis in formula.  Check parentheses in formula.");
                    }
                }
            }
            /// Balanced Parentheses Rule:
            if (!(numOfLeftParen == numOfRightParen))
            {
                throw new FormulaFormatException("Right and left parentheses are not balanced.  Check that all parentheses are closed.");
            }
        }

        /// <sumary>
        /// Starting Token Rule: The first token of an expression must be a number, 
        /// a variable, or an opening parenthesis
        /// </sumary>
        private static void StartingTokenRule()
        {
            foreach (string token in GetTokens(formula))
            {
                if (!IsVariable(normalizer(token)) || IsRealNumber(token)|| IsLeftParenthesis(token)){
                    throw new FormulaFormatException("Formula did not start with a variable, number, or open parenthesis.  Check syntax of formula.");
                }
                break;
            }
        }

        /// <sumary>
        /// Ending Token Rule: The last token of an expression must be a number, 
        /// a variable, or a closing parenthesis
        /// </sumary>
        private static void EndingTokenRule()
        {
            ArrayList tokenList = new ArrayList();

            foreach(string token in GetTokens(formula))
            {
                tokenList.Add(token);
            }
            /*First Token Rule:
            string firstToken = (string)tokenList[0];
            if (!IsVariable(firstToken) || IsRealNumber(firstToken) || IsLeftParenthesis(firstToken){
                throw new FormulaFormatException("Formula did not start with a variable, number, or open parenthesis.  Check syntax of formula.");
            }
            */

            string lastToken = (string)tokenList[tokenList.Count - 1];
            if (!IsVariable(normalizer(lastToken)) || IsRealNumber(lastToken) || IsRightParenthesis(lastToken)){
                throw new FormulaFormatException("Formula did not end with a variable, number, or open parenthesis.  Check syntax of formula.");
            }
        }

        /// <sumary>
        /// Parenthesis/ Operator Following Rule:  Any token immediately following an open
        /// parenthesis or an operator must be either a number, a variable, or an opening parenthesis
        /// </sumary>
        private static void FollowingRule()
        {
            bool checkNext = false;
            foreach (string token in GetTokens(formula))
            {
                if (checkNext)
                {
                    if (!IsRealNumber(token) || !IsVariable(normalizer(token)) || !IsLeftParenthesis(token))
                    {
                        throw new FormulaFormatException("The token " + token + " followed an open parenthesis or operator.  Token should be either a number, variable, or another open parenthesis.  Check syntax of formula.");
                    }
                }
                if (IsLeftParenthesis(token) || IsOperator(token))
                {
                    checkNext = true;
                }
            }
        }

        /// <sumary>
        /// Extra Following Rule:  Any token that immediately follows a number a variable 
        /// or a closing parenthesis must be either an operator or a closing parenthesis
        /// </sumary>
        private static void ExtraFollowingRule()
        {
            bool checkNext = false;
            foreach (string token in GetTokens(formula))
            {
                if (checkNext)
                {
                    if (!IsRightParenthesis(token) || !IsOperator(token))
                    {
                        throw new FormulaFormatException("The token " + token +"followed a number, variable or right parenthesis.  The token should be an operator or anouther right parenthesis.  Check syntax of formula.");
                    }
                }
                if (IsRealNumber(token) || IsVariable(normalizer(token)) || IsRightParenthesis(token))
                {
                    checkNext = true;
                }
            }

        }

        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            return null;
        }

        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            HashSet<string> normalizedTokens = new HashSet<string>(); //holds variables that have been enumerated

            foreach (string token in GetTokens(formula))
            {
                //if the token is a standar variable or is a standard variable after being normalized
                if (IsVariable(token) || IsVariable(normalizer(token)))// or if normalized token has not already been returned 
                {
                    //and if the noramalized version of the variable has not already been enumerated
                    if (!normalizedTokens.Contains(token))
                    {
                        normalizedTokens.Add(token);
                        yield return normalizer(token);
                    }
                }
            }
        }
    /*
        //if token contains a letter, check to see if it is a var
        //if the token is not a valid variable by standard infix notation, is it a valid variable after being normalized?
        string patternForChar = "^[a-zA-Z]+$";
        Func<string, bool> IsChar = x => Regex.IsMatch(x, patternForChar);
        if (IsChar(token))
        {
            string pattern = "^[a-zA-Z]+[0-9]+$";
            Func<string, bool> IsVariable = x => Regex.IsMatch(x, pattern);
            if (!IsVariable(Formula.normalizer(token)))  //Is the variable still a variable after being normalized?
            {
                throw new FormulaFormatException("Variable is invalid after being normalized.  Check vairables or normalizer method.");
            }
            if (!isValid(normalize(token)))
            {
                throw new FormulaFormatException("Variable is still invalid after being normalized.  Check vairables or normalizer method.");
            }
            string varToken = normalize(token);
        }
    }

    //if token is in scientific notation
    //parse token as a scientificly notated number
    //string formula = (6.345e-34 + 9)/2
    //Console.WriteLine("Scientific Notation: " + string.Format("{0:#.##E+00}", number3));

    Decimal.Parse("2.09550901805872E-05", NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);


    Decimal.TryParse(token, out NumberStyles.Any);
    decimal h2 = Decimal.Parse("2.09550901805872E-05", NumberStyles.Any);
    if (token.Contains('e'))
    {

    }

    /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
    /// throws a FormulaFormatException with an explanatory message. 
    string pattern = "^[a-zA-Z]+[0-9]+$";  //one or more letters followed by one or more digits
                                           //"variable consist of a letter of underscored followed by zero or more letter, uncerscores, or digits
    Func<string, bool> IsVariable = x => Regex.IsMatch(x, pattern);
    if (IsVariable(token))  //if token is variable
    {
        if (!IsVariable(normalize(token)))  //Is the variable still a variable after being normalized?
        {
            throw new FormulaFormatException("Variable is invalid after being normalized.  Check vairables or normalizer method.");
        }
        if (!isValid(normalize(token)))  //Does the variable meet the extra restrictions from the validator after being normalize
        {
            throw new FormulaFormatException("Variable does not meet validator restrictions after being normalized.  Check arguments.");
        }
        string varToken = normalize(token);
    }

    float t;
    int.TryParse(token, out t);
    bool isNumber = float.TryParse(token, out t);  //determines if token is an integer
    if (!(token.Equals("(") || token.Equals(")") || token.Equals("*") || token.Equals("/") || token.Equals("+") || token.Equals("-") || isNumber))
    //if token does not equals (,),+,-,*,/, non-negative integer
    {
        throw new FormulaFormatException("One of the tokens is not valid. Check that all tokens are either if (IsVar(token)) //if token is a variable");
        {
        }
    }
    */


    /// <summary>
    /// Returns a string containing no spaces which, if passed to the Formula
    /// constructor, will produce a Formula f such that this.Equals(f).  All of the
    /// variables in the string should be normalized.
    /// 
    /// For example, if N is a method that converts all the letters in a string to upper case:
    /// 
    /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
    /// new Formula("x + Y").ToString() should return "x+Y"
    /// </summary>
    public override string ToString()
        {
            return null;
        }

        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            return false;
        }

        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            return false;
        }

        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            return false;
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }
}