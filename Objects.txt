// /* Create student document with: 
// name, date of birth, class, 
// login, password, email, phone number, 
// address, and list of grades */

// var student = new BsonDocument
// {
//     { "name", new BsonDocument
//         {
//             { "firstName", "Adam" },
//             { "midName", "Michał" },
//             { "lastName", "Kowalski" }
//         } 
//     },
//     { "index", "123456" },
//     { "dateOfBirth", new DateTime(2000, 1, 1) },
//     { "class", new BsonDocument
//         {
//             { "id", ObjectId.Parse("6374d25b215d99eb272d1f14") },
//             { "specialization", "Def" },
//             { "lab", 0 },
//             { "cwi", 0 },
//             { "ang", 0 }
//         }
//     },
//     { "login", "johndoe" },
//     { "password", password },
//     { "email", "example@example.pl" },
//     { "phoneNumber", 123123123 },
//     { "address", new BsonDocument
//         {
//             { "Country", "Poland" },
//             { "HouseNumber", "1" },
//             { "ApartmentNumber", "1" },
//             { "street", "Example Street" },
//             { "city", "Example City" },
//             { "postalCode", "00-000" }
//         }
//     },
//     { "specialization", "" },
//     { "grades", new BsonDocument{} }
// };

// // Insert student document into collection
// //students.InsertOne(student);

// /* Create bson worker document with:
// name, date of birth, login, password, email, phone number,
// address, salary, position, subjects, degree */

// var worker = new BsonDocument
// {
//     { "name", new BsonDocument
//         {
//             { "firstName", "Adam" },
//             { "midName", "Michał" },
//             { "lastName", "Kowalski" }
//         } 
//     },
//     { "dateOfBirth", new DateTime(2000, 1, 1) },
//     { "login", "johndoe" },
//     { "password", password },
//     { "email", "example@example.pl" },
//     { "phoneNumber", 123123123 },
//     { "address", new BsonDocument
//         {
//             { "Country", "Poland" },
//             { "HouseNumber", "1" },
//             { "ApartmentNumber", "1" },
//             { "street", "Example Street" },
//             { "city", "Example City" },
//             { "postalCode", "00-000" }
//         }
//     },
//     { "salary", 1000 },
//     { "position", "nauczyciel" },
//     { "subjects", new BsonArray { "matematyka", "fizyka" } },
//     { "degree", "doktor" }
// };

// // Insert worker document into workers collection
// //workers.InsertOne(worker);

// /* Create bson class document with:
// semester, field of study, specializations, faculty */

// var classDocument = new BsonDocument
// {
//     { "semester", 1 },
//     { "fieldOfStudy", "informatyka" },
//     { "specializations", new BsonDocument {
//         // Specializations (Def is short for Default)
//         { "Def", new BsonDocument {
//             { "timetable", new BsonDocument {
//                 // Days of the week from M to U
//                 { "M", new BsonDocument {
//                     // Hours from 7:00 to 20:00
//                     { "7", new BsonDocument {} },
//                     { "8", new BsonDocument {} },
//                     { "9", new BsonDocument {} },
//                     { "10", new BsonDocument {} },
//                     { "11", new BsonDocument {} },
//                     { "12", new BsonDocument {} },
//                     { "13", new BsonDocument {} },
//                     { "14", new BsonDocument {} },
//                     { "15", new BsonDocument {} },
//                     { "16", new BsonDocument {} },
//                     { "17", new BsonDocument {} },
//                     { "18", new BsonDocument {} },
//                     { "19", new BsonDocument {} },
//                     { "20", new BsonDocument {} }
//                     } 
//                 }
//             } }
//             }
//         },
//         { "IoT", new BsonDocument {} },
//         { "AiS", new BsonDocument {} }
//     } },
//     { "faculty", "informatyka" }
// };

// // Insert class document into classes collection
// //classes.InsertOne(classDocument);
