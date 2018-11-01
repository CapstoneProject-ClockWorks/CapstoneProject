using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;


namespace CapstoneProject.Models
{
    public class WorkSpace
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("workspace_name")]
        public string workspace_name { get; set; }
        [BsonElement("decprition")]
        public string decprition { get; set; }
        [BsonElement("image")]
        public string image { get; set; }
    }
}