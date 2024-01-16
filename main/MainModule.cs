using System;
using System.Collections.Generic;
using System.Linq;
using CrimeAnalysisReportingSystem.Dao;
using CrimeAnalysisReportingSystem.Entity;

namespace CrimeAnalysisReportingSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CrimeAnalysisServiceImpl crimeService = new CrimeAnalysisServiceImpl();

            while (true)
            {
                Console.WriteLine("Crime Analysis and Reporting System");
                Console.WriteLine("1. Create Incident");
                Console.WriteLine("2. Update Incident Status");
                Console.WriteLine("3. Get Incidents in Date Range");
                Console.WriteLine("4. Search Incidents");
                Console.WriteLine("5. Create Case");
                Console.WriteLine("6. Get Case Details");
                Console.WriteLine("7. Update Case Details");
                Console.WriteLine("8. Get All Cases");
                Console.WriteLine("9. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Create Incident
                        CreateIncident(crimeService);
                        break;
                    case "2":
                        // Update Incident Status
                        UpdateIncidentStatus(crimeService);
                        break;
                    case "3":
                        // Get Incidents in Date Range
                        GetIncidentsInDateRange(crimeService);
                        break;
                    case "4":
                        // Search Incidents
                        SearchIncidents(crimeService);
                        break;
                    case "5":
                        // Create Case
                        CreateCase(crimeService);
                        break;
                    case "6":
                        // Get Case Details
                        GetCaseDetails(crimeService);
                        break;
                    case "7":
                        // Update Case Details
                        UpdateCaseDetails(crimeService);
                        break;
                    case "8":
                        // Get All Cases
                        GetAllCases(crimeService);
                        break;
                    case "9":
                        // Exit
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void CreateIncident(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
                
                Console.Write("Enter Incident Type: ");
                string incidentType = Console.ReadLine();

                Console.Write("Enter Incident Date (yyyy-MM-dd): ");
                DateTime incidentDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter Location: ");
                string location = Console.ReadLine();

                Console.Write("Enter Description: ");
                string description = Console.ReadLine();

                Console.Write("Enter Status: ");
                string status = Console.ReadLine();

                Console.Write("Enter Victim ID: ");
                int victimId = int.Parse(Console.ReadLine());

                Console.Write("Enter Suspect ID: ");
                int suspectId = int.Parse(Console.ReadLine());

                // Create Incident object
                Incident newIncident = new Incident
                {
                    IncidentType = incidentType,
                    IncidentDate = incidentDate,
                    Location = location,
                    Description = description,
                    Status = status,
                    VictimID = victimId,
                    SuspectID = suspectId
                };

               
                bool result = crimeService.CreateIncident(newIncident);

                // Display result
                Console.WriteLine(result ? "Incident created successfully." : "Error creating incident.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating incident: {ex.Message}");
            }
        }

        private static void UpdateIncidentStatus(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
               
                Console.Write("Enter Incident ID: ");
                int incidentId = int.Parse(Console.ReadLine());

                Console.Write("Enter New Status: ");
                string newStatus = Console.ReadLine();

               
                bool result = crimeService.UpdateIncidentStatus(incidentId, newStatus);

               
                if (result)
                    Console.WriteLine("Incident status updated successfully!");
                else
                    Console.WriteLine("Error updating incident status. Check the logs for details.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating incident status: {ex.Message}");
            }
        }

        private static void GetIncidentsInDateRange(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
               
                Console.Write("Enter Start Date (MM/DD/YYYY): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter End Date (MM/DD/YYYY): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

              
                IEnumerable<Incident> incidents = crimeService.GetIncidentsInDateRange(startDate, endDate);

              
                Console.WriteLine("Incidents within the specified date range:");
                foreach (var incident in incidents)
                {
                    Console.WriteLine($"Incident ID: {incident.IncidentID}, Type: {incident.IncidentType}, Date: {incident.IncidentDate},Location: {incident.Location}, Description: {incident.Description}, status: {incident.status}, Victim ID: {incident.VictimID}, Suspect ID: {incident.SuspectID}");
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving incidents in date range: {ex.Message}");
            }
        }
        private static void SearchIncidents(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
               
                Console.Write("Enter search criteria: ");
                string criteria = Console.ReadLine();

                
                IEnumerable<Incident> incidents = crimeService.SearchIncidents(criteria);

               
                Console.WriteLine($"Search results for criteria '{criteria}':");
                foreach (var incident in incidents)
                {
                    Console.WriteLine($"Incident ID: {incident.IncidentID}, Type: {incident.IncidentType}, Date: {incident.IncidentDate}, Location: {incident.Location}, Description: {incident.Description}, status: {incident.status}, Victim ID: {incident.VictimID}, Suspect ID: {incident.SuspectID");
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching incidents: {ex.Message}");
            }
        }

        private static void CreateCase(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
              
                Console.Write("Enter Case Description: ");
                string caseDescription = Console.ReadLine();

                Console.Write("Enter Incident ID: ");
                int incidentId;
                while (!int.TryParse(Console.ReadLine(), out incidentId))
                {
                    Console.Write("Invalid input. Please enter a valid Incident ID: ");
                }

                
                Case newCase = crimeService.CreateCase(caseDescription, incidentId);

                
                if (newCase != null)
                    Console.WriteLine($"Case created successfully! Case ID: {newCase.CaseID}");
                else
                    Console.WriteLine("Error creating case. Check the logs for details.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating case: {ex.Message}");
            }
        }
        private static void GetCaseDetails(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
               
                Console.Write("Enter Case ID: ");
                int caseId;
                while (!int.TryParse(Console.ReadLine(), out caseId))
                {
                    Console.Write("Invalid input. Please enter a valid Case ID: ");
                }

                
                Case caseDetails = crimeService.GetCaseDetails(caseId);

                
                if (caseDetails != null)
                {
                    Console.WriteLine($"Case ID: {caseDetails.CaseID}, Description: {caseDetails.CaseDescription}, Incident ID: {caseDetails.IncidentID}");
                    
                }
                else
                {
                    Console.WriteLine($"Case with ID {caseId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving case details: {ex.Message}");
            }
        }
        private static void UpdateCaseDetails(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
                
                Console.Write("Enter Case ID to update: ");
                int caseId;
                while (!int.TryParse(Console.ReadLine(), out caseId))
                {
                    Console.Write("Invalid input. Please enter a valid Case ID: ");
                }

                Console.Write("Enter updated Case Description: ");
                string updatedCaseDescription = Console.ReadLine();

               
                Case updatedCase = new Case
                {
                    CaseID = caseId,
                    CaseDescription = updatedCaseDescription
                   
                };

              
                bool result = crimeService.UpdateCaseDetails(updatedCase);

               
                if (result)
                    Console.WriteLine("Case details updated successfully!");
                else
                    Console.WriteLine("Error updating case details. Check the logs for details.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating case details: {ex.Message}");
            }
        }
        private static void GetAllCases(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
               
                IEnumerable<Case> cases = crimeService.GetAllCases();

            
                if (cases != null && cases.Any())
                {
                    foreach (var caseObj in cases)
                    {
                        Console.WriteLine($"Case ID: {caseObj.CaseID}, Description: {caseObj.CaseDescription}, Incident ID: {caseObj.IncidentID}");
                        
                    }
                }
                else
                {
                    Console.WriteLine("No cases found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all cases: {ex.Message}");
            }
        }
    }

}
