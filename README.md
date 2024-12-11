# ErikVallis3175Final
Final Exam for 3175
# README: Car API and Console App

This project consists of two parts:
1. A Web API for managing car data.
2. A Console Application to consume the API.

## Dependencies
### Web API
- Microsoft.AspNetCore.Mvc.NewtonsoftJson

### Console App
- Newtonsoft.Json
- System.Net.Http


## Instructions
### Web API Setup
1. Navigate to the Web API project directory (e.g., `CarApiFinal`).
2. Install the required NuGet packages by running the following commands in your terminal:

dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

3. Run the API using the following command:

dotnet run

4. Note the base URL (e.g., `http://localhost:5048/`). The API must be running to use the Console Application.
The port being used locally may differ for you, IE :5050 or otherwise, This must be adjusted in the Console application
prior to running it. (More below)

### Console App Setup
1. Navigate to the Console Application project directory.
2. Install the required NuGet packages by running the following commands in your terminal:

dotnet add package Newtonsoft.Json
dotnet add package System.Net.Http

3. Open the `Program.cs` file in the Console App and ensure the `baseUrl` variable matches the API's base URL. For example:

string baseUrl = "http://localhost:5048/";


4. Build and run the Console Application:

dotnet run


### Usage
- Start the Web API before using the Console Application.
- Use the Console Application to perform CRUD operations on the car data.
- Update the base URL in the Console App if the API's localhost port changes.

### Example
- **Web API Base URL**: `http://localhost:5048/`
- **Console App Operation**: `Read All Cars`
  - Output:
    ```
    Cars from API:
    ID: 1, Make: Mine, Model: NotYours, Year: 1990
    ```

### Notes
- Ensure the `cars.json` file exists in the API's `Data` folder.
- The API automatically creates an empty `cars.json` file if it doesn’t exist, but you can manually add data for testing.
