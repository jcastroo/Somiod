# SOMIOD: Service Oriented Middleware for Interoperability and Open Data

## Project Overview
SOMIOD is a middleware solution designed to enhance interoperability and promote open data sharing across various Internet of Things (IoT) systems. It standardizes how data is accessed, written, and notified, regardless of the application domain, enabling seamless integration between different devices and platforms. The middleware uses RESTful Web Services and open web standards to achieve uniformity in data handling and application creation.

## Features
- **Uniform Data Access**: Data is always accessed, written, and notified in the same way, promoting consistency across applications.
- **Interoperability**: Designed to overcome the "Silo of Things" problem by ensuring open communication between different IoT systems.
- **RESTful API**: All operations are performed using a RESTful API, ensuring simplicity and flexibility in development.
- **Subscription Mechanism**: Supports event-based notifications via HTTP and MQTT, allowing for real-time updates when data is created or deleted.

## Standardized Web Service Structure
All URLs start with: `http://<domain:port>/api/somiod/...`

## Supported Resources
- **Application**: `/application`
- **Container**: `/application/{app_name}/container`
- **Data**: `/application/{app_name}/container/{container_name}/data`
- **Subscription**: `/application/{app_name}/container/{container_name}/subscription`

## CRUD Operations
Create, Read, Update, and Delete operations are supported for most resources.

### Discover Operation
A special discovery mode is available using HTTP headers to list all available resources of a given type (e.g., applications, containers, data).

## Notification Mechanism
Notifications are triggered on data creation or deletion. Notifications can be sent via:
- **MQTT**
- **HTTP**

## Resource Hierarchy
1. **Application**: Represents a specific application.
2. **Container**: Groups data and subscriptions.
3. **Data**: Represents individual records within a container.
4. **Subscription**: Represents event notifications (creation or deletion).

## Data Format
All transferred data is in **XML** format.

## Database Persistence
All resources and their data are persisted in a database.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Authors
- João Castro ([@jcastroo](https://github.com/jcastroo))
- Pedro Antunes ([@Sneuc](https://github.com/Sneuc))
- Tomás Santos  ([@monico18](https://github.com/monico18))

