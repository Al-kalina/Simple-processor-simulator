using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zadanie_1
{
    public partial class Form1 : Form
    {
        private string AH = "00000000";
        private string AL = "00000000";
        private string BH = "00000000";
        private string BL = "00000000";
        private string CH = "00000000";
        private string CL = "00000000";
        private string DH = "00000000";
        private string DL = "00000000";
        private string inH = "00000000";
        private string inL = "00000000";
        private string iterator = ">";
        private int lineIndex = 0;
        private List<string> operations = new List<string>();
        OpenFileDialog openFile = new OpenFileDialog();
        private List<string> stack = new List<string>();
        private int SP;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            button2.Visible = false;
        }

        private void MOV(string regName, string valueH, string valueL)
        {
            if (regName == "A")
            {
                this.AH = valueH;
                this.AL = valueL;
            }
            else if (regName == "B")
            {
                this.BH = valueH;
                this.BL = valueL;
            }
            else if (regName == "C")
            {
                this.CH = valueH;
                this.CL = valueL;
            }
            else if (regName == "D")
            {
                this.DH = valueH;
                this.DL = valueL;
            }
        }

        private void SUB(string regName, string value)
        {
            string helpRegister = "";
            if (regName == "A")
            {
                helpRegister = AH + AL;
                helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) - Convert.ToInt32(value, 2)), 2);
                helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                AH = helpRegister.Substring(0, 8);
                AL = helpRegister.Substring(8, 8);
            }
            else if (regName == "B")
            {
                helpRegister = BH + BL;
                helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) - Convert.ToInt32(value, 2)), 2);
                helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                BH = helpRegister.Substring(0, 8);
                BL = helpRegister.Substring(8, 8);
            }
            else if (regName == "C")
            {
                helpRegister = CH + CL;
                helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) - Convert.ToInt32(value, 2)), 2);
                helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                CH = helpRegister.Substring(0, 8);
                CL = helpRegister.Substring(8, 8);
            }
            else if (regName == "D")
            {
                helpRegister = DH + DL;
                helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) - Convert.ToInt32(value, 2)), 2);
                helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                DH = helpRegister.Substring(0, 8);
                DL = helpRegister.Substring(8, 8);
            }
        }

        private void ADD(string regName, string value)
        {
            string helpRegister = "";
            if (regName == "A")
            {
                helpRegister = AH + AL;
                helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) + Convert.ToInt32(value, 2)), 2);
                helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                AH = helpRegister.Substring(0, 8);
                AL = helpRegister.Substring(8, 8);
            }
            else if (regName == "B")
            {
                helpRegister = BH + BL;
                helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) + Convert.ToInt32(value, 2)), 2);
                helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                BH = helpRegister.Substring(0, 8);
                BL = helpRegister.Substring(8, 8);
            }
            else if (regName == "C")
            {
                helpRegister = CH + CL;
                helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) + Convert.ToInt32(value, 2)), 2);
                helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                CH = helpRegister.Substring(0, 8);
                CL = helpRegister.Substring(8, 8);
            }
            else if (regName == "D")
            {
                helpRegister = DH + DL;
                helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) + Convert.ToInt32(value, 2)), 2);
                helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                DH = helpRegister.Substring(0, 8);
                DL = helpRegister.Substring(8, 8);
            }
        }

        private void SHR(string regName)
        {
            //przesunięcie bitowe w prawo o stałą wartość = 1
            string helpRegister = "";
            if (regName == "A")
            {
                helpRegister = AH + AL;
                helpRegister = "0" + helpRegister;
                helpRegister = helpRegister.Remove(helpRegister.Length - 1);
                AH = helpRegister.Substring(0, 8);
                AL = helpRegister.Substring(8, 8);
            }
            else if (regName == "B")
            {
                helpRegister = BH + BL;
                helpRegister = "0" + helpRegister;
                helpRegister = helpRegister.Remove(helpRegister.Length - 1);
                BH = helpRegister.Substring(0, 8);
                BL = helpRegister.Substring(8, 8);
            }
            else if (regName == "C")
            {
                helpRegister = CH + CL;
                helpRegister = "0" + helpRegister;
                helpRegister = helpRegister.Remove(helpRegister.Length - 1);
                CH = helpRegister.Substring(0, 8);
                CL = helpRegister.Substring(8, 8);
            }
            else if (regName == "D")
            {
                helpRegister = DH + DL;
                helpRegister = "0" + helpRegister;
                helpRegister = helpRegister.Remove(helpRegister.Length - 1);
                DH = helpRegister.Substring(0, 8);
                DL = helpRegister.Substring(8, 8);
            }
        }

        private void DEC(string regName)
        {
            // decrementacja argumentu i--;
            string helpRegister = "";
            try
            {
                if (regName == "A")
                {
                    helpRegister = AH + AL;
                    helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) - 1), 2);
                    helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                    AH = helpRegister.Substring(0, 8);
                    AL = helpRegister.Substring(8, 8);
                }
                else if (regName == "B")
                {
                    helpRegister = BH + BL;
                    helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) - 1), 2);
                    helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                    BH = helpRegister.Substring(0, 8);
                    BL = helpRegister.Substring(8, 8);
                }
                else if (regName == "C")
                {
                    helpRegister = CH + CL;
                    helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) - 1), 2);
                    helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                    CH = helpRegister.Substring(0, 8);
                    CL = helpRegister.Substring(8, 8);
                }
                else if (regName == "D")
                {
                    helpRegister = DH + DL;
                    helpRegister = Convert.ToString((Convert.ToInt32(helpRegister, 2) - 1), 2);
                    helpRegister = new string('0', 16 - helpRegister.Length) + helpRegister;
                    DH = helpRegister.Substring(0, 8);
                    DL = helpRegister.Substring(8, 8);
                }
            }
            catch
            {
                MessageBox.Show("Operation DEC error, register value less than 0");
            }
        }

        private void NEG(string regName)
        {
            //negacja liczby w rejestrze
            string helpRegister = "";
            if (regName == "A")
            {
                helpRegister = AH + AL;
                helpRegister = negationFunction(helpRegister);
                AH = helpRegister.Substring(0, 8);
                AL = helpRegister.Substring(8, 8);
            }
            else if (regName == "B")
            {
                helpRegister = BH + BL;
                helpRegister = negationFunction(helpRegister);
                BH = helpRegister.Substring(0, 8);
                BL = helpRegister.Substring(8, 8);
            }
            else if (regName == "C")
            {
                helpRegister = CH + CL;
                helpRegister = negationFunction(helpRegister);
                CH = helpRegister.Substring(0, 8);
                CL = helpRegister.Substring(8, 8);
            }
            else if (regName == "D")
            {
                helpRegister = DH + DL;
                helpRegister = negationFunction(helpRegister);
                DH = helpRegister.Substring(0, 8);
                DL = helpRegister.Substring(8, 8);
            }
        }

        private string negationFunction(string value)
        {
            StringBuilder sb = new StringBuilder(value);
            for (int i = 0; i < value.Length; i++)
            {
                if (sb[i] == '0')
                {
                    sb[i] = '1';
                }
                else
                {
                    sb[i] = '0';
                }
            }
            return sb.ToString();
        }

        private void CMP(string regName, string value)
        {
            //porównanie
            string helpRegister = "";
            if (regName == "A")
            {
                helpRegister = AH + AL;
            }
            else if (regName == "B")
            {
                helpRegister = BH + BL;
            }
            else if (regName == "C")
            {
                helpRegister = CH + CL;
            }
            else if (regName == "D")
            {
                helpRegister = DH + DL;
            }
            cmd.Text = "Compare result:";
            if (helpRegister == value)
            {
                cmd.Text += " True";
            }
            else
            {
                cmd.Text += " False";
            }
        }

        private void INT16h()
        {
            cmd.Text = "Enter your text here";
            button2.Visible = true;
        }
        private void INT21h()
        {
            string text = cmd.Text;
            cmd.Text = "Your text in ASCII: ";
            cmd.Text += "\n";
            foreach (char c in text)
            {
                cmd.Text += (Convert.ToInt32(c).ToString() + " ");
            }
        }

        private void INT1A()
        {
            //pobranie aktualnego czasu
            DateTime dateTime = DateTime.Now;
            cmd.Text = "Current date time:" + "\n" + dateTime.ToString();
        }

        private void INT13h()
        {
            //zapisanie tekstu na panelu
            cmd.Text = "INT13h interrupt";
        }

        private void OR(string regName, string value)
        {
            // operacja or
            string helpRegister = "";
            if (regName == "A")
            {
                helpRegister = AH + AL;
            }
            else if (regName == "B")
            {
                helpRegister = BH + BL;
            }
            else if (regName == "C")
            {
                helpRegister = CH + CL;
            }
            else if (regName == "D")
            {
                helpRegister = DH + DL;
            }
            StringBuilder sb = new StringBuilder(helpRegister);
            for (int i = 0; i < value.Length; i++)
            {
                if (sb[i] == '0' && value[i] == '1')
                {
                    sb[i] = '1';
                }
                else if (sb[i] == '1' && value[i] == '0')
                {
                    sb[i] = '1';
                }
                else if (sb[i] == '1' && value[i] == '1')
                {
                    sb[i] = '1';
                }
                else
                {
                    sb[i] = '0';
                }
            }
            cmd.Text = "Or result:" + "\n" + sb.ToString();
        }

        private void PUSH(string regName)
        {
            string helpRegister = "";
            if (regName == "A")
            {
                helpRegister = AH + AL;
            }
            else if (regName == "B")
            {
                helpRegister = BH + BL;
            }
            else if (regName == "C")
            {
                helpRegister = CH + CL;
            }
            else if (regName == "D")
            {
                helpRegister = DH + DL;
            }
            stack.Insert(0, helpRegister);
            SP = stack.Count;
        }

        private void POP()
        {
            try
            {
                cmd.Text = stack.ElementAt(0);
                stackValue.Text = stack.ElementAt(0);
                stack.RemoveAt(0);
                SP = stack.Count;
                stackText.Text = SP.ToString();
            }
            catch
            {
                cmd.Text = "Stack is empty!!!";
            }
        }

        private void saveOperation_Click(object sender, EventArgs e)
        {
            this.label4.Text = "";
            string selectedOperion = (string)operation.SelectedItem;
            if (selectedOperion == "MOV" || selectedOperion == "ADD" || selectedOperion == "SUB" || selectedOperion == "OR" || selectedOperion == "CMP")
            {
                if ((string)modeSelect.SelectedItem == "tryb natychmiastowy")
                {
                    operations.Add((operation.SelectedItem.ToString() + " " + registerSelect.SelectedItem.ToString() + " " + inH + inL).ToString());
                }
                else
                {
                    operations.Add((operation.SelectedItem.ToString() + " " + registerSelect.SelectedItem.ToString() + " " + registerValue()).ToString());
                }
            }
            else if (selectedOperion == "PUSH" || selectedOperion == "NEG" || selectedOperion == "DEC" || selectedOperion == "SHR")
            {
                operations.Add(operation.SelectedItem.ToString() + " " + registerSelect.SelectedItem.ToString());
            }
            else
            {
                operations.Add(operation.SelectedItem.ToString());
            }
            foreach (string s in operations)
            {
                this.label4.Text += s;
                this.label4.Text += "\n";
            }
        }

        private string registerValue()
        {
            if ((string)operationValue.SelectedItem == "A")
            {
                return AH + AL;
            }
            else if ((string)operationValue.SelectedItem == "B")
            {
                return BH + BL;
            }
            else if ((string)operationValue.SelectedItem == "C")
            {
                return CH + CL;
            }
            else
            {
                return DH + DL;
            }
        }

        private void executeProgram_Click(object sender, EventArgs e)
        {
            string[] verse;
            foreach (string line in operations)
            {
                verse = line.Split(' ');
                if (verse[0] == "MOV")
                {
                    MOV(verse[1], verse[2].Substring(0, 8), verse[2].Substring(8, 8));
                }
                else if (verse[0] == "SUB")
                {
                    SUB(verse[1], verse[2]);
                }
                else if (verse[0] == "ADD")
                {
                    ADD(verse[1], verse[2]);
                }
                ///nowe funkcje
                else if (verse[0] == "OR")
                {
                    OR(verse[1], verse[2]);
                }
                else if (verse[0] == "CMP")
                {
                    CMP(verse[1], verse[2]);
                }
                //...
                else if (verse[0] == "PUSH")
                {
                    PUSH(verse[1]);
                }
                else if (verse[0] == "NEG")
                {
                    NEG(verse[1]);
                }
                else if (verse[0] == "DEC")
                {
                    DEC(verse[1]);
                }
                else if (verse[0] == "SHR")
                {
                    SHR(verse[1]);
                }
                //...
                else if (verse[0] == "POP")
                {
                    POP();
                }
                else if (verse[0] == "INT13h")
                {
                    INT13h();
                }
                else if (verse[0] == "INT16h")
                {
                    INT16h();
                }
                else if (verse[0] == "INT1A")
                {
                    INT1A();
                }
            }
        }

        private void executeStep_Click(object sender, EventArgs e)
        {
            if (lineIndex < operations.Count)
            {
                if (lineIndex > 0)
                {
                    iterator = "\n" + iterator;
                    this.label9.Text = iterator;
                }
                string[] verse = operations.ElementAt(lineIndex).Split(' ');
                if (verse[0] == "MOV")
                {
                    MOV(verse[1], verse[2].Substring(0, 8), verse[2].Substring(8, 8));
                }
                else if (verse[0] == "SUB")
                {
                    SUB(verse[1], verse[2]);
                }
                else if (verse[0] == "ADD")
                {
                    ADD(verse[1], verse[2]);
                }
                //nowe funkcje
                else if (verse[0] == "OR")
                {
                    OR(verse[1], verse[2]);
                }
                else if (verse[0] == "CMP")
                {
                    CMP(verse[1], verse[2]);
                }
                //...
                else if (verse[0] == "PUSH")
                {
                    PUSH(verse[1]);
                }
                else if (verse[0] == "NEG")
                {
                    NEG(verse[1]);
                }
                else if (verse[0] == "DEC")
                {
                    DEC(verse[1]);
                }
                else if (verse[0] == "SHR")
                {
                    SHR(verse[1]);
                }
                //...
                else if (verse[0] == "POP")
                {
                    POP();
                }
                else if (verse[0] == "INT13h")
                {
                    INT13h();
                }
                else if (verse[0] == "INT16h")
                {
                    INT16h();
                }
                else if (verse[0] == "INT1A")
                {
                    INT1A();
                }
                lineIndex++;
            }
        }

        private void buttonInputA_Click(object sender, EventArgs e)
        {
            this.AH = fiilGap(AH_.Text);
            this.AL = fiilGap(AL_.Text);
            this.labelA.Text = AH + AL;
        }

        private void buttonInputB_Click(object sender, EventArgs e)
        {
            this.BH = fiilGap(BH_.Text);
            this.BL = fiilGap(BL_.Text);
            this.labelB.Text = BH + BL;
        }

        private void buttonInputC_Click(object sender, EventArgs e)
        {
            this.CH = fiilGap(CH_.Text);
            this.CL = fiilGap(CL_.Text);
            this.labelC.Text = CH + CL;
        }

        private void buttonInputD_Click(object sender, EventArgs e)
        {
            this.DH = fiilGap(DH_.Text);
            this.DL = fiilGap(DL_.Text);
            this.labelD.Text = DH + DL;
        }

        private void buttonInputIN_Click(object sender, EventArgs e)
        {
            this.inH = fiilGap(inH_.Text);
            this.inL = fiilGap(inL_.Text);
            this.labelIN.Text = inH + inL;
        }

        private string fiilGap(string value)
        {
            if (value.Length <= 8)
            {
                foreach (char c in value)
                {
                    if (!(c == '0' || c == '1'))
                    {
                        MessageBox.Show("The number is incorrect. Please enter 0 or 1");
                        return "00000000";
                    }
                }
                return new string('0', 8 - value.Length) + value;
            }
            else
            {
                MessageBox.Show("Incorrect length of a numeric value");
                return "00000000";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.registerA.Text = AH + AL;
            this.registerB.Text = BH + BL;
            this.registerC.Text = CH + CL;
            this.registerD.Text = DH + DL;
            this.registerIn.Text = inH + inL;
        }

        private void clearProgram_Click(object sender, EventArgs e)
        {
            clearFunctionFromProgram();
        }

        private void loadProgram_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFile.FileName;
                clearFunctionFromProgram();
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        // Pętla odczytująca i wyświetlająca zawartość pliku
                        // tak długo, aż osiągniemy jego koniec.
                        while ((line = sr.ReadLine()) != null)
                        {
                            operations.Add(line);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("The file could not be read");
                }
                foreach (string s in operations)
                {
                    this.label4.Text += s;
                    this.label4.Text += "\n";
                }
            }
        }

        private void clearFunctionFromProgram()
        {
            lineIndex = 0;
            iterator = ">";
            this.label9.Text = iterator;
            this.label4.Text = "";
            operations.Clear();
        }

        private void saveProgram_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = @"C:\";
            saveFile.RestoreDirectory = true;
            saveFile.FileName = "*.txt";
            saveFile.DefaultExt = "txt";
            saveFile.Filter = "txt files (*.txt)|*.txt";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                Stream fileStream = saveFile.OpenFile();
                StreamWriter sw = new StreamWriter(fileStream);
                foreach (string s in operations)
                {
                    sw.WriteLine(s);
                }
                sw.Close();
                fileStream.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            INT21h();
            button2.Visible = false;
        }
    }
}

