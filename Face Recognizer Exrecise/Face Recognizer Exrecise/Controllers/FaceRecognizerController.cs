using Face_Recognizer_Exrecise.DB;
using Face_Recognizer_Exrecise.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Face_Recognizer_Exrecise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class FaceRecognizerController : ControllerBase
    {
        private readonly IMongoCollection<RecognizerPerson> _persons;

        public FaceRecognizerController(IRecognizerPersonsDBSettings settings)
        {
            //create connction to the MongoDB
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            //create interdace for MongoDB
            _persons = database.GetCollection<RecognizerPerson>(settings.RecognizerPersonsCollectionName);
        }

        [HttpGet]
        public ActionResult<IQueryable<RecognizerPerson>> FindPerson(string features)
        {
            try
            {         
                //get dot product class
                DotProductClass dotProductClass = new DotProductClass();
                //create dictionary for return the 3 person that closet
                Dictionary<string, double> closer_persons = new Dictionary<string, double>();

                //run on the persons from DB and send them to the  
                foreach (var item in _persons.Find(_ => true).ToList())                
                    closer_persons.Add(item.Name, dotProductClass.GetClosetVector(Array.ConvertAll(item.Features.Split(','), float.Parse), Array.ConvertAll(features.Split(','), float.Parse)));
     
                //return true and the persons
                return Ok(closer_persons.OrderBy(x => x.Value).Reverse().Take(3).ToDictionary(x => x.Key, x => x.Value));
            }
            catch (Exception e)
            {
                //if have exception return it and false
                return ValidationProblem(e.Message);
            }
        }
          
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RecognizerPerson> AddPerson(string name, string features)
        {
            //create fix size of 256 per features
            const int features_fix_size = 256; 
            try
            {
                //get the persons from the DB
                var list = _persons.Find(_ => true).ToList();

                //limit the persons to 10000
                if (list.Count > 10000)
                    return ValidationProblem("Max persons is 10000");

                //save on person key name
                if (list.Find(x => x.Name == name)!= null)
                    return ValidationProblem("Person already exist");

                //split the arr by ',' for check the fix size is ok
                if (features.Split(',').Length == features_fix_size)
                {
                    //insert the person
                    _persons.InsertOne(new RecognizerPerson(name, features));

                    return Ok();
                }
                else 
                    //not fix size
                    return ValidationProblem("features must 256");

            }
            catch (Exception e)
            {
                //if have exception return it and false
                return ValidationProblem(e.Message);
            }
        }
    }
}
