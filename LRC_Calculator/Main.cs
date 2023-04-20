using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LRC_Calculator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 바이트 배열에 대한 LRC값을 구함. 모든 바이트 배열의 값을 더한후 하위 1바이트 값을 가지고
        /// 비트 반전 한 값에 +1 을 한뒤 리턴
        /// </summary>
        /// <param name="tmp"></param>
        /// <returns></returns>
        private byte Get_LRC(byte[] tmp)
        {

            int i_tmp = 0;
            foreach(byte x in tmp)
            {
                i_tmp += x;
            }
            i_tmp %= 256;
            i_tmp = ~i_tmp + 1;
            return BitConverter.GetBytes(i_tmp)[0];
        }

        private byte[] string_to_byte(string tmp)
        {
            if (tmp.Length % 2 == 1)
            {
                return null;
            }
            byte[] bytes = new byte[tmp.Length / 2];
            for (int i = 0; i < tmp.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(tmp[i].ToString() + tmp[i + 1].ToString(), 16);
            }
            return bytes;
        }

        private void InputtextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (InputtextBox.Text == "")
                {
                    return;
                }
                byte rbyte = 0;
                if (IASCIIradioButton.Checked)
                {
                    string tmp = InputtextBox.Text.Replace("\r", "").Replace("\n", "").Replace(":", "");
                    rbyte = Get_LRC(string_to_byte(tmp));

                    //MessageBox.Show(Get_LRC(Encoding.ASCII.GetBytes(tmp)).ToString());
                }
                else if (IHexradioButton.Checked)
                {
                    string tmp = InputtextBox.Text.Replace("0x", "").Replace(" ", "");
                    rbyte = Get_LRC(string_to_byte(tmp));
                    //Get_LRC(bytes).ToString();정답
                }

                if (OASCIIradioButton.Checked)
                {
                    OutputtextBox.Text = Convert.ToChar(rbyte).ToString();
                    if (rbyte == 0)
                    {
                        OutputtextBox.Text = "NULL 0x00";
                    }
                }
                else if (OHexradioButton.Checked)
                {
                    OutputtextBox.Text = BitConverter.ToString(new byte[] { rbyte });
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void OHexradioButton_CheckedChanged(object sender, EventArgs e)
        {
            string tmp = InputtextBox.Text;
            InputtextBox.Text = "";
            InputtextBox.Text = tmp;
        }

        private void OASCIIradioButton_CheckedChanged(object sender, EventArgs e)
        {
            string tmp = InputtextBox.Text;
            InputtextBox.Text = "";
            InputtextBox.Text = tmp;
        }
    }
}
