using dotenv.net;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using BCrypt.Net;

var env = DotEnv.Read();

MongoClient dbClient = new MongoClient(env["MONGODB"]);

var database = dbClient.GetDatabase (env["MONGO_DATABASE"]);
var students = database.GetCollection<BsonDocument> (env["MONGO_STUDENTS"]);
var workers = database.GetCollection<BsonDocument> (env["MONGO_WORKERS"]);
var classes = database.GetCollection<BsonDocument> (env["MONGO_CLASSES"]);

bool isLoged = false;
bool isStudent = true;
var password = string.Empty;
var login = string.Empty;

while (isLoged != true)
{
    Console.WriteLine();
    isLoged = false;
    password = string.Empty;

    // Login on console
    Console.Write("Login: ");
    login = Console.ReadLine();

    // Password login on console with limit of 25 characters
    Console.Write("Password: ");
    ConsoleKey key;

    do
    {
        var keyInfo = Console.ReadKey(intercept: true);
        key = keyInfo.Key;

        if (key == ConsoleKey.Backspace && password.Length > 0)
        {
            Console.Write("\b \b");
            password = password[0..^1];
        }
        else if (!char.IsControl(keyInfo.KeyChar) && !(password.Length >= 25) )
        {
            Console.Write("*");
            password += keyInfo.KeyChar;
        }
    } while (key != ConsoleKey.Enter);

    // Do a line of space
    Console.WriteLine();
    Console.WriteLine();

    // Search for the user in the database
    var filter = Builders<BsonDocument>.Filter.Eq("login", login);

    // Get the user
    if (students.Find(filter).FirstOrDefault() != null)
    {
        var user = students.Find(filter).FirstOrDefault();
        // Check if the password is correct
        if (BCrypt.Net.BCrypt.Verify(password, user["password"].ToString()))
        {
            Console.WriteLine("Logowanie Udane!");
            isLoged = true;
        }
        else
        {
            Console.WriteLine("Logowanie Nieudane!");
        }
    }
    else if (workers.Find(filter).FirstOrDefault() != null)
    {
        var user = workers.Find(filter).FirstOrDefault();
        // Check if the password is correct
        if (BCrypt.Net.BCrypt.Verify(password, user["password"].ToString()))
        {
            Console.WriteLine("Logowanie Udane!");
            isLoged = true;
            isStudent = false;
        }
        else
        {
            Console.WriteLine("Logowanie Nieudane!");
        }
    }
    else
    {
        Console.WriteLine("Logowanie Nieudane!");
    }
};

// Do a line of space
Console.WriteLine();

