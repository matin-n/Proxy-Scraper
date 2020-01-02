# Proxy-Scraper

Basic proxy scraper writen in Visual Basic .NET. 

* The program will read the `proxysources.txt`; which contains a list of known websites that provide public proxies
* The program will then scrape each website in the proxysources text file
* Regex filtering is used to determine the IP and port of the proxy. The regex pattern used is `(\d{1,3}\.){3}\d{1,3}` 
* The scraped proxies will be added to a string list 
* Each entry in the list will then be JSON formatted and uploaded to the central database by sending a post request to our API server
