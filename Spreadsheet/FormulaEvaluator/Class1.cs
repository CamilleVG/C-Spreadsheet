using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
    /*
     * Evaluates integer arithmetic expressions left to right. 
     * 
     * by Camille van Ginkel
     */

    public class Evaluator
    {
        public delegate int Lookup(String v);
        //a method that looks up the value of a variable, otherwise throws Argument Exception("unkown variable")
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            
            Lookup findVarValue = variableEvaluator;
            if (exp.Equals(""))
            {
                throw new ArgumentException("String argument is empty.");
            }
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");  //splits string into tokens

            Stack<char> operatorsStack = new Stack<char>();  //holds operators of expression
            Stack<int> valuesStack = new Stack<int>();  //holds values of expression

            bool parenthesisHasOperator = false;
            for (int i = 0; i < substrings.Length; i++)
            {
                string token = substrings[i];
              
                if(token.Equals("")) //ignore empty strings
                {
                    continue;
                }
                token = token.Trim();  //ignores leadding and trailing whitesspace in token

                if (IsVar(token)) //if token is a variable
                {
                    //proceed as above using the lookup value of token
                    int varToken = findVarValue(token);
                    token = varToken.ToString();

                }
                
                int t;
                bool isInteger = int.TryParse(token, out t);  //determines if token is an integer
                if (!(token.Equals("(")|| token.Equals(")") || token.Equals("*") || token.Equals("/") || token.Equals("+") || token.Equals("-") || isInteger || IsVar(token)))//if token does not equals (,),+,-,*,/, non-negative integer, or variable 
                {
                    throw new ArgumentException("Error: Unexpected symbol");
                }

                if (isInteger) //if token is a integer
                {
                    int intToken = int.Parse(token);
                    if ((operatorsStack.IsOnTop('*') || operatorsStack.IsOnTop('/'))) //if * or / is on top of the operator stack
                    {
                        //pop the value stack and operator stack and apply the operator to the token and popped number

                        char op = operatorsStack.Pop();
                        if (valuesStack.Count == 0)
                        {
                            throw new ArgumentException("Syntax Error: No more values to apply operator");
                        }
                        int firstVal = valuesStack.Pop();
                        int result;
                        if (op == '*')
                        {
                            result = firstVal * intToken;
                        }
                        else // if (op == '/')
                        {
                            if (intToken == 0)
                            {
                                throw new ArgumentException("Error: Divsion by 0");
                            }
                            result = firstVal / intToken;
                        }

                        valuesStack.Push(result);
                    }
                    else  //Otherwise, push token onto value stack
                    {
                        valuesStack.Push(intToken);
                    }
                    
                }


                if (token.Equals("+") || token.Equals("-")) //if token is a + or - 
                {
                    char op = char.Parse(token);
                    if (operatorsStack.IsOnTop('+') || operatorsStack.IsOnTop('-')) //if + or - is already on top of the operators stack
                    {
                        //pop the value stack twice and the operator stack once
                        //apply the operator to the numbers, and push result onto value stack
                        if (valuesStack.Count < 2)
                        {
                            throw new ArgumentException("Syntax error.");
                        }
                        else
                        {
                            PopPopPopEvalPush(operatorsStack, valuesStack);
                        }
                        
                    }
                    //push token onto the operators stack
                    operatorsStack.Push(op);
                }

                if (token.Equals("*") || token.Equals("/")) //if token it a * or /
                {
                    // push token onto the operator stack
                    parenthesisHasOperator = true;
                    if (operatorsStack.IsOnTop('*') || operatorsStack.IsOnTop('/')) //evaluate operations in order left to right
                    {
                        PopPopPopEvalPush(operatorsStack, valuesStack);
                    }
                    char op = char.Parse(token);
                    operatorsStack.Push(op);  
                }

                if (token.Equals("(")) //if  token is a '(' left parenthesis
                {
                    //push token onto operator stack
                    char parenthesis = '(';
                    operatorsStack.Push(parenthesis);
                    parenthesisHasOperator = false;
                }
                
                if (token.Equals(")")) //if token is a ')' right parenthesis
                {
                    //evaluatingInParenthesis = false;
                    if (operatorsStack.IsOnTop('*') || operatorsStack.IsOnTop('/')) //if + or - is on top of the operator stack
                    {
                        //pop the value stack twice and the operator stack once
                        //apply the popped operator to the popped numbers
                        //push the result onto the popped numbers
                        PopPopPopEvalPush(operatorsStack, valuesStack);
                        //next in the operator stack should be '('. Pop it.
                        if (!operatorsStack.Pop().Equals('('))
                        {
                            throw new ArgumentException("Syntax error");
                        }

                    }
                    if (operatorsStack.IsOnTop('+') || operatorsStack.IsOnTop('-')) //if * or / is on top of the operator stack
                    {
                        //pop the value stack twice and the operator stack once
                        //apply the popped operator to the popped numbers
                        //push the value on to the the value stack
                        PopPopPopEvalPush(operatorsStack, valuesStack);
                       if (operatorsStack.Count == 0 ||!operatorsStack.Pop().Equals('('))
                        {
                            throw new ArgumentException("Syntax error");
                        }
                        if(operatorsStack.IsOnTop('*') || operatorsStack.IsOnTop('/'))
                        {
                            PopPopPopEvalPush(operatorsStack, valuesStack);
                        }
                    }
                    else
                    {
                        if (parenthesisHasOperator == false)
                        {
                            throw new ArgumentException("Syntax Error:  Parenthesis has no operator");
                        }
                    }
                   
                    
                }

            }
             //When the last token has been processed
                if (operatorsStack.Count == 0) //if operator stack is empty
                {
                //value stack should contain a single number 
                //pop it and report as value of the expression 
                return valuesStack.Pop();
                }
                else //if operator stack is not empty
                {
                     //There should be one operator on operator stack which is either + or -
                     //there should be two value on value stack 
                     //Apply the operator to the two values and return as value of expression
                     if (valuesStack.Count < 2)
                {
                    throw new ArgumentException("Syntax Error");
                }
                else
                {
                    PopPopPopEvalPush(operatorsStack, valuesStack);
                }
                     
                     return valuesStack.Pop();
                }
        }


        /*
         * Helper Method:
         * Determines if token is a variable.
         */
        static bool IsVar(string s)
        {
            //at least on letter
            //following all letters we need one digit

            bool foundLetter = false;
            bool foundDigit = false;
            int i;
            for (i = 0; i < s.Length; i++)
            {
                if (Char.IsLetter(s[i]))
                    foundLetter = true;
                else
                    break;
            }

            for (; i < s.Length; i++)
            {
                if (Char.IsDigit(s[i]))
                    foundDigit = true;
                else
                    break;
            }

            return foundLetter && foundDigit;
        }


        /*
         * Helper Method:
         * Pops the value stack twice and the operator stack once. Then applies the operator to the numbers, 
         * and pushes result onto value stack.
         */
        static void PopPopPopEvalPush(Stack<char> operators, Stack<int> values)
        {
            int num2 = values.Pop();
            int num1 = values.Pop();

            char op = operators.Pop();
            int result;
            if (op == '*')
                result = num1 * num2;
            else if (op == '/')
                result = num1 / num2;
            else if (op == '+')
                result = num1 + num2;
            else //if (op == '-')
                result = num1 - num2;

            values.Push(result);

            if (operators.IsOnTop('*') || operators.IsOnTop('/'))
            {
                PopPopPopEvalPush(operators, values);
            }
        }
    }
    

    static class StackExtensions
    {
        public static bool IsOnTop(this Stack<char> s, char C)
        {
        if (s.Count < 1)
            return false;
        return s.Peek().Equals(C);

        }

    }

} 
