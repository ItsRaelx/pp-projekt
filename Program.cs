using dotenv.net;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

var env = DotEnv.Read();

MongoClient dbClient = new MongoClient(env["MONGODB"]);

var database = dbClient.GetDatabase (env["MONGO_DATABASE"]);
var collection = database.GetCollection<BsonDocument> (env["MONGO_COLLECTION"]);

// var document = new BsonDocument { 
//     { "student_id", 1 }, 
//     {"scores", new BsonArray {}}, 
//     { "class_id", 480 }
// };

// collection.InsertOne(document);

// Console.Write("Type: ");
// string type = Console.ReadLine();
// Console.Write("Score: ");
// double score = double.Parse(Console.ReadLine());
// var arrayFilter = Builders<BsonDocument>.Filter.Eq("student_id", 1);
// var arrayUpdate = Builders<BsonDocument>.Update.Push<BsonDocument>("scores", new BsonDocument { { "type", type }, { "score", score } });
// collection.UpdateOne(arrayFilter , arrayUpdate);
// Console.WriteLine("Done!");

var filter = Builders<BsonDocument>.Filter.Eq("student_id", 1);
var studentDocument = collection.Find(filter).FirstOrDefault();

Console.WriteLine(studentDocument["scores"]);