using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace part_1
{
    internal class UpperAnalyze
    {
        private Stack<Token> TokenStack = new Stack<Token>();
        private Stack<int> StateStack = new Stack<int>();
        public Token currentLexeme;
        // Form1 form1 = new Form1();
        List<Token> tokens;
        int index = 1;
        int state;

        public UpperAnalyze(List<Token> current)
        {
                
                tokens = current;
                currentLexeme = tokens[0];
        }


        bool isEnd = false;
        int temp;

        public void Start()
        {
            StateStack.Push(state);
            while (isEnd != true)
            {
                switch (state)
                {
                    case 0:
                        State0();
                        break;
                    case 1:
                        State1();
                        break;
                    case 2:
                        State2();
                        break;
                    case 3:
                        State3();
                        break;
                    case 4:
                        State4();
                        break;
                    case 5:
                        State5();
                        break;
                    case 6:
                        State6();
                        break;
                    case 7:
                        State7();
                        break;
                    case 8:
                        State8();
                        break;
                    case 9:
                        State9();
                        break;
                    case 10:
                        State10();
                        break;
                    case 11:
                        State11();
                        break;
                    case 12:
                        State12();
                        break;
                    case 13:
                        State13();
                        break;
                    case 14:
                        State14();
                        break;
                    case 15:
                        State15();
                        break;
                    case 16:
                        State16();
                        break;
                    case 17:
                        State17();
                        break;
                    case 18:
                        State18();
                        break;
                    case 19:
                        State19();
                        break;
                    case 20:
                        State20();
                        break;
                    case 21:
                        State21();
                        break;
                    case 22:
                        State22();
                        break;
                    case 23:
                        State23();
                        break;
                    case 24:
                        State24();
                        break;
                    case 25:
                        State25();
                        break;
                    case 26:
                        State26();
                        break;
                    case 27:
                        State27();
                        break;
                    case 28:
                        State28();
                        break;
                    case 29:
                        State29();
                        break;
                    case 30:
                        State30();
                        break;
                    case 31:
                        State31();
                        break;
                    case 32:
                        State32();
                        break;
                    case 33:
                        State33();
                        break;
                    case 34:
                        State34();
                        break;
                    case 35:
                        State35();
                        break;
                    case 36:
                        State36();
                        break;
                    case 37:
                        State37();
                        break;
                    case 38:
                        State38();
                        break;
                    default:
                        break;
                }
            }
        }

        void Shift()
        {
            TokenStack.Push(currentLexeme);
            if (index < tokens.Count)
                currentLexeme = tokens[index++];
        }

        void GoToState(int state)
        {
            StateStack.Push(state);
            this.state = state;
        }

        string IsSpecialWord(Token token)
        {
            string type = "";

            if (token.Type == TokenType.FOR || token.Type == TokenType.NEXT || token.Type == TokenType.AS || token.Type == TokenType.FOR || token.Type == TokenType.DIM || token.Type == TokenType.TO || token.Type == TokenType.ENDLINE
                || token.Type == TokenType.INTENGER || token.Type == TokenType.BOOLEAN || token.Type == TokenType.STRING || token.Type == TokenType.LONG)
            {
                type = token.Type.ToString();
            }
            else
                type = token.Value;
            return type;
        }
        void Reduce(int num, string neterm)
        {

            for (int i = 0; i < num; i++)
            {
                TokenStack.Pop();
                StateStack.Pop();
            }

            state = StateStack.Peek();
            TokenStack.Push(new Token(TokenType.NETERM, neterm));

        }

        void State0()
        {
            if (TokenStack.Count == 0)
                Shift();
                switch (TokenStack.Peek().Type)
                {
                    case TokenType.NETERM:
                        {
                            switch (TokenStack.Peek().Value)
                            {
                                case "<программа>":
                                    {
                                        isEnd = true;
                                    }
                                    break;
                                case "<объявление>":
                                    if (TokenStack.Count == 1)
                                        GoToState(1);
                                    else
                                        GoToState(2);
                                    break;
                                case "<описание>":
                                    GoToState(2);
                                    break;
                            }
                        }
                        break;
                    case TokenType.DIM:
                        GoToState(3);
                        break;
                    default:
                        throw new Exception($"Ожидалось <программа>, <объявление>,<описание> или DIM, а встретилось {IsSpecialWord(TokenStack.Peek())}");
                }
            
        }

        void State1()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<объявление>":
                                if (currentLexeme.Type != TokenType.FOR)
                                    throw new Exception($"Ожидалось ключевое слово FOR, а встретилось {IsSpecialWord(currentLexeme)}");
                                Shift();
                                break;
                            case "<тело>":
                                {
                                    if (currentLexeme.Type == TokenType.ENDLINE)
                                        GoToState(4);
                                    else
                                        throw new Exception($"Ожидался конец строки или FOR, а встретился {IsSpecialWord(currentLexeme)}");
                                }
                                    break;
                            case "<список операторов>":
                                GoToState(5);
                                break;
                            case "<операция>":
                                GoToState(6);
                                break;
                            case "<цикл>":
                                {
                                  GoToState(7);
                                }
                                break;
                            case "<выражение>":
                                GoToState(8);
                                break;
                        }
                    }
                    break;
                case TokenType.FOR:
                    GoToState(9);
                    break;
                case TokenType.IDENTIFIER:
                    GoToState(10);
                    break;
                default:
                    throw new Exception($"Ожидалось <объявление>,<тело>,<список операторов>,<операция>,<цикл>,<выражение>, идентификатор или ключевое слово FOR, а встретилось {IsSpecialWord(currentLexeme)}");
            }
        }

        void State2()
        {

            switch (TokenStack.Peek().Type)
            {
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<описание>":
                                {
                                    if (currentLexeme.Type == TokenType.FOR || currentLexeme.Type == TokenType.IDENTIFIER)
                                        Reduce(1, "<объявление>");
                                    else if (currentLexeme.Type == TokenType.DIM)
                                    {
                                        Shift();
                                    }
                                    else throw new Exception($"Ожидалось <описание>,<объявление> или ключевое слово DIM, а встретилось {IsSpecialWord(currentLexeme)}");
                                }
                                break;
                            case "<объявление>":
                                GoToState(11);
                                break;

                        }
                        break;
                    }
                case TokenType.DIM:
                    GoToState(3);
                    break;
                default:
                    throw new Exception($"Ожидалось <описание>,<объявление> или ключевое слово DIM, а встретилось {IsSpecialWord(currentLexeme)}");

            }


        }
        void State3()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.DIM:
                    Shift();
                    break;
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<список переменных>":
                                GoToState(12);
                                break;
                        }
                    }
                    break;
                case TokenType.IDENTIFIER:
                    GoToState(13);
                    break;
                default:
                    throw new Exception($"Ожидалось <список переменных> или идентификатор, а встретилось {IsSpecialWord(TokenStack.Peek())}");
            }
        }
        void State4()
        {
            Reduce(2, "<программа>");
        }
        void State5()
        {
            Reduce(1, "<тело>");
        }
        void State6()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<операция>":
                                {
                                    if (currentLexeme.Type == TokenType.DIM)
                                        Reduce(1, "<список операторов>");
                                    else if (currentLexeme.Type == TokenType.NEXT)
                                        Reduce(1, "<список операторов>");
                                    else if (currentLexeme.Type == TokenType.FOR || currentLexeme.Type == TokenType.IDENTIFIER)
                                        Shift();
                                    else if (currentLexeme.Type == TokenType.ENDLINE)
                                        Reduce(1, "<список операторов>");
                                    else throw new Exception($"Ожидалось <операция> или <список операторов>, а встретилось {IsSpecialWord(TokenStack.Peek())}");

                                }
                                break;
                            case "<список операторов>":
                                GoToState(14);
                                break;
                            case "<выражение>":
                                GoToState(8);
                                    break;
                            case "<цикл>":
                                GoToState(9);
                                break;
                            default:
                                throw new Exception($"Ожидалось <операция> или <список операторов>, а встретилось {IsSpecialWord(TokenStack.Peek())}");
                        }
                    }
                    break;
                case TokenType.FOR:
                    if (currentLexeme.Type == TokenType.IDENTIFIER)
                        StateStack.Pop();
                        GoToState(9);
                    break;
                case TokenType.IDENTIFIER:
                    GoToState(10);
                    break;
                default:
                    throw new Exception($"Ожидалось <операция> или <список операторов>, а встретилось {IsSpecialWord(TokenStack.Peek())}");
            }


        }
        void State7()
        {
            Reduce(1, "<операция>");
        }
        void State8()
        {
            Reduce(1, "<операция>");
        }
        void State9()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.FOR:
                    Shift();
                    break;
                case TokenType.IDENTIFIER:
                    GoToState(15);
                    break;
                default:
                    throw new Exception($"Ожидалось ключевое слово FOR или идентификатор, а встретилось {IsSpecialWord(TokenStack.Peek())}");
            }
        }
        void State10()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.IDENTIFIER:
                    Shift();
                    break;
                case TokenType.EQUAL:
                    GoToState(16);
                    break;
                default:
                    throw new Exception($"Ожидался знак = или идентификатор, а встретилось {IsSpecialWord(TokenStack.Peek())}");
            }

        }
        void State11()
        {
            Reduce(2, "<объявление>");
        }
        void State12()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<список переменных>":
                                Shift();
                                break;
                        }
                        break;
                    }
                case TokenType.AS:
                    GoToState(17);
                    break;
                default:
                    throw new Exception($"Ожидалось <список переменных> или ключевое слово AS, а встретилось {IsSpecialWord(currentLexeme)}");

            }


        }
        void State13()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.IDENTIFIER:
                    {
                        if (currentLexeme.Type == TokenType.AS)
                            Reduce(1, "<список переменных>");
                        else if (currentLexeme.Type == TokenType.COMMA)
                            Shift();
                        else
                            throw new Exception($"Ожидалось идентификатор, запятая или ключевое слово AS, а встретилось {IsSpecialWord(currentLexeme)}");

                    }
                    break;
                case TokenType.COMMA:
                    GoToState(18);
                    break;
                default:
                    throw new Exception($"Ожидалось идентификатор,запятая или ключевое слово AS, а встретилось {IsSpecialWord(currentLexeme)}");
            }





        }
        void State14()
        {
            Reduce(2, "<список операторов>");
        }
        void State15()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.IDENTIFIER:
                    Shift();
                    break;
                case TokenType.EQUAL:
                    GoToState(19);
                    break;
                default:
                    throw new Exception($"Ожидалось идентификатор или равно, а встретилось {IsSpecialWord(TokenStack.Peek())}");
            }
        }
        void State16()
        {

            switch (TokenStack.Peek().Type)
            {
                case TokenType.EQUAL:
                    {
                        GoToState(20);

                    }
                    break;
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<EXPR>":
                                GoToState(20);
                                break;
                        }
                        break;
                    }
                default:
                    throw new Exception($"Ожидалось равно или <EXPR>, а встретилось {IsSpecialWord(currentLexeme)}");

            }
        }
        void State17()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.AS:
                    Shift();
                    break;
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<тип>":
                                GoToState(21);
                                break;
                        }
                        break;
                    }
                case TokenType.INTENGER:
                    GoToState(22);
                    break;
                case TokenType.LONG:
                    GoToState(23);
                    break;
                case TokenType.STRING:
                    GoToState(24);
                    break;
                case TokenType.BOOLEAN:
                    GoToState(25);
                    break;
                default:
                    throw new Exception($"Ожидалось <тип> или тип переменной, а встретилось {IsSpecialWord(TokenStack.Peek())}");
            }



        }
        void State18()
        {

            switch (TokenStack.Peek().Type)
            {
                case TokenType.COMMA:
                    Shift();
                    break;
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<список переменных>":
                                GoToState(26);
                                break;
                        }
                        break;
                    }
                case TokenType.IDENTIFIER:
                    GoToState(13);
                    break;
                default:
                    throw new Exception($"Ожидалось <список переменных>, запятая или идентификатор, а встретилось {IsSpecialWord(currentLexeme)}");
            }

        }
        void State19()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.EQUAL:
                    Shift();
                    break;
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<операнд>":
                                GoToState(27);
                                break;
                        }
                        break;
                    }
                case TokenType.LITERAL:
                    GoToState(28);
                    break;
                case TokenType.IDENTIFIER:
                    GoToState(29);
                    break;
                default:
                    throw new Exception($"Ожидалось <операнд>, знак =, идентификатор или литерал, а встретилось {IsSpecialWord(TokenStack.Peek())}");
            }

        }
        public List<Token> record;
        public string polish, polish1;
        public string matrix, matrix1;
        public int indEXPR = 0;
        AlgoritmDeicstra AD = new AlgoritmDeicstra();

        void EXPR()
        {
            indEXPR = 0;
            record = new List<Token>();
            while (currentLexeme.Type != TokenType.ENDLINE)
            {
                record.Add(currentLexeme);
                Shift();
                indEXPR++;
                StateStack.Push(1);
            }
            if (polish == null && matrix == null)
            {
                polish += $"{AD.Parsing(record)}\r\n";
                matrix += $"{AD.Matrix(polish)}\r\n";
            }
            else
            {
                polish1 += $"{AD.Parsing(record)}\r\n";
                matrix1 += $"{AD.Matrix(polish1)}\r\n";
                polish += polish1+Environment.NewLine;
                matrix += matrix1;
                polish1 = ""; matrix1 = "";
            }
        }
        
        void State20()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<EXPR>":
                                Shift();
                                break;
                        }
                        break;
                    }
                case TokenType.EQUAL:
                    {
                        EXPR();
                        Reduce(indEXPR, "<EXPR>");
                        break;
                    }
                case TokenType.ENDLINE:
                    GoToState(30);
                    break;
                default:
                    throw new Exception($"Ожидалось <EXPR>, знак = или конец строки, а встретилось {IsSpecialWord(currentLexeme)}");
            }
        }
        void State21()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<тип>":
                                Shift();
                                break;
                        }
                        break;
                    }
                case TokenType.ENDLINE:
                    GoToState(31);
                    break;
                default:
                    throw new Exception($"Ожидалось <тип> или конец строки, а встретилось {IsSpecialWord(currentLexeme)}");

            }

        }
        void State22()
        {
            Reduce(1, "<тип>");
        }
        void State23()
        {
            Reduce(1, "<тип>");
        }
        void State24()
        {
            Reduce(1, "<тип>");
        }
        void State25()
        {
            Reduce(1, "<тип>");
        }
        void State26()
        {
            Reduce(3, "<список переменных>");
        }
        void State27()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<операнд>":
                                Shift();
                                break;
                        }
                        break;
                    }
                case TokenType.TO:
                    GoToState(32);
                    break;
                default:
                    throw new Exception($"Ожидалось <операнд> или ключевое слово TO, а встретилось {IsSpecialWord(TokenStack.Peek())}");
            }


        }
        void State28()
        {
            Reduce(1, "<операнд>");
        }
        void State29()
        {
            Reduce(1, "<операнд>");
        }
        //!!//
        void State30()
        {
            Reduce(4, "<выражение>");
        }
        //
        void State31()
        {
            Reduce(5, "<описание>");
        }
        void State32()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.TO:
                    Shift();
                    break;
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<операнд>":
                                GoToState(33);
                                break;
                        }
                        break;
                    }
                case TokenType.LITERAL:
                    GoToState(28);
                    break;
                case TokenType.IDENTIFIER:
                    GoToState(29);
                    break;
                default:
                    throw new Exception($"Ожидалось <операнд>, ключевое слово TO, идентификатор или литерал, а встретилось {IsSpecialWord(TokenStack.Peek())}");
            }


        }
        void State33()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<операнд>":
                                Shift();
                                break;
                        }
                        break;
                    }
                case TokenType.ENDLINE:
                    GoToState(34);
                    break;
                default:
                    throw new Exception($"Ожидалось <операнд> или конец строки, а встретилось {IsSpecialWord(currentLexeme)}");
            }

        }
        void State34()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.ENDLINE:
                    Shift();
                    break;
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<список операторов>":
                                GoToState(35);
                                break;
                            case "<операция>":
                                GoToState(6);
                                break;
                            case "<цикл>":
                                GoToState(7);
                                break;
                            case "<выражение>":
                                GoToState(8);
                                break;
                        }
                        break;
                    }
                case TokenType.FOR:
                    GoToState(9);
                    break;
                case TokenType.IDENTIFIER:
                    GoToState(10);
                    break;
                default:
                    throw new Exception($"Ожидалось <список операторов>,<операция>,<цикл>,<выражение>,конец строки, идентификатор или ключевое слово FOR, а встретилось {IsSpecialWord(TokenStack.Peek())}");
            }



        }
        void State35()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.NETERM:
                    {
                        switch (TokenStack.Peek().Value)
                        {
                            case "<список операторов>":
                                Shift();
                                break;
                        }
                        break;
                    }
                case TokenType.NEXT:
                    GoToState(36);
                    break;
                default:
                    throw new Exception($"Ожидалось <список операторов> или ключевое слово NEXT, а встретилось {IsSpecialWord(currentLexeme)}");
            }


        }
        void State36()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.NEXT:
                    Shift();
                    break;
                case TokenType.FOR:
                    GoToState(37);
                    break;
                default:
                    throw new Exception($"Ожидались ключевые слова NEXT или FOR, а встретилось {IsSpecialWord(currentLexeme)}");
            }
        }
        void State37()
        {
            switch (TokenStack.Peek().Type)
            {
                case TokenType.FOR:
                    Shift();
                    break;
                case TokenType.ENDLINE:
                    GoToState(38);
                    break;
                default:
                    throw new Exception($"Ожидалось ключевое слово FOR или конец строки, а встретилось {IsSpecialWord(currentLexeme)}");
            }
        }
        void State38()
        {
            Reduce(11, "<цикл>");
        }
    }
}
