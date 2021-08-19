using Face_Recognizer_Exrecise.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Face_Recognizer_Exrecise.DB
{
        public class RecognizerPersonsDB : IRecognizerPersonsDBSettings
        {
            public string RecognizerPersonsCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface IRecognizerPersonsDBSettings
        {
            string RecognizerPersonsCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }     
    }
