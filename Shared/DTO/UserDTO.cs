using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class UserDTO
    {


        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime CreateDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMailAddress { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
