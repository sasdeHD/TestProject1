using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject1.Data.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StatementDate { get; set; }
        public DateTime Created { get; } = DateTime.Now;
        public string Message { get; set; }
        public string Email { get; set; }

        [Column(TypeName = "jsonb")]
        public Dictionary<string, object>? AdditionalData { get; set; }

    }
}
