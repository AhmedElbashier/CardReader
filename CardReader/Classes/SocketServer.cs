using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using CardReader;
using System.Windows.Forms;

namespace CardReader.Classes
{
    class SocketServer
    {
        private static int port = 5050;
        private static string Ip = "192.168.1.76";
      //  private static HttpListenerResponse response;
        public static TcpListener listener;
        public static Form1 form;


        public static void HttpServer()
        {
            form.GetAllBatchesInstallmentsDates();
            IPAddress localAdd = IPAddress.Parse(Ip);
            listener = new TcpListener(localAdd, port);
            listener.Start();
            while (true)
            {

                TcpClient client = listener.AcceptTcpClient();

                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                string trimmedData = dataReceived.Substring(25);
              
                string trimmedData2 = trimmedData.Replace("&mjihao=1&cjihao=HW253824&status=11&time","");
                
                string ReceivedCardId = trimmedData2.Substring(0, 10);
                //MessageBox.Show(ReceivedCardId);
                form.GetstudentInfo(ReceivedCardId);
                //MessageBox.Show(ReceivedCardId);
                string stdYear=form.Year;
                form.CheckStudentYear();
                


                //      var data = JToken.Parse(ReceivedCardId).ToObject<CardInfo>();

                //TODO add the support for the info 

                // int status = form.CheckStudentStatus(form.ID);
                byte[] msg = Encoding.ASCII.GetBytes("{\"data\":[{\"cardid\":\"" + ReceivedCardId + "\",\"cjihao\":0,\"mjihao\":1,\"status\":" + 1 +",\"time\":\"0928162352\",\"output\":2}],\"code\":0,\"message\":\"");

                nwStream.Write(msg, 0, msg.Length);

                client.Close();
            }
            //listener.Stop();
        }

        private class StudantInfo
        {
            public string Name { get; set; }
            public string CollegeNumber { get; set; }
            public string Semster { get; set; }
            public string StdClass { get; set; }
            public string Program { get; set; }
        }
    }
}