// Menu for student and diffrent menu for worker
if (isStudent)
{
    Console.WriteLine("Menu Studenta: ");
    Console.WriteLine("1. Zobacz oceny");
    Console.WriteLine("2. Zobacz plan lekcji");
    Console.WriteLine("3. Zobacz twoje dane");
    Console.WriteLine("6. Zmien haslo");
    Console.WriteLine("7. Wyloguj");
    Console.WriteLine();
    Console.Write("Wybierz opcje: ");
    string option = Console.ReadLine();
    Console.WriteLine();
    
    //get the student
    var filter = Builders<BsonDocument>.Filter.Eq("login", login);
    var user = students.Find(filter).FirstOrDefault();

    switch (option)
    {
        case "1":
            Console.WriteLine("Oceny");
            break;
        case "2":
            Console.WriteLine("Plan lekcji");
            break;
        case "3":
            Console.WriteLine("Twoje dane: ");
            Console.WriteLine();
            var studentName = user["name"];
            var studentFirstName = studentName["firstName"].ToString();
            var studentLastName = studentName["lastName"].ToString();
            var studentMiddleName = studentName["midName"].ToString();
            var studentBirthDate = user["dateOfBirth"].ToString();
            var studentIndex = user["index"].ToString();

            var studentAddress = user["address"];
            var studentStreet = studentAddress["street"].ToString();
            var studentCity = studentAddress["city"].ToString();
            var studentPostalCode = studentAddress["postalCode"].ToString();
            var studentCountry = studentAddress["country"].ToString();
            var studentPhone = user["phoneNumber"].ToString();
            var studentEmail = user["email"].ToString();

            var studentClass = user["class"];
            var studentClassId = studentClass["id"].ToString();
            var studentClassLab = studentClass["lab"].ToString();
            var studentClassCwi = studentClass["cwi"].ToString();
            var studentClassAng = studentClass["ang"].ToString();
            

            // Print the student data
            Console.WriteLine("Imie: " + studentFirstName);
            Console.WriteLine("Nazwisko: " + studentLastName);
            Console.WriteLine("Drugie imie: " + studentMiddleName);
            Console.WriteLine("Data urodzenia: " + studentBirthDate);
            Console.WriteLine("Ulica: " + studentStreet);
            Console.WriteLine("Miasto: " + studentCity);
            Console.WriteLine("Kod pocztowy: " + studentPostalCode);
            Console.WriteLine("Kraj: " + studentCountry);
            Console.WriteLine("Telefon: " + studentPhone);
            Console.WriteLine("Email: " + studentEmail);
            Console.WriteLine("Klasa: " + studentClassId);
            Console.WriteLine("Lab: " + studentClassLab);
            Console.WriteLine("Cwi: " + studentClassCwi);
            Console.WriteLine("Ang: " + studentClassAng);

            break;
        case "6":
            Console.WriteLine("Zmien haslo");
            break;
        case "7":
            Console.WriteLine("Wyloguj");
            break;
        default:
            Console.WriteLine("Nie ma takiej opcji");
            break;
    }

}
else
{
    Console.WriteLine("Menu Pracownika: ");
    Console.WriteLine("1. Dodaj ucznia");
    Console.WriteLine("2. Dodaj nauczyciela");
    Console.WriteLine("3. Dodaj klase");
    Console.WriteLine("4. Dodaj przedmiot");
    Console.WriteLine("5. Dodaj ocene");
    Console.WriteLine("6. Dodaj obecnosc");
    Console.WriteLine("7. Zmien haslo");
    Console.WriteLine("8. Wyloguj");
    Console.WriteLine();
    Console.Write("Wybierz opcje: ");
    string option = Console.ReadLine();

    // Get the worker
    var filter = Builders<BsonDocument>.Filter.Eq("login", login);
    var user = workers.Find(filter).FirstOrDefault();

    switch (option)
    {
        case "1":
             Console.WriteLine("Dodaj studenta");
            Console.WriteLine("imię studenta");
            var firstName = Console.ReadLine();
            Console.WriteLine("drugie imię studenta");
            var midName = Console.ReadLine();
            Console.WriteLine("nazwisko studenta");
            var lastName = Console.ReadLine();
            Console.WriteLine("numer indeksu studenta");
            int index = int.Parse(Console.ReadLine());
            Console.WriteLine("data urodzenia studenta");
            string data = Console.ReadLine();
            //checking date format
            int d=0,m=0,y=0;
            if(data.Length==10||data.Length==8){
                 if(data[2]=='-'){
                            String[] list = data.Split("-");
                             int[] da = Array.ConvertAll(data.Split("-"), new Converter<string, int>(int.Parse));
                             d=da[0];m=da[1];y=da[2];

                            Console.WriteLine(da[0]+" "+da[1]+" "+da[2]);

                }
                 else{
                     String[] list = data.Split(".");
                             int[] da = Array.ConvertAll(data.Split("."), new Converter<string, int>(int.Parse));
                             d=da[0];m=da[1];y=da[2];
                            Console.WriteLine(da[0]+" "+da[1]+" "+da[2]);
                 }
                    }
            if(data.Length==9||data.Length==7){
                 if(data[1]=='-'){
                            String[] list = data.Split("-");
                             int[] da = Array.ConvertAll(data.Split("-"), new Converter<string, int>(int.Parse));
                             d=da[0];m=da[1];y=da[2];
                            Console.WriteLine(da[0]+" "+da[1]+" "+da[2]);

                }
                 else{
                     String[] list = data.Split(".");
                             int[] da = Array.ConvertAll(data.Split("."), new Converter<string, int>(int.Parse));
                             d=da[0];m=da[1];y=da[2];
                            Console.WriteLine(da[0]+" "+da[1]+" "+da[2]);
                 }


            }
            Random rng = new Random();
            int haslo = rng.Next() % (10000000);
            Console.WriteLine("mail studenta");
            //checking if mail is correct

            bool pop=false;
            string mail=string.Empty;
            while(pop==false){
            mail = Console.ReadLine();
             String[] e = mail.Split("@");
                             
            if (e.Length==1) {
                Console.WriteLine("mail jest niepoprawny, podaj poprawny mail");
            }
            else{
                String[] em =e[1].Split(".");
                if (em.Length==1) {
                Console.WriteLine("mail jest niepoprawny, podaj poprawny mail");}
                else{
                Console.WriteLine("mail jest poprawny");
                pop=true;}}
            }
            Console.WriteLine("numer kontaktowy studenta");
            int phonenumber = int.Parse(Console.ReadLine());
            Console.WriteLine("podaj adres studenta");
            Console.WriteLine("kraj:");
            var country= Console.ReadLine();
            Console.WriteLine("miasto:");
            var city= Console.ReadLine();
             Console.WriteLine("ulica:");
            var street= Console.ReadLine();
             Console.WriteLine("nr domu:");
            var HouseNumber= Console.ReadLine();
             Console.WriteLine("nr mieszkania:");
            var ApartmentNumber= Console.ReadLine();
            Console.WriteLine("kod pocztowy:");
            var postalCode= Console.ReadLine();
            




            var student = new BsonDocument
{
    { "name", new BsonDocument
        {
            { "firstName", firstName },
            { "midName", midName },
            { "lastName", lastName }
        } 
    },
    { "index", index },
    { "dateOfBirth", new DateTime(y, m, d+1) },
    { "class", new BsonDocument
        {
            { "id", ObjectId.Parse("6374d25b215d99eb272d1f14") },
            { "specialization", "Def" },
            { "lab", 0 },
            { "cwi", 0 },
            { "ang", 0 }
        }
    },
    { "login", index },
    { "password", haslo },
    { "email", mail },
    { "phoneNumber", phonenumber },
    { "address", new BsonDocument
        {
            { "Country", country },
            { "HouseNumber", HouseNumber },
            { "ApartmentNumber", ApartmentNumber },
            { "street", street },
            { "city", city },
            { "postalCode", postalCode }
        }
    },
    { "specialization", "" },
    { "grades", new BsonDocument{} }
};

            students.InsertOne(student);

            break;
        case "2":
            Console.WriteLine("Dodaj nauczyciela");
            break;
        case "3":
            Console.WriteLine("Dodaj klase");
            break;
        case "4":
            Console.WriteLine("Dodaj przedmiot");
            break;
        case "5":
            Console.WriteLine("Dodaj ocene");
            break;
        case "6":
            Console.WriteLine("Dodaj obecnosc");
            break;
        case "7":
            Console.WriteLine("Zmien haslo");
            break;
        case "8":
            Console.WriteLine("Wyloguj");
            break;
        default:
            Console.WriteLine("Nie ma takiej opcji");
            break;
    }
}
//siemano test test test
// Siemano