I made use of Swagger and Postman Swagger can be access whie using Visul Studio i made use of visual studio 2022.
swagger is straight forward doesnt need any setup.
NOTE THE url IS explanation only check and make sure it correspond with yours
................Using Postman............
Open Postman.
Create a new request by clicking on the "New" button and selecting "Request".
...........Configure Request..................
Select the HTTP method (GET, POST, DELETE) based on the endpoint you want to test.
Enter the URL of your API endpoint (e.g., http://localhost:7205/api/log/search). 
NOTE: THE PORT NUMBER WHICH IS 7205 IS BASE ON MY OWN PORT NUMBER CHECK YOURS
.............Add Query Parameters............
Some Endpoint request require Parameter 
Click on the "Params" tab in Postman.

Add the necessary query parameters as specified in the endpoint definition.
.............Example Requests.............

1. Search Logs in Directories method: GET
URL: http://localhost:7205/api/log/search
Query Parameters:
directories: C:\AmadeoLogs
searchPattern: log

2.  Counts number of unique errors per log files method: GET
URL: http://localhost:7205/api/log/count-unique-errors
Query Parameters:
files: C:\AmadeoLogs

3. Count Duplicated Errors method: GET
URL: http://localhost:7205/api/log/count-duplicated-errors
Query Parameters:
files: C:\AmadeoLogs

4  Counts number of duplicated errors per log files method: GET
URL: http://localhost:7205/api/log/delete-archives
Query Parameters:
directory: C:\Loggings
startDate: 2023-01-01
endDate: 2023-12-31

5. Delete archive from a period method: DELETE
URL: http://localhost:7205/api/log/delete-logs
Query Parameters:
directory: C:\Loggings
startDate: 2023-01-01
endDate: 2023-12-31

6. Archive Logs From Period method: POST
URL: http://localhost:7205/api/log/archive-logs
Query Parameters:
directory: C:\Loggings
startDate: 2023-01-01
endDate: 2023-12-31

7. upload logs on a remote server per API method: POST
URL: http://localhost:7205/api/log/upload-logs
Query Parameters:
files: C:\Logs\file1.log,C:\Logs\file2.log
apiUrl: http://remote-server.com/upload

8. Delete logs from a period method Delete
URL: http://localhost:7205/api/log/count-total-logs
Query Parameters:
directory: C:\Loggings
startDate: 2023-01-01
endDate: 2023-12-31

9. Count Total Available Logs method: GET
URL: http://localhost:7205/api/log/count-total-logs
Query Parameters:
directory: C:\AWIErrors
startDate: 2023-01-01
endDate: 2023-12-31

10. Search Logs by Size method: GET
URL: http://localhost:7205/api/log/search-by-size
Query Parameters:
directory: C:\AWIErrors
minSize: 1000
maxSize: 5000
Search Logs by Directory:

Running the Requests
Set the method and URL in Postman.
Add the query parameters required by the endpoint.
Click the "Send" button to execute the request.
Review the response returned by the API.
