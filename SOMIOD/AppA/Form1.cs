using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using RestSharp;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace AppA
{

  

    public partial class Form1 : Form
    {

        private MqttClient mcClient = null;

        private RestClient restClient = null;
        private string baseURI = @"http://localhost:50998/";
        private string ImageBluePath = "Images\\Blue.jpg";
        private string ImageRedPath = "Images\\Red.png";

        public string ImageBLuePath { get; private set; }

        public Form1()
        {
            InitializeComponent();
            restClient = new RestClient(baseURI);
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlElement application = doc.CreateElement("application");
                application.SetAttribute("name", "appA");
                XmlElement container = doc.CreateElement("container");
                container.SetAttribute("name", "color");
                XmlElement subscription = doc.CreateElement("subscription");
                subscription.SetAttribute("name", "sub1");
                XmlElement events = doc.CreateElement("event");
                events.InnerText = "1";
                XmlElement endpoint = doc.CreateElement("endpoint");
                endpoint.InnerText = "127.0.0.1";

                subscription.AppendChild(events);
                subscription.AppendChild(endpoint);
                container.AppendChild(subscription);
                application.AppendChild(container);
                doc.AppendChild(application);


                string xmlContent = doc.OuterXml;

                string appName = application.GetAttribute("name");
                string containerName = container.GetAttribute("name");
                string subscriptionName = subscription.GetAttribute("name");

                var requestPostA = new RestRequest("api/somiod/", Method.Post);

                requestPostA.AddHeader("Content-Type", "application/xml");
                requestPostA.AddParameter("application/xml", xmlContent, ParameterType.RequestBody);
                restClient.Execute(requestPostA);
                         
                var requestPostS = new RestRequest($"api/somiod/{appName}/{containerName}/subscriptions", Method.Post);

                requestPostS.AddHeader("Content-Type", "application/xml");
                requestPostS.AddParameter("application/xml", xmlContent, ParameterType.RequestBody);
                restClient.Execute(requestPostS);

                mcClient = new MqttClient("127.0.0.1");
                string[] mStrTopicsInfo = { "data" };
                mcClient.Connect(Guid.NewGuid().ToString());
                if (!mcClient.IsConnected)
                {
                    Console.WriteLine("Error connecting to message broker...");
                    return;
                }

                mcClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                //Subscribe to topics
                byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE};
                mcClient.Subscribe(mStrTopicsInfo, qosLevels);
                    
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string receivedData = Encoding.UTF8.GetString(e.Message);
            if (receivedData == "Blue")
            {
                pictureBox1.Image = Image.FromFile(ImageBluePath);
                UpdateStatusLabel("Status: Blue");
            }
            if(receivedData == "Red")
            {
                pictureBox1.Image = Image.FromFile(ImageRedPath);
                UpdateStatusLabel("Status: Red");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }

        private void UpdateStatusLabel(string newText)
        {
            if (labelStatus.InvokeRequired)
            {
                labelStatus.Invoke(new Action(() => labelStatus.Text = newText));
            }
            else
            {
                labelStatus.Text = newText;
            }
        }
    }
}
