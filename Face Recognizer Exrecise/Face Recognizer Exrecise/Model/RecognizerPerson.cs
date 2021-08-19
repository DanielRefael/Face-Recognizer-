using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Face_Recognizer_Exrecise.Model
{
    public class RecognizerPerson
    {

        private string name;
        [Key]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string features;
        public string Features
        {
            get { return features; }
            set { features = value; }
        }

        private ObjectId id;
        public ObjectId Id
        {
            get { return id; }
            set { id = value; }
        }

        public RecognizerPerson (string _name, string _features)
        {
            Name = _name;
            Features = _features;
        }
        public RecognizerPerson()
        {

        }
    }
}
