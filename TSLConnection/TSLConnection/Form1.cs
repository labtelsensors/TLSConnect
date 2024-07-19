using System;
using System.Net.Sockets;
using static System.Console;

namespace TSLConnection
{
    public partial class Form1 : Form
    {
        public const int TcpPort = 51971;
        Byte[] data = System.Text.Encoding.ASCII.GetBytes("*IDN?");

        public Form1()
        {
            InitializeComponent();

            // *** CHANGE TO THE DESIRED IP ADDRESS ***
            string instrumentIpAddress = "192.168.86.70";

            // Create a TCP client for communicating with the Hyperion instrument
            TcpClient tcpClient = new TcpClient();

            // Connect to the instrument over TCP/IP
            tcpClient.Connect(instrumentIpAddress, TcpPort);
            NetworkStream tcpNetworkStream = tcpClient.GetStream();

            // Execute a simple command to retrieve the instrument serial number
            tcpNetworkStream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", "*IDN?");

            // Receive the server response.

            // Buffer to store the response bytes.
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = tcpNetworkStream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);

            // Explicit close is not necessary since TcpClient.Dispose() will be
            // called automatically.
            tcpNetworkStream.Close();
            // client.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}