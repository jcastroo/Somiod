using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Data.SqlClient;
using RestSharp;
using System.Reflection;
using SOMIODMiddleware.Models;
using System.Xml.Linq;
using System.Xml;

namespace SOMIODPUB
{

    public partial class FormPub : Form
    {

        private MqttClient mcClient = null;

        private RestClient restClient = null;
        private string baseURI = @"http://localhost:50998";
        public FormPub()
        {
            InitializeComponent();
            restClient = new RestClient(baseURI);
        }

        private void FormPub_Load(object sender, EventArgs e)
        {
            var request = new RestRequest("api/somiod/discover/", Method.Get);
            request.RequestFormat = DataFormat.Json;


            var response = restClient.Execute<List<string>>(request);

            if (response != null && response.Data != null && response.Data.Count > 0)
            {
                var responseData = response.Data;

                comboBoxType.Items.Clear();
                comboBoxType2.Items.Clear();
                comboBoxType3.Items.Clear();
                comboBoxType4.Items.Clear();
                comboBoxType5.Items.Clear();
                foreach (string appName in responseData)
                {
                    comboBoxType.Items.Add(appName);
                    comboBoxType2.Items.Add(appName);
                    comboBoxType3.Items.Add(appName);
                    comboBoxType4.Items.Add(appName);
                    comboBoxType5.Items.Add(appName);

                    comboBoxType3.SelectedIndexChanged += comboBoxType_SelectedIndexChanged;

                }
            }
            else
            {
                comboBoxType.Items.Add("No apps available");
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxNomeContainer.Enabled = true;
        }
        private void comboBoxType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxUpdateApplication.Enabled = true;
        }
        private void comboBoxType3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxType7.Enabled = true;

            string selectedAppName = comboBoxType3.SelectedItem.ToString();
    
            var requestContainer = new RestRequest($"api/somiod/discover/{selectedAppName}/containers", Method.Get);
            requestContainer.RequestFormat = DataFormat.Json;

            var responseContainer = restClient.Execute<List<string>>(requestContainer);

            if (responseContainer != null && responseContainer.Data != null)
            {
                var responseContainerData = responseContainer.Data;

                comboBoxType7.Items.Clear();
                foreach (string containerName in responseContainerData)
                {
                    comboBoxType7.Items.Add(containerName);
                }
            }
        }

        private void comboBoxType5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxType6.Enabled = true;
            string selectedAppName = comboBoxType5.SelectedItem.ToString();

            var requestContainer = new RestRequest($"api/somiod/discover/{selectedAppName}/containers", Method.Get);
            requestContainer.RequestFormat = DataFormat.Json;

            var responseContainer = restClient.Execute<List<string>>(requestContainer);

