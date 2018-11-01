using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace CapstoneProject.Models.Repository
{
    public class ContactCollectionWorkSpace
    {
        //Intializes the mongo db repository
        internal MongoDbRepo _repo = new MongoDbRepo("mongodb://DuyHo:hothanhphuongduy2101@ds227469.mlab.com:27469/capstoneproject_k21t1_team8", "capstoneproject_k21t1_team8");
        //Defines the collection name used by project
        private const string _collectionName = "WorkPlace";
        //Contains all documents inside the collection
        public IMongoCollection<WorkSpace> Collection;

        public ContactCollectionWorkSpace()
        {
            this.Collection = _repo.Db.GetCollection<WorkSpace>(_collectionName);
        }

        /// <summary>
        /// Insert a contract inside the collection
        /// </summary>
        /// <param name="contact">Contract to inser</param>
        public void InsertContact(WorkSpace contact)
        {
            this.Collection.InsertOneAsync(contact);
        }
        /// <summary>
        /// Selects all documents
        /// </summary>
        /// <returns></returns>
        public List<WorkSpace> SelectAll()
        {
            var query = this.Collection.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }
        /// <summary>
        /// Get a contract by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public WorkSpace Get(string id)
        {
            return this.Collection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).FirstAsync().Result;
        }
        /// <summary>
        /// Updates an contract
        /// </summary>
        /// <param name="id">Id of contract to update</param>
        /// <param name="contact">Updated informations</param>
        public void UpdateContact(string id, WorkSpace contact)
        {
            contact._id = new ObjectId(id);

            var filter = Builders<WorkSpace>.Filter.Eq(s => s._id, contact._id);
            this.Collection.ReplaceOneAsync(filter, contact);
        }
    }
}