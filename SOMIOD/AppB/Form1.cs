using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RestSharp;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using System.Xml;
using System.Data.SqlClient;

namespace AppB
{
    public partial class Form1 : Form
    {
        private MqttClient mcClient = null;

        private RestClient restClient = null;

        private string baseURI = @"http://localhost:50998";
        public Form1()
        {
            InitializeComponent();
            restClient = new RestClient(baseURI);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement application = doc.CreateElement("application");
            application.SetAttribute("name", "appB");
            XmlElement container = doc.CreateElement("container");
            container.SetAttribute("name", "switch");
            XmlElement data = doc.CreateElement("data");
            data.SetAttribute("name", "data1");
            XmlElement content = doc.CreateElement("content");
            content.InnerText = "Blue";
               
            data.AppendChild(content);         
            container.AppendChild(data);
            application.AppendChild(container);
            doc.AppendChild(application);


            string xmlContent = doc.OuterXml;

            var requestPostA = new RestRequest("api/somiod/", Method.Post);

            requestPostA.AddHeader("Content-Type", "application/xml");
            requestPostA.AddParameter("application/xml", xmlContent, ParameterType.RequestBody);
            restClient.Execute(requestPostA);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement application = doc.CreateElement("application");
            application.SetAttribute("name", "appA");
            XmlElement container = doc.CreateElement("container");
            container.SetAttribute("name", "color");
            XmlElement data = doc.CreateElement("data");
            data.SetAttribute("name", "data1");
            XmlElement content = doc.CreateElement("content");
            content.InnerText = "Blue";

            data.AppendChild(content);
            container.AppendChild(data);
            application.AppendChild(container);
            doc.AppendChild(application);

            string xmlContent = doc.OuterXml;

            string appName = application.GetAttribute("name");
            string containerName = container.GetAttribute("name");


            var requestPost = new RestRequest($"api/somiod/{appName}/{containerName}/data/", Method.Post);

            requestPost.AddHeader("Content-Type", "application/xml");
            requestPost.AddParameter("application/xml", xmlContent, ParameterType.RequestBody);
            restClient.Execute(requestPost);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement application = doc.CreateElement("application");
            application.SetAttribute("name", "appA");
            XmlElement container = doc.CreateElement("container");
            container.SetAttribute("name", "color");
            XmlElement data = doc.CreateElement("data");
            data.SetAttribute("name", "data1");
            XmlElement content = doc.CreateElement("content");
            content.InnerText = "Red";

            data.AppendChild(content);
            container.AppendChild(data);
            application.AppendChild(container);
            doc.AppendChild(application);

            string xmlContent = doc.OuterXml;

            string appName = application.GetAttribute("name");
            string containerName = container.GetAttribute("name");

            var requestPost = new RestRequest($"api/somiod/{appName}/{containerName}/data/", Method.Post);

            requestPost.AddHeader("Content-Type", "application/xml");
            requestPost.AddParameter("application/xml", xmlContent, ParameterType.RequestBody);
            restClient.Execute(requestPost);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}