            if (responseContainer != null && responseContainer.Data != null)
            {
                var responseContainerData = responseContainer.Data;

                comboBoxType6.Items.Clear();
                foreach (string containerName in responseContainerData)
                {
                    comboBoxType6.Items.Add(containerName);
                }
            }
        }

        private void comboBoxType7_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxUpdateContainer.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBoxType.SelectedItem != null && !string.IsNullOrWhiteSpace(textBoxNomeContainer.Text))
            {
                string parentAppName = comboBoxType.SelectedItem.ToString();
                string containerName = textBoxNomeContainer.Text;

                var request = new RestRequest($"api/somiod/{parentAppName}/containers", Method.Post);

                request.AddHeader("Content-Type", "application/xml");
                request.AddParameter("application/xml", "\"" + containerName + "\"", ParameterType.RequestBody);

                RestResponse response = restClient.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Container Added !!");
                    comboBoxType.SelectedItem = null;
                    textBoxNameApp.Text = null;
                }
                else
                {
                    MessageBox.Show($"Unable to add new Container:{response.StatusCode}: !!");
                }
            }
            else
            {
                MessageBox.Show("Please select an application and provide a container name.");
            }

        }

        private void textBoxNome_TextChanged(object sender, EventArgs e)
        {

        }


        private void buttonConnect_Click(object sender, EventArgs e)
        {

            mcClient = new MqttClient(IPAddress.Parse(brokerDomainTextBox.Text));

            mcClient.Connect(Guid.NewGuid().ToString());
            if (!mcClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            else
            {

                MessageBox.Show("CONECTADO COM SUCESSO");
                textBoxNomeContainer.Visible = true;
                comboBoxType.Visible = true;
                comboBoxType2.Visible = true;
                comboBoxType3.Visible = true;
                comboBoxType4.Visible = true;
                comboBoxType5.Visible = true;
                comboBoxType6.Visible = true;
                comboBoxType7.Visible = true;
                buttonCreateContainer.Visible = true;
                buttonCreateApp.Visible = true;
                buttonUpdateApp.Visible = true;
                buttonUpdateContainer.Visible = true;
                buttonRemoveAplication.Visible = true;
                buttonRemoveContainer.Visible = true;
                textBoxNameApp.Visible = true;
                textBoxUpdateApplication.Visible = true;
                textBoxUpdateContainer.Visible = true;
                label3.Visible = true;
                label1.Visible = true;
                label5.Visible = true;
                label4.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label13.Visible = true;
                tabControlCreate.Visible = true;
                brokerDomainTextBox.Visible = false;
                label2.Visible = false;
                buttonConnect.Visible = false;

            }
        }

        private void buttonCreateApp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxNameApp.Text))
            {
                string appName = textBoxNameApp.Text;

                XmlDocument doc = new XmlDocument();
                XmlElement application = doc.CreateElement("application");
                application.SetAttribute("name", appName);
                XmlElement dateTimeXML = doc.CreateElement("creation_dt");
                dateTimeXML.InnerText = DateTime.Now.ToString();
                application.AppendChild(dateTimeXML);
                doc.AppendChild(application);

                string xmlContent = doc.OuterXml;
                   
                var request = new RestRequest("api/somiod/", Method.Post);

                request.AddHeader("Content-Type", "application/xml");
                request.AddParameter("application/xml", xmlContent, ParameterType.RequestBody);

                RestResponse response = restClient.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Application Added !!");

                    var discoverRequest = new RestRequest("api/somiod/discover/", Method.Get);
                    discoverRequest.RequestFormat = DataFormat.Json;

                    var discoverResponse = restClient.Execute<List<string>>(discoverRequest);
                    if (discoverResponse != null && discoverResponse.Data != null && discoverResponse.Data.Count > 0)
                    {
                        var discoverData = discoverResponse.Data;

                        comboBoxType.Items.Clear();
                        comboBoxType2.Items.Clear();
                        comboBoxType3.Items.Clear();
                        comboBoxType4.Items.Clear();
                        comboBoxType5.Items.Clear();
                        foreach (string discoveredAppName in discoverData)
                        {
                            comboBoxType.Items.Add(discoveredAppName);
                            comboBoxType2.Items.Add(discoveredAppName);
                            comboBoxType3.Items.Add(discoveredAppName);
                            comboBoxType4.Items.Add(discoveredAppName);
                            comboBoxType5.Items.Add(discoveredAppName);
                        }
                    }
                    else
                    {
                        comboBoxType.Items.Add("No apps available");
                    }
                }
                else
                {
                    MessageBox.Show($"Unable to add new Application:{response.StatusCode}: {response.Content}: {xmlContent} !!");
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBoxType2.SelectedItem != null && !string.IsNullOrEmpty(textBoxUpdateApplication.Text))
            {
                string oldAppName = comboBoxType2.SelectedItem.ToString();
                string applicationName = textBoxUpdateApplication.Text;

                XmlDocument doc = new XmlDocument();
                XmlElement application = doc.CreateElement("application");
                application.SetAttribute("name", applicationName);
                XmlElement dateTimeXML = doc.CreateElement("creation_dt");
                dateTimeXML.InnerText = DateTime.Now.ToString();
                
                application.AppendChild(dateTimeXML);
                doc.AppendChild(application);

                string xmlContent = doc.OuterXml;

                var request = new RestRequest($"api/somiod/{oldAppName}", Method.Put);

                request.AddHeader("Content-Type", "application/xml");
                request.AddParameter("application/xml", xmlContent, ParameterType.RequestBody);

                RestResponse response = restClient.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Application Updated !!");
                    var discoverRequest = new RestRequest("api/somiod/discover/", Method.Get);
                    discoverRequest.RequestFormat = DataFormat.Xml;

                    var discoverResponse = restClient.Execute<List<string>>(discoverRequest);
                    if (discoverResponse != null && discoverResponse.Data != null && discoverResponse.Data.Count > 0)
                    {
                        var discoverData = discoverResponse.Data;

                        comboBoxType.Items.Clear();
                        comboBoxType2.Items.Clear();
                        comboBoxType3.Items.Clear();
                        comboBoxType4.Items.Clear();
                        comboBoxType5.Items.Clear();
                        foreach (string discoveredAppName in discoverData)
                        {
                            comboBoxType.Items.Add(discoveredAppName);
                            comboBoxType2.Items.Add(discoveredAppName);
                            comboBoxType3.Items.Add(discoveredAppName);
                            comboBoxType4.Items.Add(discoveredAppName);
                            comboBoxType5.Items.Add(discoveredAppName);
                        }
                    }
                    else
                    {
                        comboBoxType.Items.Add("No apps available");
                    }
                }
                else
                {
                    MessageBox.Show($"Unable to update Application:{response.StatusCode}: !!");
                }
            }
            else
            {
                MessageBox.Show("Please select an application and provide a container name.");
            }
        }
        private void buttonRemoveAplication_Click(object sender, EventArgs e)
        {
            if (comboBoxType4.SelectedItem != null)
            {
                string applicationName = comboBoxType4.SelectedItem.ToString();

                XmlDocument doc = new XmlDocument();
                XmlElement application = doc.CreateElement("application");
                application.SetAttribute("name", applicationName);
                XmlElement dateTimeXML = doc.CreateElement("creation_dt");
                dateTimeXML.InnerText = DateTime.Now.ToString();
                application.AppendChild(dateTimeXML);
                doc.AppendChild(application);

                string xmlContent = doc.OuterXml;

                var request = new RestRequest($"api/somiod/{applicationName}", Method.Delete);

                request.AddHeader("Content-Type", "application/xml");
                request.AddParameter("application/xml", xmlContent, ParameterType.RequestBody);

                RestResponse response = restClient.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Application Deleted !!");
                    var discoverRequest = new RestRequest("api/somiod/discover/", Method.Get);
                    discoverRequest.RequestFormat = DataFormat.Xml;

                    var discoverResponse = restClient.Execute<List<string>>(discoverRequest);
                    if (discoverResponse != null && discoverResponse.Data != null && discoverResponse.Data.Count > 0)
                    {
                        var discoverData = discoverResponse.Data;

                        comboBoxType.Items.Clear();
                        comboBoxType2.Items.Clear();
                        comboBoxType3.Items.Clear();
                        comboBoxType4.Items.Clear();
                        comboBoxType5.Items.Clear();
                        foreach (string discoveredAppName in discoverData)
                        {
                            comboBoxType.Items.Add(discoveredAppName);
                            comboBoxType2.Items.Add(discoveredAppName);
                            comboBoxType3.Items.Add(discoveredAppName);
                            comboBoxType4.Items.Add(discoveredAppName);
                            comboBoxType5.Items.Add(discoveredAppName);
                        }
                    }
                    else
                    {
                        comboBoxType.Items.Add("No apps available");
                    }
                }
                else
                {
                    MessageBox.Show($"Unable to update Application:{response.StatusCode}: !!");
                }
            }
            else
            {
                MessageBox.Show("Please select an application and provide a container name.");
            }
        }
        private void comboBoxType4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void label5_Click(object sender, EventArgs e)
        {
        }
        private void tabPageApp_Click(object sender, EventArgs e)
        {

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

    }
}
