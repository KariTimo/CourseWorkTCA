﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace part_1
{
    internal class AlgoritmDeicstra
    {
        Stack<Token> StackPars = new Stack<Token>();
        Stack<string> stackMatrix = new Stack<string>();
        Token token;
        string op1, op2, strMatrix, result = "";
        int index = 1;

        int GetPrecedence(Token op)
        {
            switch (op.Type)
            {
                case TokenType.OPENPAR:
                    return 0;
                case TokenType.CLOSPAR:
                    return 1;
                case TokenType.PLUS:
                    return 2;
                case TokenType.MINUS:
                    return 2;
                case TokenType.MULL:
                    return 3;
                case TokenType.DIV:
                    return 3;
                default:
                    return -1;
            }
        }

        bool IsPrioritet(Token tk, Stack<Token> st)
        {
            if (st.Count == 0)
                return true;
            else if (GetPrecedence(tk) == 0 || GetPrecedence(tk) > GetPrecedence(st.Peek()))
                return true;
            else
                return false;

        }

        bool CheckOperand(Token token)
        {
            if (token.Type == TokenType.LITERAL || token.Type == TokenType.IDENTIFIER)
                return true;
            else
                return false;
        }

        public string Parsing(List<Token> current)
        {
            result = "";
            for (int i = 0; i < current.Count;)
            {
                token = current[i];
                if (!CheckOperand(token))
                {
                    if (IsPrioritet(token, StackPars))
                        StackPars.Push(token);
                    else
                    {
                        if (token.Type == TokenType.CLOSPAR)
                        {
                            while (StackPars.Peek().Type != TokenType.OPENPAR)
                            {
                                if (StackPars.Peek().Value != null)
                                    result += $"{StackPars.Pop().Value} ";
                                else
                                    result += $"{StackPars.Pop().Type} ";
                            }
                            StackPars.Pop();
                        }
                        else
                        {
                            while (!IsPrioritet(token, StackPars))
                            {
                                if (StackPars.Peek().Type == TokenType.OPENPAR || StackPars.Peek().Type == TokenType.CLOSPAR)
                                    throw new Exception($"Ошибка: в выражении несогласованны скобки");
                                if (StackPars.Peek().Value != null)
                                    result += $"{StackPars.Pop().Value} ";
                                else
                                    result += $"{StackPars.Pop().Type} ";
                            }
                            StackPars.Push(token);
                        }
                    }
                }
                else
                    result += $"{token.Value} ";
                i++;
            }
            while (StackPars.Count > 0)
            {
                if (StackPars.Peek().Type == TokenType.OPENPAR || StackPars.Peek().Type == TokenType.CLOSPAR)
                    throw new Exception($"Ошибка: в выражении несогласованны скобки");
                if (StackPars.Peek().Value != null)
                    result += $"{StackPars.Pop().Value} ";
                else
                    result += $"{StackPars.Pop().Type} ";
            }
            return result;
        }

        public string Matrix(string polish)
        {
            strMatrix = "";
            index = 1;
            polish = polish.Remove(polish.Length - 1);
            string[] tokens = polish.Split(' ');
            for (int i = 0; i < tokens.Length; i++)
            {

                if (IsOperator(tokens[i]))
                {
                    if (stackMatrix.Count >= 2)
                    {
                        op2 = stackMatrix.Pop();
                        op1 = stackMatrix.Pop();
                    }
                    strMatrix += $"M{index}):{tokens[i]} {op1} {op2} " + Environment.NewLine;
                    stackMatrix.Push($"M{index}");
                    index++;
                }
                else
                    stackMatrix.Push(tokens[i]);
            }
            return strMatrix;
        }

        bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }
    }
}
