using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace SteganoGraphyLab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Status = new Thread(UpdateConnectionStatus);
            Status.Start();
        }
        private TcpClient tcpClient = new();
        private Thread Status;
        private Queue<bool> WaitingBits = new();
        private Queue<bool> SentBits = new();
        private HTTP2Response currentResponse;
        private IPv4Response ipv4Response;

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(!tcpClient.Connected)
                    Connect();

                NetworkStream netStream = tcpClient.GetStream();
                var sendingString = "";

                switch (messageType.Items.IndexOf(messageType.SelectedItem))
                {
                    case 0:
                        var hiddenBit = WaitingBits.Dequeue();
                        SentBits.Enqueue(hiddenBit);
                        currentResponse = new HTTP2Response(hiddenBit);
                        sendingString = currentResponse.ToString(false);
                        break;
                    case 1:
                        string hiddenBits = "";
                        for (int i = 0; i < 16; i++)
                        {
                            var bit = WaitingBits.Dequeue();
                            SentBits.Enqueue(bit);
                            hiddenBits += (bit ? "1" : "0");
                        }
                        ipv4Response = new IPv4Response(hiddenBits,ipAdress.Text);
                        sendingString = ipv4Response.ToString(false);
                        break;
                    default:
                        break;
                }
                
                var sendingBytes = Encoding.UTF8.GetBytes(sendingString);
                netStream.WriteByte((byte)messageType.Items.IndexOf(messageType.SelectedItem));
                netStream.Write(sendingBytes,0,sendingBytes.Length);
                netStream.Flush();
                UpdateMessageBoxes();
                UpdateHiddenBits();
                tcpClient.Dispose();
                tcpClient.Close();
            }
            catch
            {
                MessageBox.Show("Неудалось отправить сообщение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Connect()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(ipAdress.Text, Convert.ToInt32(port.Text));

            if (tcpClient.Connected && hiddenMessage.ReadOnly)
            {
                sendButton.Enabled = true;
            }
        }
        private void ConnectButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                Connect();
            }
            catch
            {
                MessageBox.Show("Неудалось подключиться!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateConnectionStatus()
        {
            while (true)
            {
                Invoke(() =>
                {
                    connectionStatus.Text = tcpClient.Connected ?
                    $"Статус подключения:Подключён к получателю{ipAdress.Text}:{port.Text}" :
                    "Статус подключения:Не подключён к получателю";
                });
            }
        }

        private void MessageTextBox_TextChanged(object sender, EventArgs e){}
        private void messageTextBox_TextChanged_1(object sender, EventArgs e){}
        private void label5_Click(object sender, EventArgs e){}

        private void messageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMessageBoxes();
        }
        private void UpdateMessageBoxes()
        {
            Invoke(() =>
            {
                switch ((byte)messageType.Items.IndexOf(messageType.SelectedItem))
                {
                    case 0:
                        if(currentResponse == null)
                            currentResponse = new HTTP2Response(false);
                        messageTextBox.Text = currentResponse.ToString(true);
                        messageBinary.Text = currentResponse.ToString(false);
                        break;
                    case 1:
                        if (ipv4Response == null)
                        {
                            ipv4Response = new IPv4Response("0", ipAdress.Text);
                        }
                        messageTextBox.Text = ipv4Response.ToString(true);
                        messageBinary.Text = ipv4Response.ToString(false);
                        break;
                }
            });
        }

        private void hiddenMessage_TextChanged(object sender, EventArgs e)
        {
            var bitView = String.Join(String.Empty, Encoding.UTF8.GetBytes(hiddenMessage.Text)
        .Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
            hiddenMessageBit.Text = bitView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hiddenMessage.ReadOnly = true;
            if (tcpClient.Connected && hiddenMessage.ReadOnly)
            {
                sendButton.Enabled = true;
            }
            foreach(var c in hiddenMessageBit.Text)
            {
                if (!(c == '1' || c == '0'))
                    continue;
                WaitingBits.Enqueue(c == '1');
            }
            UpdateHiddenBits();
            
        }
        private void UpdateHiddenBits()
        {
            var w = "";
            var s = "";
            foreach (var b in WaitingBits)
            {
                w += b ? "1" : "0";
            }
            foreach (var b in SentBits)
            {
                s += b ? "1" : "0";
            }
            Invoke(() =>
            {
                waitingBits.Text = w;
                sentBits.Text = s;
            });
        }
    }

    public class HTTP2Response
    {
        //HEADER
        private int Length = 2148;
        private string binaryLength => Convert.ToString(Length, 2).PadLeft(24, '0');
        private bool Type = false; //0 - DATA 1-Headers
        private string binaryType => Type ? "00000001" : "00000000";
        //Flags
        private bool END_STREAM = false;//END_STREAM (0x1) PADDED (0x8)
        private string binaryEND_STREAM => END_STREAM ? "1" : "0";
        private bool PADDED = false; //Добавлять ли смещение 
        private string binaryPADDED => PADDED ? "1" : "0";

        private bool R = false; //0 при отправке , игнорируется при получении.
        private string binaryR => R ? "1" : "0";
        private int StreamIdentifier = 629353;//Идентификатор потока указан для примера.
        private string binaryStreamIdentifier => Convert.ToString(StreamIdentifier, 2).PadLeft(31, '0');
        //DATA
        private int PadLength = 153;
        private string binaryPadLength => Convert.ToString(PadLength, 2).PadLeft(8, '0');
        private long Data = 15314643224;
        private string binaryData => Convert.ToString(Data, 2).PadLeft(Length + (PADDED ? PadLength : 0), '0');

        public string ToString(bool readable)
        {
            if (readable)
            {
                return
                $"Length:{binaryLength}\n" +
                $"Type:{binaryType}\n" +
                $"Flags:{binaryEND_STREAM}00{binaryPADDED}0000\n" +
                $"  END_STREAM:{binaryEND_STREAM}\n" +
                $"  PADDED:{binaryPADDED}\n" +
                $"Stream:{binaryR}{binaryStreamIdentifier}\n" +
                $"  R:{binaryR}\n" +
                $"  StreamIdentifier:{binaryStreamIdentifier}\n" +
                $"PadLength:{binaryPadLength}\n" +
                $"DATA:{Convert.ToString(Data, 2)}";
            }
            else
            {
                return
                    $"{binaryLength} " +
                    $"{binaryType} " +
                    $"{binaryEND_STREAM}00{binaryPADDED}0000 " +
                    $"{binaryR}{binaryStreamIdentifier} " +
                    $"{binaryPadLength} " +
                    $"{binaryData}";
            }
        }
        public string GetHiddenBinary()
        {
            if (!PADDED)
            {
                return "1";
            }
            else
            {
                if(PADDED && PadLength == 0)
                {
                    return "0";
                }
            }
            return "No hidden binary";
        }
        public HTTP2Response(string binaryString)
        {
            var message = binaryString.Split(' ');
            Length = Convert.ToInt32(message[0]);
            Type = message[1] == "1";
            END_STREAM = message[2].Substring(0,1) == "1";
            PADDED = message[2].Substring(3,1) == "1";
            R = message[3].Substring(0,1) == "1";
            StreamIdentifier = Convert.ToInt32(message[3].Substring(1));
            PadLength = Convert.ToInt32(message[4]);
            Data = Convert.ToInt64(message[5]);
        }
        public HTTP2Response(bool AsHiddenOne)
        {
            if (AsHiddenOne)
            {
                PADDED = false;
            }
            else
            {
                PADDED = true;
                PadLength = 0;
            }
        }
    }

    public class IPv4Response
    {
        //HEADER
        private int Version = 4;        
        private string binaryVersion => Tools.ToBinaryString(Version,4);

        private int HeaderLength = 14;  
        private string binaryHeaderLength => Tools.ToBinaryString(HeaderLength, 14);

        private int TOS = 100;          
        private string binaryTOS => Tools.ToBinaryString(TOS, 8);

        private int TotalLength = 20000;
        private string binaryTotalLength => Tools.ToBinaryString(TotalLength, 16);

        private int Identification;
        private string binaryIdentification => Tools.ToBinaryString(Identification, 16);

        private int Flags = 3;
        private string binaryFlags => Tools.ToBinaryString(Flags, 3);

        private int FragmentOffset = 623;
        private string binaryFragmentOffset => Tools.ToBinaryString(FragmentOffset, 13);

        private int TTL = 50;
        private string binaryTTL => Tools.ToBinaryString(TTL, 8);

        private int Proto = 4;
        private string binaryProto => Tools.ToBinaryString(Proto, 8);

        private int HeaderChecksum = 51256;
        private string binaryHeaderChecksum => Tools.ToBinaryString(HeaderChecksum, 16);

        private uint SourceIPAddress;
        private string binarySourceIPAddress => Tools.ToBinaryString(SourceIPAddress, 32);

        private uint DestinationIPAddress;
        private string binaryDestinationIPAddress => Tools.ToBinaryString(DestinationIPAddress, 32);


        public string ToString(bool readable)
        {
            if (readable)
            {
                return
                $"Version:{binaryVersion}\n" +
                $"HeaderLength:{binaryHeaderLength}\n" +
                $"TOS:{binaryTOS}\n" +
                $"TotalLength:{binaryTotalLength}\n" +
                $"Identification:{binaryIdentification}\n" +
                $"Flags:{binaryFlags}\n" +
                $"FragmentOffset:{binaryFragmentOffset}\n" +
                $"TTL:{binaryTTL}\n" +
                $"Proto:{binaryProto}\n" +
                $"HeaderChecksum:{binaryHeaderChecksum}\n" +
                $"SourceIPAddress:{binarySourceIPAddress}\n" +
                $"DestinationIPAddress:{binaryDestinationIPAddress}\n";
            }
            else
            {
                return
                $"{binaryVersion} " +
                $"{binaryHeaderLength} " +
                $"{binaryTOS} " +
                $"{binaryTotalLength} " +
                $"{binaryIdentification} " +
                $"{binaryFlags} " +
                $"{binaryFragmentOffset} " +
                $"{binaryTTL} " +
                $"{binaryProto} " +
                $"{binaryHeaderChecksum} " +
                $"{binarySourceIPAddress} " +
                $"{binaryDestinationIPAddress}";
            }
        }
        public string GetHiddenBinary()
        {
            return binaryIdentification;
        }
        public IPv4Response(string binaryString)
        {
            var message = binaryString.Split(' ');
            Version = Convert.ToInt32(message[0]);
            HeaderLength = Convert.ToInt32(message[1]);
            TOS = Convert.ToInt32(message[2]);
            TotalLength = Convert.ToInt32(message[3]);
            Identification = Convert.ToInt32(message[4]);
            Flags = Convert.ToInt32(message[5]);
            FragmentOffset = Convert.ToInt32(message[6]);
            TTL = Convert.ToInt32(message[7]);
            Proto = Convert.ToInt32(message[8]);
            HeaderChecksum = Convert.ToInt32(message[9]);
            SourceIPAddress = Convert.ToUInt32(message[10]);
            DestinationIPAddress = Convert.ToUInt32(message[11]);
        }

        public IPv4Response(string hiddenMessage,string destIP)
        {
            if (destIP == "")
            {
                destIP = "127.0.0.1";
            }
            SourceIPAddress = BitConverter.ToUInt32(IPAddress.Parse(Tools.GetLocalIPAddress()).GetAddressBytes(), 0);
            DestinationIPAddress = BitConverter.ToUInt32(IPAddress.Parse(destIP).GetAddressBytes(), 0);
            Identification = Convert.ToInt32(hiddenMessage);
        }
        public void SetHiddenMessage(string hiddenMessage)
        {
            Identification = Convert.ToInt32(hiddenMessage);
        }

    }

    public static class Tools
    {
        public static string ToBinaryString(int val,int bins)
        {
            return Convert.ToString(val, 2).PadLeft(bins, '0');
        }
        public static string ToBinaryString(uint val, int bins)
        {
            return Convert.ToString(val, 2).PadLeft(bins, '0');
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}