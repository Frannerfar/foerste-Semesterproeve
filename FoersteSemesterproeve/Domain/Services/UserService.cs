using FoersteSemesterproeve.Domain.Models;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace FoersteSemesterproeve.Domain.Services
{
    /// <summary>
    ///     UserService class
    /// </summary>
    /// <author>Rasmus, Marcus, Martin</author>
    public class UserService
    {
        public User? authenticatedUser;
        public User? targetUser;
        public List<User> users;
        public MembershipService membershipService;
        string filepath = Path.Combine(Environment.CurrentDirectory, "Data", "Files", "users.txt");


        /// <summary>
        ///     Constructor til UserService class
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="membershipService"></param>
        public UserService(MembershipService membershipService) 
        {
            // parameteren membershipService sættes i objektets field
            this.membershipService = membershipService;

            // metoden "LoadUsers" kaldes og returnerer en liste af brugere i "users" field;
            users = LoadUsers();
        }


        /// <summary>
        ///     Bruges til at tjekke om parameteren "password" matcher brugerens password
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool isUserPasswordCorrect(User user, string password)
        {
            // Hvis parameteren password har samme værdi som user objektets password
            if (password == user.password)
            {
                // Returner true, hvis det er det samme
                return true;
            }
            // Returnerer falsk, hvis det ikke er det samme
            return false;
        }

        /// <summary>
        ///     Bruges til at fjerne bruger fra listen af brugere
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="user"></param>
        public void DeleteUserByObject(User user)
        {
            // parameteren user fjernes fra listen af brugere "users" i field
            users.Remove(user);
            // Looper igennem alle aktiviteter som brugeren er tilmeldt
            for(int i = 0; user.activityList.Count > 0; i++)
            {
                // Fjerner brugeren fra deltagerlisten på hver aktivitet, hvor brugeren er deltager på
                user.activityList[i].participants.Remove(user);
            }
            // Fjerner alle aktiviteter på brugerens liste over aktiviteter
            user.activityList.Clear();
        }


        /// <summary>
        ///     Bruges til at indhente data fra .txt fil og returnere liste af brugere ud fra dataen.
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <returns></returns>
        public List<User> LoadUsers()
        {
            // Her læses al tekst fra txt filen og sættes i string variablen "userText"
            string userText = File.ReadAllText(this.filepath);
            Debug.WriteLine("File read!");
            Debug.WriteLine($"File path: {this.filepath}");
            Debug.WriteLine(userText);

            // Ny liste af brugere instantieres og sættes i variablen "users"
            List<User> users = new List<User>();

            // stringen userText splittes op ved hvert semikolon ";" og sættes i variablen usersInTextFormat, som array af strings
            // Dette gøres fordi hver bruger er adskilt med et semikolon i users.txt filen
            string[] usersInTextFormat = userText.Split(";");

            // For hver bruger tekst i arrayet "usersInTextFormat"
            for (int i = 0; i < usersInTextFormat.Length; i++)
            {
                // Try for at gribe exceptions
                try
                {
                    // brugerens teksts splittes endnu engang op ved hvert komma "," 
                    // og sættes i variablen "userInformationParts", som array af strings
                    string[] userInformationParts = usersInTextFormat[i].Split(",");

                    // hvert array element sættes i ny string variabel
                    string stringID = userInformationParts[0];
                    string stringFirstName = userInformationParts[1];
                    string stringLastName = userInformationParts[2];
                    string stringEmail = userInformationParts[3];
                    string stringStreet = userInformationParts[4];
                    string stringCity = userInformationParts[5];
                    string stringPassword = userInformationParts[6];
                    string stringIsCoach = userInformationParts[7];
                    string stringIsAdmin = userInformationParts[8];
                    string stringDOB = userInformationParts[9];
                    string stringPostal = userInformationParts[10];
                    string stringMembershipID = userInformationParts[11];

                    Debug.WriteLine($"ITERATION: {i}");

                    // Bool til at tjekke om noget er fejlet. Sættes kun til true, når en konvertering er fejlet.
                    bool flag = false;

                    // Konverterer stringID om til en faktisk integer ID.
                    int id;
                    bool isUserID = int.TryParse(stringID, out id);
                    if (!isUserID) { MessageBox.Show($"UserID {stringID} could not be converted to a integer"); flag = true; }

                    // Konverterer stringIsCoach til faktisk bool.
                    bool isCoach;
                    bool isUserCoachBool = bool.TryParse(stringIsCoach, out isCoach);
                    if (!isUserCoachBool) { MessageBox.Show($"isCoach bool: {stringIsCoach} could not be converted to a bool"); flag = true; }

                    // Konverterer stringIsAdmin til faktisk bool.
                    bool isAdmin;
                    bool isUserAdminBool = bool.TryParse(stringIsAdmin, out isAdmin);
                    if (!isUserAdminBool) { MessageBox.Show($"isAdmin bool: {stringIsAdmin} could not be converted to a bool"); flag = true; }

                    // Konverterer stringDOB til faktisk DateOnly
                    DateOnly dob;
                    //bool isUserDOB = DateOnly.TryParse(stringDOB, out dob);
                    bool isUserDOB = DateOnly.TryParseExact(stringDOB, "d-m-yyyy", out dob);
                    if (!isUserDOB) { MessageBox.Show($"Date of Birth DateOnly: {stringDOB} could not be converted to a DateOnly"); flag = true; }

                    // Konverterer stringPostal til integer
                    int postal;
                    int? postalNullable = null;
                    // Hvis konvertering lykkedes
                    if (int.TryParse(stringPostal, out postal))
                    {   // postalNullable sættes til at være postal værdien, da User constructor forventer at postal er int? (? betyder nullable)
                        postalNullable = postal;
                    }
                    // Hvis konvertering mislykkedes
                    else
                    {
                        MessageBox.Show($"Postal integer: {stringPostal} could not be converted to a integer");
                        flag = true;
                    }

                    // Konverterer stringMembershipID til integer
                    int membershipTypeId;
                    bool isMembershipTypeID = int.TryParse(stringMembershipID, out membershipTypeId);
                    if (!isMembershipTypeID) { MessageBox.Show($"MembershipTypeID {stringMembershipID} could not be converted to a integer, from userID: {id}"); flag = true; }


                    // Hvis ikke nogen konverteringer er fejlet
                    if (!flag)
                    {
                        // variabel membershipType af typen MembershipType? sættes til null (? betyder nullable)
                        MembershipType? membershipType = null;
                        // Looper igennem alle membershipTypes
                        for (int j = 0; j < membershipService.membershipTypes.Count; j++)
                        {
                            // Hvis membershipTypens ID er det samme som det ID vi har fået fra konverteringen fra stringMembershipID
                            if (membershipService.membershipTypes[j].id == membershipTypeId)
                            {
                                // membershipType 7 linjer ovenfor sættes til at være denne iteration af membershipTypes.
                                membershipType = membershipService.membershipTypes[j];
                            }
                        }
                        // Efter for loopet og membershipType ikke er null mere
                        if (membershipType != null)
                        {
                            // Der oprettes nyt objekt af klassen User og sættes i variablen "user"
                            User user = new User(id, stringFirstName, stringLastName, stringEmail, stringStreet, stringCity, stringPassword, isCoach, isAdmin, dob, postal, membershipType);
                            // Tilføj den nyoprettede bruger til listen af brugere vi har loaded ind fra .txt filen
                            users.Add(user);
                        }
                    }
                }
                // Her fanges exceptions / fejl
                catch (Exception e)
                {
                    MessageBox.Show($"ERROR in iteration #{i} \n RAW DATA:\n {usersInTextFormat[i]} \n EXCEPTION: \n {e.Message}");
                    continue;
                }
            }
            // listen af brugere oprettet fra .txt filen returneres
            return users;
        }


        /// <summary>
        ///      Bruges til at få næste ID i rækken, ud fra de eksisterende User objekter i parametereren "users".
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="users"></param>
        /// <returns></returns>
        public int GetNewId(List<User> users)
        {
            // int variabel sættes til 0
            int highestId = 0;
            // Looper igennem antallet af brugere i listen
            for (int i = 0; i < users.Count; i++)
            {
                // Hvis brugerens ID er højere end variablen "highestId"
                if (users[i].id > highestId)
                {
                    // Sæt variablen "highestId" til at have værdien fra loopets nuværende iteration af brugerens ID
                    highestId = users[i].id;
                }
            }
            // Tilføj 1 til variablen, da vi gerne vil have næste ID og ikke bare det højeste ID.
            highestId++;
            // returnerer det variablen "highestId", som nu repræsenterer det næste ID til brug i oprettelse
            return highestId;
        }


        /// <summary>
        ///     Bruges til at tilføje ny bruger
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="city"></param>
        /// <param name="address"></param>
        /// <param name="date"></param>
        /// <param name="postal"></param>
        /// <param name="isAdmin"></param>
        /// <param name="isCoach"></param>
        /// <param name="membershipType"></param>
        public void AddUser(string firstName, string lastName, string email, string city, string address, DateOnly date, int? postal, bool isAdmin, bool isCoach, MembershipType membershipType)
        {
            // Næste ID genereres ved at kalde funktionen "GetNewId" og give en List<User> som argument. Her sættes "users" som argument.
            int newId = this.GetNewId(this.users);
            // Instantierer nyt User objekt ud fra funktionen "AddUser" parametre
            this.users.Add(new User(newId, firstName, lastName, email, address, city, "1234", isCoach, isAdmin, date, postal, membershipType));
        }
    }
}
