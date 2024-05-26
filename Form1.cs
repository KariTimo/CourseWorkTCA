using System.Windows.Forms;
using static part_1.Form1;

namespace part_1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }
        string buffer = "";
        char condition = ' ';
        string separator = ",+=*()-/";
        List<Token> tokens = new List<Token>();


        public List<Token> GetList()
        {
            return tokens;
        }
        Token token;
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            TxbCheck.Text = "";
            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                TxbInputData.Text = File.ReadAllText(OFD.FileName);
            }
        }

        public string RefactoringCode(string str)
        {
            while (str.Contains("  ")) { str = str.Replace("  ", " "); }
            while (str.Contains(" \r\n")) { str = str.Replace(" \r\n", "\r\n"); }
            return str;
        }

        public bool IsSpace(char ch)
        {
            if (ch == ' ')
                return true;
            return false;
        }

        string symbols = "!@#$%^&№;:?_";
        private void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                TxbCheck.Text = "";
                tokens.Clear();
                string data = TxbInputData.Text.ToUpper();
                data = RefactoringCode(data) + Environment.NewLine;
                TxbInputData.Text = data;
                if (data == "" || data == " ")
                    throw new Exception($"Поле ввода не может быть пустым");
                else
                {
                    for (int i = 0; i < data.Length;)
                    {
                        if (Char.IsLetter(data[i]))
                        {
                            buffer += data[i];
                            i++;
                            if (Char.IsLetter(data[i]))
                            {
                                while (Char.IsLetter(data[i]))
                                {
                                    buffer += data[i];
                                    i++;
                                }
                            }
                            else if (Char.IsDigit(data[i]))
                            {
                                while (Char.IsDigit(data[i]))
                                {
                                    buffer += data[i];
                                    i++;
                                }
                                i -= 1;
                            }
                            else
                                i -= 1;
                            Check(buffer);
                            buffer = "";
                        }
                        else if (Char.IsDigit(data[i]))
                        {
                            buffer += data[i];
                            i++;
                            if (Char.IsDigit(data[i]))
                            {
                                while (Char.IsDigit(data[i]))
                                {
                                    buffer += data[i];
                                    i++;
                                }
                                i -= 1;
                            }
                            else if (Char.IsLetter(data[i]))
                            {
                                buffer = "";
                                MessageBox.Show("Идентификатор не может начинаться с цифры");
                                return;
                            }
                            else
                                i -= 1;
                            Check(buffer);
                            buffer = "";
                        }
                        else if (separator.IndexOf(data[i]) >= 0)
                        {
                            buffer += data[i];
                            Check(buffer);
                            buffer = "";
                        }
                        else if (symbols.IndexOf(data[i]) >= 0)
                        {
                            buffer += data[i];
                            Check(buffer);
                            buffer = "";
                        }
                        else if (data[i] == '\n')
                        {
                            buffer += data[i];
                            Check(buffer);
                            buffer = "";
                        }
                        i++;
                    }
                }
                for (int i = 0; i < tokens.Count; i++)
                {
                    TxbCheck.Text += $"{tokens[i]} ";
                }
                btnAnalyzer.Enabled = true;
            }
            catch (Exception ex)
            {
                buffer = "";
                MessageBox.Show(ex.Message);
            }

        }


        static Dictionary<string, TokenType>
            SpecialWords = new Dictionary<string, TokenType>()
        {
            { "FOR", TokenType.FOR },
            { "NEXT", TokenType.NEXT },
            { "AS", TokenType.AS },
            { "DIM", TokenType.DIM },
            { "INTENGER", TokenType.INTENGER},
            { "LONG", TokenType.LONG},
            { "STRING", TokenType.STRING},
            { "BOOLEAN", TokenType.BOOLEAN},
            { "TO", TokenType.TO},
            { "\n", TokenType.ENDLINE},
            {"NETERM", TokenType.NETERM}
        };

        static Dictionary<char, TokenType>
            SpecialSymbols = new Dictionary<char, TokenType>()
        {
            { '+', TokenType.PLUS },
            { '-', TokenType.MINUS },
            { '*', TokenType.MULL },
            { '/', TokenType.DIV },
            { '=', TokenType.EQUAL },
            { ',', TokenType.COMMA },
            { '(', TokenType.OPENPAR },
            { ')', TokenType.CLOSPAR }
        };

        public static bool IsSpecialSymbol(char ch)
        {
            return SpecialSymbols.ContainsKey(ch);
        }

        public static bool IsSpecialWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }
            return (SpecialWords.ContainsKey(word));
        }

        public static bool IsContainSymbol(Token token)
        {
            if (token.Value.Contains("!") || token.Value.Contains("@") || token.Value.Contains("#") || token.Value.Contains("$") || token.Value.Contains("%") ||
                token.Value.Contains("^") || token.Value.Contains("&") || token.Value.Contains("!") || token.Value.Contains("№") || token.Value.Contains("_") ||
                token.Value.Contains(";") || token.Value.Contains(":") || token.Value.Contains("?"))
                return true;
            return false;
        }

        public void Check(string buf)
        {
            char first = Convert.ToChar(buf.Substring(0, 1));
            if (IsSpecialSymbol(first))
            {
                token = new Token(SpecialSymbols[first]);
                token.Value = buf;
            }
            else if (IsSpecialWord(buf))
            {
                token = new Token(SpecialWords[buf]);
            }
            else if (Char.IsDigit(first))
            {
                token = new Token(TokenType.LITERAL);
                token.Value = buf;
            }
            else
            {
                token = new Token(TokenType.IDENTIFIER);
                token.Value = buf;
                if (IsContainSymbol(token) == true)
                    throw new Exception($"Идентификатор не может содержать специальные символы");
                if (token.Value.Length >= 8)
                    throw new Exception($"Идентификатор не может быть больше 8 символов");
            }

            tokens.Add(token);

        }

        //Analyzer analyzer;
        UpperAnalyze analyzerUp;
        private void btnAnalyzer_Click(object sender, EventArgs e)
        {
            TxbMatrix.Text = " ";
            TxbPolish.Text = " ";
            try
            {
                if (TxbInputData.Text == "" || TxbInputData.Text == " ")
                    throw new Exception($"Поле ввода не может быть пустным");
                else
                {
                    analyzerUp = new UpperAnalyze(tokens);
                    analyzerUp.Start();
                    MessageBox.Show("Успешно просканировано");
                    TxbPolish.Text += analyzerUp.polish;
                    TxbMatrix.Text += analyzerUp.matrix;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void TxbInputData_TextChanged(object sender, EventArgs e)
        {
            btnAnalyzer.Enabled = false;
        }

        private void BtnOpen_MouseEnter_1(object sender, EventArgs e)
        {
            BtnOpen.BackColor = Color.CornflowerBlue;
        }

        private void BtnOpen_MouseLeave(object sender, EventArgs e)
        {
            BtnOpen.BackColor = BackColor;
        }

        private void BtnStart_MouseEnter(object sender, EventArgs e)
        {
            BtnStart.BackColor = Color.CornflowerBlue;
        }

        private void BtnStart_MouseLeave(object sender, EventArgs e)
        {
            BtnStart.BackColor = BackColor;
        }

        private void btnAnalyzer_MouseEnter(object sender, EventArgs e)
        {
            btnAnalyzer.BackColor = Color.CornflowerBlue;
        }

        private void btnAnalyzer_MouseLeave(object sender, EventArgs e)
        {
            btnAnalyzer.BackColor = BackColor;
        }
    }

    public enum TokenType
    {
        LITERAL, IDENTIFIER, FOR, PLUS, MINUS, NEXT, AS, COMMA, MULL, DIV, EQUAL, DIM, INTENGER, ENDLINE, LONG, STRING, BOOLEAN, TO, OPENPAR,CLOSPAR,NETERM
    }

   public class Token
    {
        public TokenType Type;
        public string Value;

        public Token(TokenType type)
        {
            Type = type;
        }
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
        public override string ToString()
        {
            return string.Format("{0} - {1}", Type, Value + Environment.NewLine);
        }


       

    }
}
