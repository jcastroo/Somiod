using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SOMIODMiddleware.Models;
using System.Web;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using System.Threading;
using System.Globalization;
using System.CodeDom;

namespace SOMIODMiddleware.Controllers
{
    [RoutePrefix("api/somiod")]
    public class SOMIODMiddlewareController : ApiController
    {
            string somiodXSDPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XPath", "SOMIOD.xsd");
            string CONN_STR = Properties.Settings.Default.ConnStr;

        #region Application
            [Route("")]
            public IHttpActionResult GetAllApps()
            {
            string header = HttpContext.Current.Request.Headers["somiod-discover"];

            if (header == "application")
            {
                List<string> applicationNames = new List<string>();
                string sql = "SELECT Name FROM Application";

                SqlConnection conn = null;


                try
                {
                    conn = new SqlConnection(CONN_STR);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string appName = (string)reader["Name"];
                        applicationNames.Add(appName);
                    }

                    reader.Close();
                    conn.Close();
                    return Ok(applicationNames);
                }
                catch (Exception ex)
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    throw;
                }
            }
            else
            {
                return Ok(GetAllAppsContents());
            }
            }
            private IEnumerable<Application> GetAllAppsContents()
            {
                List<Application> applications = new List<Application> { };
                string sql = "SELECT * FROM Application";
                SqlConnection conn = null;
                try
                {
                    conn = new SqlConnection(CONN_STR);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Application p = new Application
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Container = GetAllContainersByAppContents((string)reader["name"]).ToList(),
                            CreationDate = (DateTime)reader["Creation_dt"]
                        };
                        applications.Add(p);
                    }


                    reader.Close();
                    conn.Close();
                    return applications;
                }
                catch (Exception ex)
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    return applications;
                }
            }

            [HttpGet]
            [Route("{name}")]
            public IHttpActionResult GetAppByName(string name)
            {
                List<Models.Application> applications = new List<Models.Application> { };
                string sql = "SELECT * FROM Application WHERE Name= @name";
                SqlConnection conn = null;
                Models.Application application = null;
                try
                {

                    conn = new SqlConnection(CONN_STR);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", name);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        application = new Models.Application
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Container = GetAllContainersByAppContents((string)reader["Name"]).ToList(),
                            CreationDate = (DateTime)reader["Creation_dt"]
                        };
                    }


                    reader.Close();
                    conn.Close();
                    if (application == null)
                    {
                        return NotFound();
                    }
                    return Ok(application);
                }
                catch (Exception ex)
                {

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    return NotFound();
                }
            }

            [HttpPost]
            [Route("")]
            public IHttpActionResult PostApplicationWithName([FromBody] XElement xmlContents)
            {
                try
                {
                    if (xmlContents == null)
                    {
                        return BadRequest("Application name cannot be null or empty.");
                    }

                    if (xmlContents.Element("container")?.Attribute("name")?.Value == "")  
                    {
                        return BadRequest("At least one Container is required to create an Application");
                    }

                    HandlerXML handler = new HandlerXML(xmlContents, somiodXSDPath);
                    if (!handler.ValidateXML())
                    {
                        return BadRequest(handler.ValidationMessage);
                    }

                string name = xmlContents.Attribute("name").Value;

                string sqlDiscover = "SELECT COUNT(*) FROM Application WHERE Name = @Name";
                int count;
                using (SqlConnection conn = new SqlConnection(CONN_STR))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        count = (int)cmd.ExecuteScalar();
                    }
                }

                if (count > 0)
                {
                    name = GenerateRandomString(5, name);
                }


                Models.Application newApplication = new Models.Application
                {
                    Name = name,
                    
                };



                string sql = "INSERT INTO Application (Name, Creation_dt) VALUES (@Name, @CreationDate)";

                    using (SqlConnection conn = new SqlConnection(CONN_STR))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", newApplication.Name);
                            DateTime dateTime = DateTime.Now;
                            string date = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                            cmd.Parameters.AddWithValue("@CreationDate", date);
                            
                            cmd.ExecuteNonQuery();
                        }
                    }
                    PostContainer(newApplication.Name,xmlContents);
                    return Ok(newApplication.Name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return InternalServerError(ex);
                }
            }

            
            [HttpDelete]
            [Route("{name}")]
            public IHttpActionResult DeleteApplication(string name)
            {

            string sqlDiscover = "SELECT COUNT(*) FROM Application WHERE Name = @Name";
            int count;
            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    count = (int)cmd.ExecuteScalar();
                }
            }

            if (count == 0)
            {
                return BadRequest("Name not Found");
            }

            string sql = "DELETE Application WHERE Name=@Name";


                using (SqlConnection conn = new SqlConnection(CONN_STR))
                {

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;

                    try
                    {
                        cmd.Connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            return Ok();
                        }
                        else { return NotFound(); }

                    }
                    catch (Exception ex)
                    {

                        return InternalServerError();
                    }


                }
            }

            [HttpPut]
            [Route("{oldName}")]
            public IHttpActionResult UpdateApplication(string oldName, [FromBody] XElement xmlContents)
            {
                if (xmlContents == null)
                {
                    return BadRequest("Application name cannot be null or empty.");
                }

                HandlerXML handler = new HandlerXML(xmlContents, somiodXSDPath);
                if (!handler.ValidateXML())
                {
                    return BadRequest(handler.ValidationMessage);
                }

            string name = xmlContents.Attribute("name").Value;

            string sqlDiscover = "SELECT COUNT(*) FROM Application WHERE Name = @Name";
            int count;
            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    count = (int)cmd.ExecuteScalar();
                }
            }

            if (count > 0)
            {
                name = GenerateRandomString(5, name);
            }

            string sql = "UPDATE Application SET Name=@NewName WHERE Name=@OriginalName";

                using (SqlConnection conn = new SqlConnection(CONN_STR))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@OriginalName", SqlDbType.VarChar).Value = oldName;
                    cmd.Parameters.Add("@NewName", SqlDbType.VarChar).Value = name;

                    try
                    {
                        cmd.Connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            return Ok();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return InternalServerError();
                    }
                }
            }
        #endregion

        #region Containers
        [HttpGet]
        [Route("{parentName}/containers")]
        public IHttpActionResult GetAllContainersByApp(string parentName)
        {
            string header = HttpContext.Current.Request.Headers["somiod-discover"];

            if (header == "container")
            {
                List<string> containerNames = new List<string>();

                string sqlDiscover = "SELECT Name FROM container WHERE Parent_Id = (SELECT Id FROM application WHERE Name = @parentName)";
                SqlConnection connDiscover = null;
                try
                {
                    connDiscover = new SqlConnection(CONN_STR);
                    connDiscover.Open();
                    SqlCommand cmdDiscover = new SqlCommand(sqlDiscover, connDiscover);
                    cmdDiscover.Parameters.AddWithValue("@parentName", parentName);

                    SqlDataReader reader = cmdDiscover.ExecuteReader();
                    while (reader.Read())
                    {
                        string containerName = (string)reader["Name"];
                        containerNames.Add(containerName);
                        
                    }

                    reader.Close();
                    connDiscover.Close();

                    return Ok(containerNames);
                }
                catch (Exception ex)
                {

                    if (connDiscover.State == System.Data.ConnectionState.Open)
                    {
                        connDiscover.Close();
                    }
                    return Ok(containerNames);
                }
            }
            else
            {
                return Ok(GetAllContainersByAppContents(parentName));
            }
        }

        private IEnumerable<Container> GetAllContainersByAppContents(string parentName)
        {
            List<Container> containers = new List<Container> { };

            string sql = "SELECT c.* FROM container c JOIN application a ON c.Parent_Id = a.Id WHERE a.Name = @parentName";
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@parentName", parentName);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Container c = new Container
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Data = GetAllDataContents((string)reader["Name"]).ToList(),
                        Parent_Id = (int)reader["Parent_Id"],
                        Subscription = GetAllSubscriptionsContents((string)reader["Name"]).ToList(),
                    };
                    containers.Add(c);
                }

                reader.Close();
                conn.Close();

                return containers;
            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return containers;
            }
        }
        [HttpGet]
        [Route("{parentName}/{containerName}")]
        public IHttpActionResult GetContainerByName(string parentName, string containerName)
        {
            List<Container> containers = new List<Container> { };

            string sql = "SELECT c.* FROM container c JOIN application a ON c.Parent_Id = a.Id WHERE a.Name = @parentName AND c.Name = @containerName";
            SqlConnection conn = null;
            Container container = null;

            try
            {
                conn = new SqlConnection(CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@parentName", parentName);
                cmd.Parameters.AddWithValue("@containerName", containerName);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    container = new Container
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Data = GetAllDataContents((string)reader["Name"]).ToList(),
                        Parent_Id = (int)reader["Parent_Id"],
                        Subscription = GetAllSubscriptionsContents((string)reader["Name"]).ToList(),
                    };
                }

                reader.Close();
                conn.Close();
                if (container == null)
                {
                    return NotFound();
                }
                return Ok(container);
            }
            catch (Exception ex)
            {

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return NotFound();
            }
        }

        [HttpPost]
        [Route("{parentName}/containers")]
        public IHttpActionResult PostContainer(string parentName, [FromBody] XElement xmlContents)
        {
            try
            {
                if (xmlContents == null)
                {
                    return BadRequest("Application name cannot be null or empty.");
                }

                HandlerXML handler = new HandlerXML(xmlContents, somiodXSDPath);
                if (!handler.ValidateXML())
                {
                    return BadRequest(handler.ValidationMessage);
                }

                string name = xmlContents.Element("container").Attribute("name").Value;

                string sqlDiscover = "SELECT COUNT(*) FROM Container WHERE Name = @Name ";
                int count;
                using (SqlConnection conn = new SqlConnection(CONN_STR))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@parentName", parentName);

                        count = (int)cmd.ExecuteScalar();
                    }
                }

                if (count > 0)
                {
                    name = GenerateRandomString(5, name);
                }

                Container newContainer = new Container
                {
                    Name = name,
                    CreationDate = DateTime.Now
                };

                string sql = "INSERT INTO Container (Name, Parent_Id, Creation_dt) VALUES (@Name, (SELECT Id FROM application WHERE Name = @parentName), @CreationDate)";

                using (SqlConnection conn = new SqlConnection(CONN_STR))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", newContainer.Name);
                        cmd.Parameters.AddWithValue("@parentName", parentName);
                        cmd.Parameters.AddWithValue("@CreationDate", newContainer.CreationDate);

                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{parentName}/{containerName}")]
        public IHttpActionResult DeleteContainer(string parentName, string containerName)
        {

            string sqlDiscover = "SELECT COUNT(*) FROM Container WHERE Name = @Name";
            int count;
            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", containerName);
                    cmd.Parameters.AddWithValue("@parentName", parentName);

                    count = (int)cmd.ExecuteScalar();
                }
            }

            if (count == 0)
            {
                return BadRequest("Name not found");
            }

            string sql = "DELETE Container WHERE Parent_Id = (SELECT Id FROM application WHERE Name = @parentName) AND Name = @containerName";

            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@parentName", parentName);
                cmd.Parameters.AddWithValue("@containerName", containerName);

                try
                {
                    cmd.Connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return Ok();
                    }
                    else { return NotFound(); }
                }
                catch (Exception ex)
                {
                    return InternalServerError();
                }
            }
        }

        [HttpPut]
        [Route("{parentName}/{containerName}")]
        public IHttpActionResult UpdateContainer(string parentName, string containerName, [FromBody] XElement xmlContents)
        {
            if (xmlContents == null)
            {
                return BadRequest("Container name cannot be null or empty.");
            }

            HandlerXML handler = new HandlerXML(xmlContents, somiodXSDPath);
            if (!handler.ValidateXML())
            {
                return BadRequest(handler.ValidationMessage);
            }

            string name = xmlContents.Element("container").Attribute("name").Value;

            string sqlDiscover = "SELECT COUNT(*) FROM Container WHERE Name = @Name AND Parent_Id = (SELECT Id FROM application WHERE Name = @parentName)";
            int count;
            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@parentName", parentName);

                    count = (int)cmd.ExecuteScalar();
                }
            }

            if (count > 0)
            {
                name = GenerateRandomString(5, name);
            }

            string sql = "UPDATE Container SET Name=@Name WHERE Parent_Id = (SELECT Id FROM application WHERE Name = @parentName) AND Name = @containerName";

            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@parentName", SqlDbType.VarChar).Value = parentName;
                cmd.Parameters.Add("@containerName", SqlDbType.VarChar).Value = containerName;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;

                try
                {
                    cmd.Connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return InternalServerError();
                }
            }
        }
        #endregion

        #region Data
        
        [HttpGet]
        [Route("{applicationName}/{containerName}/data")]
        public IHttpActionResult GetAllData(string containerName)
        {
            string header = HttpContext.Current.Request.Headers["somiod-discover"];

            if (header == "data")
            {
                List<string> dataNames = new List<string>();
                string sql = "SELECT d.Name FROM data d INNER JOIN container c ON d.Parent_Id = c.id WHERE c.Name = @containerName";
                SqlConnection conn = null;

                try
                {
                    conn = new SqlConnection(CONN_STR);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@containerName", containerName);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string dataName = (string)reader["Name"];
                        dataNames.Add(dataName);
                    }

                    reader.Close();
                    conn.Close();

                    return Ok(dataNames);
                }
                catch (Exception ex)
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    return Ok(ex.Message);
                }
            }
            else
            {
                return Ok(GetAllDataContents(containerName));
            }
        }
        

        private IEnumerable<Data> GetAllDataContents(string containerName)
        {
            List<Data> datas = new List<Data> { };
            string sql = "SELECT d.* FROM data d INNER JOIN container c ON d.Parent_Id = c.id WHERE c.Name = @containerName";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@containerName", containerName);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        Data d = new Data
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Content = (string)reader["Content"],
                            Parent_Id = (int)reader["Parent_id"],
                        };
                        datas.Add(d);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading data: {ex.Message}");
                    }
                }

                reader.Close();
                conn.Close();

                return datas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return datas;
            }
        }
        
        
        [HttpGet]
        [Route("{applicationName}/{containerName}/data/{dataName}")]
        public IHttpActionResult GetData(string containerName, string dataName)
        {
            string sql = "SELECT d.* FROM data d INNER JOIN container c ON d.Parent_Id = c.id WHERE c.Name = @containerName AND d.Name = @dataName";
            SqlConnection conn = null;
            Data data = null;

            try
            {
                conn = new SqlConnection(CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@containerName", containerName);
                cmd.Parameters.AddWithValue("@dataName", dataName);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    data = new Data
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Content = (string)reader["Content"],
                        Parent_Id = (int)reader["Parent_Id"]
                    };
                }

                reader.Close();
                conn.Close();

                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return NotFound();
            }
        }

        
        [HttpPost]
        [Route("{applicationName}/{containerName}/data")]
        public IHttpActionResult CreateData(string containerName, [FromBody] XElement xmlContents)
        {
            try
            {
               
                if (xmlContents == null)
                {
                    return BadRequest("Data name cannot be null or empty.");
                }

                HandlerXML handler = new HandlerXML(xmlContents, somiodXSDPath);
                if (!handler.ValidateXML())
                {
                    return BadRequest(handler.ValidationMessage);
                }

                string name = xmlContents.Element("container").Element("data").Attribute("name").Value;
                string content = xmlContents.Element("container").Element("data").Element("content").Value;

                string sqlDiscover = "SELECT COUNT(*) FROM Data WHERE Name = @Name ";
                int count;
                using (SqlConnection conn = new SqlConnection(CONN_STR))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@containerName", containerName);

                        count = (int)cmd.ExecuteScalar();
                    }
                }

                if (count > 0)
                {
                    name = GenerateRandomString(5, name);
                }


                Data newData = new Data
                {
                    Name = name,
                    Content = content,
                };

                string sql = "INSERT INTO data(Name, Content, Parent_Id, Creation_dt) VALUES(@Name, @Content,(SELECT Id FROM container WHERE LOWER(Name) = LOWER(@containerName)),@CreationDate)";

                using (SqlConnection conn = new SqlConnection(CONN_STR))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = newData.Name;
                        cmd.Parameters.Add("@Content", SqlDbType.Text).Value = newData.Content; 
                        cmd.Parameters.Add("@containerName", SqlDbType.VarChar).Value = containerName;
                        cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = DateTime.Now;

                        cmd.ExecuteNonQuery();
                    }

                    string[] mStrTopicsInfo = { "data" };
                    MqttClient mcClient = null;

                    mcClient = new MqttClient("127.0.0.1");

                    mcClient.Connect(Guid.NewGuid().ToString());
                    if (!mcClient.IsConnected)
                    {
                        Console.WriteLine("Error connecting to message broker...");
                        return BadRequest("Error connecting mqtt");
                    }

                    mcClient.Publish(mStrTopicsInfo[0], Encoding.UTF8.GetBytes(content));


                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{applicationName}/{containerName}/data/{dataName}")]
        public IHttpActionResult UpdateData(string containerName, string dataName, [FromBody] XElement xmlContents)
        {
            if (xmlContents == null)
            {
                return BadRequest("Application name cannot be null or empty.");
            }

            HandlerXML handler = new HandlerXML(xmlContents, somiodXSDPath);
            if (!handler.ValidateXML())
            {
                return BadRequest(handler.ValidationMessage);
            }

            string name = xmlContents.Element("container").Element("data").Attribute("name").Value;
            string content = xmlContents.Element("container").Element("data").Element("content").Value;

            string sqlDiscover = "SELECT COUNT(*) FROM Data WHERE Name = @Name";
            int count;
            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@containerName", containerName);
                    count = (int)cmd.ExecuteScalar();
                }
            }

            if (count > 0)
            {
                name = GenerateRandomString(5, name);
            }

            string sql = "UPDATE Data SET Name=@Name,Content=@Content WHERE Parent_Id = (SELECT Id FROM container WHERE Name = @containerName) AND Name = @dataName";

            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@containerName", containerName);
                cmd.Parameters.AddWithValue("@dataName", dataName);
                cmd.Parameters.Add("@Content", SqlDbType.VarChar).Value = content;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;

                try
                {
                    cmd.Connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        string[] mStrTopicsInfo = { "data" };
                        MqttClient mcClient = null;

                        mcClient = new MqttClient(IPAddress.Parse("127.0.0.1"));

                        mcClient.Connect(Guid.NewGuid().ToString());
                        if (!mcClient.IsConnected)
                        {
                            Console.WriteLine("Error connecting to message broker...");
                            return BadRequest("Error connecting mqtt");
                        }

                        mcClient.Publish(mStrTopicsInfo[0], Encoding.UTF8.GetBytes(content));

                        mcClient.Unsubscribe(mStrTopicsInfo);
                        mcClient.Disconnect();
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return InternalServerError();
                }
            }
        }

        [HttpDelete]
        [Route("{applicationName}/{containerName}/data/{dataName}")]
        public IHttpActionResult DeleteData(string containerName, string dataName)
        {

            string sqlDiscover = "SELECT COUNT(*) FROM Data WHERE Name = @Name AND Parent_Id = (SELECT Id FROM container WHERE Name = @containerName)";
            int count;
            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", dataName);
                    cmd.Parameters.AddWithValue("@containerName", containerName);
                    count = (int)cmd.ExecuteScalar();
                }
            }

            if (count == 0)
            {
                return BadRequest("Name not found");
            }
            string sql = "DELETE Data WHERE Parent_Id = (SELECT Id FROM container WHERE Name = @containerName) AND Name = @dataName";

            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@dataName", dataName);
                cmd.Parameters.AddWithValue("@containerName", containerName);

                try
                {
                    cmd.Connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return InternalServerError();
                }
            }
        }
        #endregion

        #region Subscription
        [HttpGet]
        [Route("{appName}/{containerName}/subscription")]
        public IHttpActionResult GetAllSubscriptions(string containerName)
        {
            string header = HttpContext.Current.Request.Headers["somiod-discover"];

            if (header == "subscription")
            {
                List<string> subscriptionNames = new List<string>();

                string sql = "SELECT Name FROM subscription WHERE Parent_Id = (SELECT Id FROM container WHERE Name = @containerName)";

                SqlConnection conn = null;
                try
                {

                    conn = new SqlConnection(CONN_STR);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@containerName", containerName);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string subscriptionName = (string)reader["Name"];
                        subscriptionNames.Add(subscriptionName);
                    }


                    reader.Close();
                    conn.Close();

                    return Ok(subscriptionNames);
                }
                catch (Exception ex)
                {

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    return Ok(subscriptionNames);
                }
            }
            else
            {
                return Ok(GetAllSubscriptionsContents(containerName));
            }
        }
        private IEnumerable<Subscription> GetAllSubscriptionsContents(string containerName)
        {
            List<Subscription> subscriptions = new List<Subscription> { };

            string sql = "SELECT * FROM subscription WHERE Parent_Id = (SELECT Id FROM container WHERE Name = @containerName)";
            SqlConnection conn = null;
            try
            {

                conn = new SqlConnection(CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@containerName", containerName);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Subscription c = new Subscription
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Parent_Id = (int)reader["Parent_Id"],
                        Event = (string)reader["Event"],
                        Endpoint = (string)reader["Endpoint"],

                    };
                    subscriptions.Add(c);
                }


                reader.Close();
                conn.Close();

                return subscriptions;
            }
            catch (Exception ex)
            {

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return subscriptions;
            }
        }

        [HttpGet]
        [Route("{appName}/{containerName}/subscription/{subscriptionName}")]
        public IHttpActionResult GetSubscriptionByName(string subscriptionName, string containerName)
        {
            string sql = "SELECT * FROM subscription WHERE Parent_Id= (SELECT Id FROM container WHERE Name = @containerName) AND Name = @subscriptionName";
            SqlConnection conn = null;
            Subscription subscription = null;
            try
            {

                conn = new SqlConnection(CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subscriptionName", subscriptionName);
                cmd.Parameters.AddWithValue("@containerName", containerName);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    subscription = new Subscription
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Parent_Id = (int)reader["Parent_Id"],
                        Event = (string)reader["Event"],
                        Endpoint = (string)reader["Endpoint"],
                    };
                }


                reader.Close();
                conn.Close();
                if (subscription == null)
                {
                    return NotFound();
                }
                return Ok(subscription);
            }
            catch (Exception ex)
            {

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return NotFound();
            }
        }
        
        [HttpPost]
        [Route("{appName}/{containerName}/subscriptions")]
        public IHttpActionResult PostSubs(string containerName, [FromBody] XElement xmlContents)
        {
            try
            {
                if (xmlContents == null)
                {
                    return BadRequest("Application name cannot be null or empty.");
                }

                HandlerXML handler = new HandlerXML(xmlContents, somiodXSDPath);
                if (!handler.ValidateXML())
                {
                    return BadRequest(handler.ValidationMessage);
                }

                string name = xmlContents.Element("container").Element("subscription").Attribute("name").Value;
                string events = xmlContents.Element("container").Element("subscription").Element("event").Value;
                string endpoint = xmlContents.Element("container").Element("subscription").Element("endpoint").Value;

                //Esta validação não está funcional, ao passar neste if fica partes do xmlContents a null, e nós não conseguimos perceber o porque
                /*if (!string.Equals(xmlContents.Element("container").Element("subscription").Element("event").Value.ToString(), "1") 
                    || !string.Equals(xmlContents.Element("container").Element("subscription").Element("event").Value.ToString(), "2"))
                {
                    return BadRequest("Please input a valid event, 1 for creation , 2 for deletion");
                }*/
                

                string sqlDiscover = "SELECT COUNT(*) FROM Subscription WHERE Name = @Name AND Parent_Id = (SELECT Id FROM container WHERE Name = @containerName)";
                int count;
                using (SqlConnection conn = new SqlConnection(CONN_STR))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@containerName", containerName);
                        count = (int)cmd.ExecuteScalar();
                    }
                }

                if (count > 0)
                {
                    name = GenerateRandomString(5, name);
                }

                Subscription newSubscription = new Subscription
                {
                    Name = name,
                    Event = events,
                    Endpoint = endpoint,
                };


                string sql = "INSERT INTO Subscription (Name, Parent_Id, Creation_dt, Event, Endpoint) VALUES (@Name,(SELECT Id FROM container WHERE Name = @containerName), @CreationDate, @Event, @Endpoint)";

                using (SqlConnection conn = new SqlConnection(CONN_STR))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                        cmd.Parameters.AddWithValue("@containerName", containerName);
                        cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@Event", SqlDbType.VarChar).Value = events;
                        cmd.Parameters.Add("@Endpoint", SqlDbType.VarChar).Value = endpoint;

                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPut]
        [Route("{appName}/{containerName}/subscription/{subscriptionName}")]
        public IHttpActionResult UpdateSubscription(string containerName, string subscriptionName, [FromBody] XElement xmlContents)
        {
            if (xmlContents == null)
            {
                return BadRequest("Container name cannot be null or empty.");
            }

            HandlerXML handler = new HandlerXML(xmlContents, somiodXSDPath);
            if (!handler.ValidateXML())
            {
                return BadRequest(handler.ValidationMessage);
            }

            string name = xmlContents.Element("container").Element("subscription").Attribute("name").Value;
            string events = xmlContents.Element("container").Element("subscription").Element("event").Value;
            string endpoint = xmlContents.Element("container").Element("subscription").Element("endpoint").Value;


            string sqlDiscover = "SELECT COUNT(*) FROM Subscription WHERE  Name = @Name AND Parent_Id = (SELECT Id FROM container WHERE Name = @containerName)";
            int count;
            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@containerName", containerName);
                    count = (int)cmd.ExecuteScalar();
                }
            }

            if (count > 0)
            {
                name = GenerateRandomString(5, name);
            }


            string sql = "UPDATE Subscription SET Name=@Name,Event=@Event,Endpoint=@Endpoint WHERE Parent_Id= (SELECT Id FROM container WHERE Name = @containerName) AND Name = @OriginalName";

            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@containerName", containerName);
                cmd.Parameters.AddWithValue("@OriginalName", subscriptionName);
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@Event", SqlDbType.VarChar).Value = events;
                cmd.Parameters.Add("@Endpoint", SqlDbType.VarChar).Value = endpoint;

                try
                {
                    cmd.Connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return InternalServerError();
                }

            }
        }


        [HttpDelete]
        [Route("{appName}/{containerName}/subscription/{subscriptionName}")]
        public IHttpActionResult DeleteSubscription(string containerName, string subscriptionName)
        {

            string sqlDiscover = "SELECT COUNT(*) FROM Subscription WHERE Name = @Name AND Parent_Id = (SELECT Id FROM container WHERE Name = @containerName)";
            int count;
            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlDiscover, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", subscriptionName);
                    cmd.Parameters.AddWithValue("@containerName", containerName);

                    count = (int)cmd.ExecuteScalar();
                }
            }
            if (count == 0)
            {
                return BadRequest("Name not found");
            }


            string sql = "DELETE Subscription WHERE Parent_Id= (SELECT Id FROM container WHERE Name = @containerName) AND Name = @subscriptionName";


            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@containerName", containerName);
                cmd.Parameters.AddWithValue("@subscriptionName", subscriptionName);

                try
                {
                    cmd.Connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return Ok();
                    }
                    else { return NotFound(); }

                }
                catch (Exception ex)
                {

                    return InternalServerError();
                }

            }
        }
        #endregion

        static string GenerateRandomString(int length, string name = null)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] randomArray = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomArray[i] = chars[random.Next(chars.Length)];
            }

            if (string.IsNullOrEmpty(name))
            {
                return new string(randomArray);
            }
            else
            {
                return $"{name}_{new string(randomArray)}";
            }
        }
    }
}
