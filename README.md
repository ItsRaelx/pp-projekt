# PP-Projekt
Projekt z Podstaw Programowania UMG

<br>

**Temat projektu:**
- Wykonanie prototypu wirtualnej uczelni w C# (Aplikacja Konsolowa)

<br>

**1. Importowanie wymaganych bibliotek (MongoDB do uzywania bazy danych, bson do formatowania plików z bazy danych)**
```
using dotenv.net;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
```

**2. Wczytanie zmiennych z .env (Danych do logowania itp.)**
```
var env = DotEnv.Read();

// Łączenie z bazą danych
MongoClient dbClient = new MongoClient(env["MONGODB"]);

var database = dbClient.GetDatabase (env["MONGO_DATABASE"]);
var collection = database.GetCollection<BsonDocument> (env["MONGO_COLLECTION"]);
```

**3. Tak można tworzyć nowe wpisy w bazie danych**
```
var document = new BsonDocument { 
    { "student_id", 1 }, 
    {"scores", new BsonArray {}}, 
    { "class_id", 480 }
};

collection.InsertOne(document);
```

**4. Insertowanie danych do istniejącego dokumentu w bazie danych**
```
Console.Write("Type: ");
string type = Console.ReadLine();

Console.Write("Score: ");
double score = double.Parse(Console.ReadLine());

var arrayFilter = Builders<BsonDocument>.Filter.Eq("student_id", 1);
var arrayUpdate = Builders<BsonDocument>.Update.Push<BsonDocument>("scores", 
    new BsonDocument { { "type", type }, { "score", score } });

collection.UpdateOne(arrayFilter , arrayUpdate);
Console.WriteLine("Done!");
```

**5. Wyświetlanie tylko jednej zmiennej z całego dokumentu pobranego z bazy**
```
var filter = Builders<BsonDocument>.Filter.Eq("student_id", 1);
var studentDocument = collection.Find(filter).FirstOrDefault();

Console.WriteLine(studentDocument["scores"]);
```
