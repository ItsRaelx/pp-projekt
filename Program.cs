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
var password = string.Empty;

while (isLoged != true)
{
    isLoged = false;
    password = string.Empty;

    // Login on console
    Console.Write("Login: ");
    string login = Console.ReadLine();

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

    // Search for the user in the database
    var filter = Builders<BsonDocument>.Filter.Eq("login", login);

    // Get the user
    if (students.Find(filter).FirstOrDefault() != null)
    {
        var user = students.Find(filter).FirstOrDefault();
        // Check if the password is correct
        if (BCrypt.Net.BCrypt.Verify(password, user["password"].ToString()))
        {
            Console.WriteLine("Login successful!");
            isLoged = true;
        }
        else
        {
            Console.WriteLine("Login failed!");
        }
    }
    else if (workers.Find(filter).FirstOrDefault() != null)
    {
        var user = workers.Find(filter).FirstOrDefault();
        // Check if the password is correct
        if (BCrypt.Net.BCrypt.Verify(password, user["password"].ToString()))
        {
            Console.WriteLine("Login successful!");
            isLoged = true;
        }
        else
        {
            Console.WriteLine("Login failed!");
        }
    }
    else
    {
        Console.WriteLine("Login failed!");
    }
};

/* Create student document with: 
name, date of birth, class, 
login, password, email, phone number, 
address, and list of grades */

var student = new BsonDocument
{
    { "name", new BsonDocument
        {
            { "firstName", "Adam" },
            { "midName", "Michał" },
            { "lastName", "Kowalski" }
        } 
    },
    { "dateOfBirth", new DateTime(2000, 1, 1) },
    { "class", new BsonDocument
        {
            { "id", ObjectId.Parse("6374d25b215d99eb272d1f14") },
            { "specialization", "Def" },
            { "lab", 0 },
            { "cwi", 0 },
            { "ang", 0 }
        }
    },
    { "login", "johndoe" },
    { "password", password },
    { "email", "example@example.pl" },
    { "phoneNumber", 123123123 },
    { "address", new BsonDocument
        {
            { "Country", "Poland" },
            { "HouseNumber", "1" },
            { "ApartmentNumber", "1" },
            { "street", "Example Street" },
            { "city", "Example City" },
            { "postalCode", "00-000" }
        }
    },
    { "specialization", "" },
    { "grades", new BsonDocument{} }
};

// Insert student document into collection
//students.InsertOne(student);

/* Create bson worker document with:
name, date of birth, login, password, email, phone number,
address, salary, position, subjects, degree */

var worker = new BsonDocument
{
    { "name", new BsonDocument
        {
            { "firstName", "Adam" },
            { "midName", "Michał" },
            { "lastName", "Kowalski" }
        } 
    },
    { "dateOfBirth", new DateTime(2000, 1, 1) },
    { "login", "johndoe" },
    { "password", password },
    { "email", "example@example.pl" },
    { "phoneNumber", 123123123 },
    { "address", new BsonDocument
        {
            { "Country", "Poland" },
            { "HouseNumber", "1" },
            { "ApartmentNumber", "1" },
            { "street", "Example Street" },
            { "city", "Example City" },
            { "postalCode", "00-000" }
        }
    },
    { "salary", 1000 },
    { "position", "nauczyciel" },
    { "subjects", new BsonArray { "matematyka", "fizyka" } },
    { "degree", "doktor" }
};

// Insert worker document into workers collection
//workers.InsertOne(worker);

/* Create bson class document with:
semester, field of study, specializations, faculty */

var classDocument = new BsonDocument
{
    { "semester", 1 },
    { "fieldOfStudy", "informatyka" },
    { "specializations", new BsonDocument {
        // Specializations (Def is short for Default)
        { "Def", new BsonDocument {
            { "timetable", new BsonDocument {
                // Days of the week from M to U
                { "M", new BsonDocument {
                    // Hours from 7:00 to 20:00
                    { "7", new BsonDocument {} },
                    { "8", new BsonDocument {} },
                    { "9", new BsonDocument {} },
                    { "10", new BsonDocument {} },
                    { "11", new BsonDocument {} },
                    { "12", new BsonDocument {} },
                    { "13", new BsonDocument {} },
                    { "14", new BsonDocument {} },
                    { "15", new BsonDocument {} },
                    { "16", new BsonDocument {} },
                    { "17", new BsonDocument {} },
                    { "18", new BsonDocument {} },
                    { "19", new BsonDocument {} },
                    { "20", new BsonDocument {} }
                    } 
                }
            } }
            }
        },
        { "IoT", new BsonDocument {} },
        { "AiS", new BsonDocument {} }
    } },
    { "faculty", "informatyka" }
};

// Insert class document into classes collection
//classes.InsertOne(classDocument);

