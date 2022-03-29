using Lab2_BackEnd.Enums;
using Lab2_BackEnd.Factory;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2_FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileData sourceFile;
        private FileData resultData;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemOpen_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    var fileContent =  File.ReadAllBytes(openFileDialog.FileName);
                    this.sourceFile = new FileData(fileContent);
                    this.SorceBits_TB.Text = this.sourceFile.BytesAsBits;
                }
            }
            catch (Exception ex)
            {
                this.Console_TB.Text = ex.Message;
            }
        }

        private void MenuItemSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                    File.WriteAllBytes(saveFileDialog.FileName, resultData.Bytes.ToArray());
            }
            catch (Exception ex)
            {
                Console_TB.Text = ex.Message;
            }
        }


        private void KeyInitStateBits_TB_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (!(e.Key is Key.D1 || e.Key is Key.D0))
            {
                e.Handled = true;
            }
        }


        private void KeyInitStateBits_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regex = new Regex(@"^[0-1]$");
            if (!regex.IsMatch(KeyInitStateBits_TB.Text))
            {
                KeyInitStateBits_TB.Text = new string(KeyInitStateBits_TB.Text.Where(x => regex.IsMatch(x.ToString())).ToArray());
                e.Handled = true;
            }
            this.CounterOfStartStateCharacters_TB.Text = KeyInitStateBits_TB.Text.Length.ToString();
        }

        private void Process_Btn_Click(object sender, RoutedEventArgs e)
        {
            Console_TB.Text = string.Empty;
            try
            {
                var taskModel = new TaskModel(BitsToXor_TB.Text, MaxPower_TB.Text);
                var keyModel = new KeyModel(this.KeyInitStateBits_TB.Text, taskModel.MaxPower);
                var datamodel = new DataModel();
                var cipher = CipherFactory.ProduceCipher(Ciphers.LFSRCipher);
                cipher.RegisterShowKey(ShowKey);
                var key = new Lab2_BackEnd.Ciphers.StreamCipher.Key(keyModel.KeyStartState, taskModel.BitsToXor, taskModel.MaxPower);
                var result = cipher.Encrypt(key, this.sourceFile.Bytes);
                this.resultData = new FileData(result);
                ResultBits_TB.Text = resultData.BytesAsBits;
                
            }
            catch (Exception ex)
            {
                Console_TB.Text = ex.Message;
            }

        }
        
        private void ShowKey(byte[] bytesToShowAsBits)
        {
            this.KeyBits_TB.Text = GetBits(bytesToShowAsBits);
        }

        private string GetBits(byte[] bytes)
        {
            var resultBuilder = new StringBuilder();
            foreach (byte b in bytes)
            {
                resultBuilder.Append(ConverterExtentions.CovertByteToBits(b));
            }
            return resultBuilder.ToString();
        }

        private void MaxPower_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regex = new Regex(@"^\d$");
            if (!regex.IsMatch(MaxPower_TB.Text))
            {
                MaxPower_TB.Text = new string(MaxPower_TB.Text.Where(x => regex.IsMatch(x.ToString())).ToArray());
                e.Handled = true;
            }
        }

    }
}